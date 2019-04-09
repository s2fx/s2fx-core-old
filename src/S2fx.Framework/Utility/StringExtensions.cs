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
            var sb = new StringBuilder(input.Length + 8);

            int lastChar = -1;
            foreach (var c in input) {
                if (char.IsUpper(c)) {
                    if (lastChar >= 0 && lastChar != '_') {
                        sb.Append('_');
                    }
                    sb.Append(char.ToLowerInvariant(c));
                }
                else {
                    sb.Append(c);
                }

                lastChar = c;
            }
            return sb.ToString();
        }

    }
}
