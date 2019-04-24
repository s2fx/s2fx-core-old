using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using S2fx.Remoting;

namespace S2fx.Mvc.Remoting {

    public abstract class AbstractRemoteServiceModelConvention {
        protected IHttpContextAccessor HttpContextAccessor { get; }

        protected IServiceProvider Services => this.HttpContextAccessor.HttpContext.RequestServices;
        protected IRemoteServiceManager RemoteServiceManager => this.Services.GetRequiredService<IRemoteServiceManager>();

        public AbstractRemoteServiceModelConvention(IHttpContextAccessor httpContextAccessor) {
            this.HttpContextAccessor = httpContextAccessor;
        }

    }

}
