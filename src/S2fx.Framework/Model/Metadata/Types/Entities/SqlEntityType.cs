using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using S2fx.Model.Annotations;
using S2fx.Model.Environment;
using S2fx.Utility;

namespace S2fx.Model.Metadata.Types {

    public class SqlEntityType : IEntityType {

        public string Name => BuiltinEntityTypeNames.SqlEntityTypeName;

        private readonly IEnumerable<IFieldType> _propertyTypes;

        public SqlEntityType(IEnumerable<IFieldType> types) {
            _propertyTypes = types;
        }

        public override int GetHashCode() =>
            this.Name.GetHashCode();
    }

}
