using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace S2fx.Utility {

    public static class StringExtensions {

        public static string ToSnakeCase(this string input) {
            if (string.IsNullOrEmpty(input)) {
                return input;
            }
            return string.Concat(input.Select((x, i) => i > 0 && char.IsUpper(x) ? "_" + x.ToString() : x.ToString()))
                .ToLowerInvariant();
        }

    }
}
