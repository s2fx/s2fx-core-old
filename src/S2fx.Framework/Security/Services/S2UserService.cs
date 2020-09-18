using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using OrchardCore.Users;
using OrchardCore.Users.Services;
using S2fx.Security.Model;

namespace S2fx.Security.Services {

    /// <summary>
    /// Implements <see cref="IUserService"/> by using the ASP.NET Core Identity packages.
    /// </summary>
    public class S2UserService : IUserService {
        private readonly SignInManager<IUser> _signInManager;
        private readonly UserManager<IUser> _userManager;
        private readonly IOptions<IdentityOptions> _identityOptions;

        public S2UserService(
            SignInManager<IUser> signInManager,
            UserManager<IUser> userManager,
            IOptions<IdentityOptions> identityOptions) {
            _signInManager = signInManager;
            _userManager = userManager;
            _identityOptions = identityOptions;
        }

        public async Task<IUser> AuthenticateAsync(string userName, string password, Action<string, string> reportError) {
            if (string.IsNullOrWhiteSpace(userName)) {
                reportError("UserName", "A user name is required.");
                return null;
            }

            if (string.IsNullOrWhiteSpace(password)) {
                reportError("Password", "A password is required.");
                return null;
            }

            var user = await _userManager.FindByNameAsync(userName);
            if (user == null) {
                reportError(string.Empty, "The specified username/password couple is invalid.");
                return null;
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, password, lockoutOnFailure: true);
            if (result.IsNotAllowed) {
                reportError(string.Empty, "The specified user is not allowed to sign in.");
                return null;
            }
            else if (result.RequiresTwoFactor) {
                reportError(string.Empty, "The specified user is not allowed to sign in using password authentication.");
                return null;
            }
            else if (!result.Succeeded) {
                reportError(string.Empty, "The specified username/password couple is invalid.");
                return null;
            }

            return user;
        }

        public async Task<IUser> CreateUserAsync(IUser user, string password, Action<string, string> reportError) {
            if (!(user is UserEntity newUser)) {
                throw new ArgumentException("Expected a User instance.", nameof(user));
            }

            // Accounts can be created with no password
            var identityResult = String.IsNullOrWhiteSpace(password)
                ? await _userManager.CreateAsync(user)
                : await _userManager.CreateAsync(user, password);
            if (!identityResult.Succeeded) {
                ProcessValidationErrors(identityResult.Errors, newUser, reportError);
                return null;
            }

            return user;
        }

        public async Task<bool> ChangePasswordAsync(IUser user, string currentPassword, string newPassword, Action<string, string> reportError) {
            var identityResult = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);

            if (!identityResult.Succeeded) {
                ProcessValidationErrors(identityResult.Errors, (UserEntity)user, reportError);
            }

            return identityResult.Succeeded;
        }

        public Task<IUser> GetAuthenticatedUserAsync(ClaimsPrincipal principal) {
            if (principal == null) {
                return Task.FromResult<IUser>(null);
            }

            return _userManager.GetUserAsync(principal);
        }

        public async Task<IUser> GetForgotPasswordUserAsync(string userIdentifier) {
            if (string.IsNullOrWhiteSpace(userIdentifier)) {
                return await Task.FromResult<IUser>(null);
            }

            var user = await FindByUsernameOrEmailAsync(userIdentifier) as UserEntity;

            if (user == null) {
                return await Task.FromResult<IUser>(null);
            }

            user.ResetToken = await _userManager.GeneratePasswordResetTokenAsync(user);

            return user;
        }

        public async Task<bool> ResetPasswordAsync(string userIdentifier, string resetToken, string newPassword, Action<string, string> reportError) {
            var result = true;
            if (string.IsNullOrWhiteSpace(userIdentifier)) {
                reportError("UserName", "A user name or email is required.");
                result = false;
            }

            if (string.IsNullOrWhiteSpace(newPassword)) {
                reportError("Password", "A password is required.");
                result = false;
            }

            if (string.IsNullOrWhiteSpace(resetToken)) {
                reportError("Token", "A token is required.");
                result = false;
            }

            if (!result) {
                return result;
            }

            var user = await FindByUsernameOrEmailAsync(userIdentifier) as UserEntity;

            if (user == null) {
                return false;
            }

            var identityResult = await _userManager.ResetPasswordAsync(user, resetToken, newPassword);

            if (!identityResult.Succeeded) {
                ProcessValidationErrors(identityResult.Errors, user, reportError);
            }

            return identityResult.Succeeded;
        }

        public Task<ClaimsPrincipal> CreatePrincipalAsync(IUser user) {
            if (user == null) {
                return Task.FromResult<ClaimsPrincipal>(null);
            }

            return _signInManager.CreateUserPrincipalAsync(user);
        }

        /// <summary>
        /// Gets the user, if any, associated with the normalized value of the specified identifier, which can refer both to username or email
        /// </summary>
        /// <param name="userIdentification">The username or email address to refer to</param>
        private async Task<IUser> FindByUsernameOrEmailAsync(string userIdentifier)
            => await _userManager.FindByNameAsync(userIdentifier) ??
               await _userManager.FindByEmailAsync(userIdentifier);

        public Task<IUser> GetUserAsync(string userName) => _userManager.FindByNameAsync(userName);

        public Task<IUser> GetUserByUniqueIdAsync(string userIdentifier) => _userManager.FindByIdAsync(userIdentifier);

        public void ProcessValidationErrors(IEnumerable<IdentityError> errors, UserEntity user, Action<string, string> reportError) {
            foreach (var error in errors) {
                switch (error.Code) {
                    // Password
                    case "PasswordRequiresDigit":
                        reportError("Password", "Passwords must have at least one digit character ('0'-'9').");
                        break;
                    case "PasswordRequiresLower":
                        reportError("Password", "Passwords must have at least one lowercase character ('a'-'z').");
                        break;
                    case "PasswordRequiresUpper":
                        reportError("Password", "Passwords must have at least one uppercase character ('A'-'Z').");
                        break;
                    case "PasswordRequiresNonAlphanumeric":
                        reportError("Password", "Passwords must have at least one non letter or digit character.");
                        break;
                    case "PasswordTooShort":
                        reportError("Password", $"Passwords must be at least {_identityOptions.Value.Password.RequiredLength} characters.");
                        break;
                    case "PasswordRequiresUniqueChars":
                        reportError("Password", $"Passwords must contain at least {_identityOptions.Value.Password.RequiredUniqueChars} unique characters.");
                        break;

                    // CurrentPassword
                    case "PasswordMismatch":
                        reportError("CurrentPassword", "Incorrect password.");
                        break;

                    // User name
                    case "InvalidUserName":
                        reportError("UserName", $"User name '{user.UserName}' is invalid, can only contain letters or digits.");
                        break;
                    case "DuplicateUserName":
                        reportError("UserName", $"User name '{user.UserName}' is already used.");
                        break;

                    // Email
                    case "InvalidEmail":
                        reportError("Email", $"Email '{user.Email}' is invalid.");
                        break;
                    default:
                        reportError(string.Empty, $"Unexpected error: '{error.Code}'.");
                        break;
                }
            }
        }

        public Task<bool> ChangeEmailAsync(IUser user, string newEmail, Action<string, string> reportError) {
            throw new NotImplementedException();
        }
    }
}
