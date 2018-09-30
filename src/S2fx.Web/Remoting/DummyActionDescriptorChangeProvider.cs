using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Primitives;

namespace S2fx.Web.Remoting {

    public class DummyActionDescriptorChangeProvider : IActionDescriptorChangeProvider {
        public static DummyActionDescriptorChangeProvider Instance { get; } = new DummyActionDescriptorChangeProvider();

        public CancellationTokenSource TokenSource { get; private set; }

        public bool HasChanged { get; set; }

        public IChangeToken GetChangeToken() {
            TokenSource = new CancellationTokenSource();
            return new CancellationChangeToken(TokenSource.Token);
        }

    }

}
