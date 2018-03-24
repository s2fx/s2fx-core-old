using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using OrchardCore.Users;
using OrchardCore.Users.Services;

namespace S2fx.Security.Users {

    public class UserService : IUserService {
        public Task<bool> ChangePasswordAsync(IUser user, string currentPassword, string newPassword, Action<string, string> reportError) {
            throw new NotImplementedException();
        }

        public Task<IUser> CreateUserAsync(string userName, string email, string[] roleNames, string password, Action<string, string> reportError) {
            throw new NotImplementedException();
        }

        public Task<IUser> GetAuthenticatedUserAsync(ClaimsPrincipal principal) {
            throw new NotImplementedException();
        }
    }
}
