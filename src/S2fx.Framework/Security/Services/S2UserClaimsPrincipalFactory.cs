using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using OrchardCore.Users;
using S2fx.Security.Model;

namespace S2fx.Security.Services {

    /// Custom implementation of  <see cref="IUserClaimsPrincipalFactory"/> adding email claims.
    /// </summary>
    public class S2UserClaimsPrincipalFactory : UserClaimsPrincipalFactory<IUser, IRole> {
        private readonly UserManager<IUser> _userManager;
        private readonly IOptions<IdentityOptions> _identityOptions;

        public S2UserClaimsPrincipalFactory(
            UserManager<IUser> userManager,
            RoleManager<IRole> roleManager,
            IOptions<IdentityOptions> identityOptions) : base(userManager, roleManager, identityOptions) {
            _userManager = userManager;
            _identityOptions = identityOptions;
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(IUser user) {
            var claims = await base.GenerateClaimsAsync(user);

            var email = await _userManager.GetEmailAsync(user);

            if (email != null) {
                claims.AddClaim(new Claim("email", email));
            }

            if (await _userManager.IsEmailConfirmedAsync(user)) {
                claims.AddClaim(new Claim("email_verified", "true"));
            }

            return claims;
        }
    }

}
