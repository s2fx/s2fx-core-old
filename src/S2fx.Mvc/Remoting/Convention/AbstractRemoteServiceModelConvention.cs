using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using S2fx.Remoting;

namespace S2fx.Mvc.Remoting {

    public abstract class AbstractRemoteServiceModelConvention {
        protected IServiceProvider Services { get; }
        protected IRemoteServiceManager RemoteServiceManager { get; }

        public AbstractRemoteServiceModelConvention(IServiceProvider services) {
            this.Services = services;
            this.RemoteServiceManager = this.Services.GetRequiredService<IRemoteServiceManager>();

        }

    }

}
