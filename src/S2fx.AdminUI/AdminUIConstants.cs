using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace S2fx.AdminUI {

    public static class AdminUIConstants {
        public const string DefaultPageName = "index.html";

        public static readonly string AdminUIModuleId = Assembly.GetExecutingAssembly().GetName().Name;
    }
}
