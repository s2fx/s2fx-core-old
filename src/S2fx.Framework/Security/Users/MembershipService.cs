using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using OrchardCore.Users;
using OrchardCore.Users.Services;

namespace S2fx.Security.Users {

    public class MembershipService : IMembershipService {

        public Task<bool> CheckPasswordAsync(string userName, string password) {
            throw new NotImplementedException();
        }

        public Task<ClaimsPrincipal> CreateClaimsPrincipal(IUser user) {
            throw new NotImplementedException();
        }

        public Task<IUser> GetUserAsync(string userName) {
            throw new NotImplementedException();
        }

    }
}
