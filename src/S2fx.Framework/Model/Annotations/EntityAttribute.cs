using System;

namespace S2fx.Model.Annotations {

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class EntityAttribute : Attribute {
        public string Name { get; set; } = null;
    }
}
