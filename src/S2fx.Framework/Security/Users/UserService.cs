using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using OrchardCore.Users;
using OrchardCore.Users.Services;

namespace S2fx.Security.Users {

    public class UserService : IUserService {
        public Task<IUser> AuthenticateAsync(string userName, string password, Action<string, string> reportError) {
            throw new NotImplementedException();
        }

        public Task<bool> ChangePasswordAsync(IUser user, string currentPassword, string newPassword, Action<string, string> reportError) {
            throw new NotImplementedException();
        }

        public Task<ClaimsPrincipal> CreatePrincipalAsync(IUser user) {
            throw new NotImplementedException();
        }

        public Task<IUser> CreateUserAsync(string userName, string email, string[] roleNames, string password, Action<string, string> reportError) {
            throw new NotImplementedException();
        }

        public Task<IUser> CreateUserAsync(IUser user, string password, Action<string, string> reportError) {
            throw new NotImplementedException();
        }

        public Task<IUser> GetAuthenticatedUserAsync(ClaimsPrincipal principal) {
            throw new NotImplementedException();
        }

        public Task<IUser> GetForgotPasswordUserAsync(string userIdentifier) {
            throw new NotImplementedException();
        }

        public Task<IUser> GetUserAsync(string userName) {
            throw new NotImplementedException();
        }

        public Task<IUser> GetUserByUniqueIdAsync(string userIdentifier) {
            throw new NotImplementedException();
        }

        public Task<bool> ResetPasswordAsync(string userIdentifier, string resetToken, string newPassword, Action<string, string> reportError) {
            throw new NotImplementedException();
        }
    }
}
