using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using S2fx.Remoting.RemoteServices;

namespace S2fx.Web.Remoting {

    public class EntityQueryParametersModelBinder : IModelBinder {

        public Task BindModelAsync(ModelBindingContext bindingContext) {
            if (bindingContext == null) {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            var model = new EntityQueryParameters {
                QueryString = bindingContext.HttpContext.Request.QueryString.Value,
            };

            //TODO 
            bindingContext.Result = ModelBindingResult.Success(model);
            return Task.CompletedTask;

            /*
                bindingContext.ModelState.TryAddModelError(
                                        bindingContext.ModelName,
                                        "Author Id must be an integer.");
                                        */
        }

    }

}
