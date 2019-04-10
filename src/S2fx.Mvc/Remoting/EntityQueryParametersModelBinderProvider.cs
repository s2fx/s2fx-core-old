using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using S2fx.Remoting.RemoteServices;

namespace S2fx.Mvc.Remoting {

    public class EntityQueryParametersModelBinderProvider : IModelBinderProvider {

        public EntityQueryParametersModelBinderProvider() {

        }

        public IModelBinder GetBinder(ModelBinderProviderContext context) {
            if (context == null) {
                throw new ArgumentNullException(nameof(context));
            }

            if (context.Metadata.ModelType == typeof(EntityQueryParameters)) {
                return new BinderTypeModelBinder(typeof(EntityQueryParametersModelBinder));
            }

            return null;
        }

    }
}
