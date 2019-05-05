using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.Environment.Shell;
using S2fx.Data.Transactions;

namespace S2fx.Mvc.Data.Transactions {

    public class TransactionalMiddleware {
        readonly RequestDelegate _next;
        readonly IHttpContextAccessor _hca;

        public TransactionalMiddleware(RequestDelegate next, IHttpContextAccessor hca) {
            _next = next;
            _hca = hca;
        }

        public async Task Invoke(HttpContext httpContext) {
            var shellSettings = _hca.HttpContext.RequestServices.GetRequiredService<ShellSettings>();

            if (!string.IsNullOrEmpty(shellSettings["DatabaseConnectionString"])) {
                var txm = _hca.HttpContext.RequestServices.GetRequiredService<ITransactionManager>();
                // TODO 不是每次请求都要事务
                using (var tx = txm.BeginTransaction()) {
                    await _next.Invoke(httpContext);
                    await tx.CommitAsync();
                }
            }

        }
    }

}
