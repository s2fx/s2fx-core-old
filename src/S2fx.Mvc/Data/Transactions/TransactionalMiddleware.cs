using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OrchardCore.Environment.Shell;
using S2fx.Data.Transactions;
using S2fx.Remoting;

namespace S2fx.Mvc.Data.Transactions {

    public class TransactionalMiddleware {
        readonly RequestDelegate _next;

        public TransactionalMiddleware(RequestDelegate next) {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext) {
            var shellSettings = httpContext.RequestServices.GetRequiredService<ShellSettings>();

            if (!string.IsNullOrEmpty(shellSettings["DatabaseConnectionString"])) {
                var txm = httpContext.RequestServices.GetRequiredService<ITransactionManager>();
                // TODO 不是每次请求都要事务
                using (var tx = txm.BeginTransaction()) {
                    await _next.Invoke(httpContext);
                    await tx.CommitAsync();
                }
            }
            else {
                await _next.Invoke(httpContext);
            }

        }
    }

}
