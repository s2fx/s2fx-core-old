using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.Users;
using OrchardCore.Users.Services;
using S2fx.Conventions;
using S2fx.Data.Importing;
using S2fx.Data.Seeding;
using S2fx.Data.UnitOfWork;
using S2fx.Security.Services;

namespace S2fx.Security {

    public static class ServiceCollectionExtensions {

        public static void AddS2Security(this IServiceCollection services) {

            // services.AddTransient<IDynamicEntityRepositoryResolver, DynamicEntityRepositoryResolver>();

            services.AddTransient<IUserService, S2UserService>();
            services.AddTransient<IMembershipService, S2MembershipService>();
            services.AddScoped<IUserClaimsPrincipalFactory<IUser>, S2UserClaimsPrincipalFactory>();
        }

    }

}
