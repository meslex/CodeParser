using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Text;
using System.Threading.Tasks;

namespace CodeParse
{
    class RegularExpressionCreator
    {
        public string CreateVariableIdentifier(string pattern)
        {
            string[] steps = { @"(?<sym>[$&*<>()[\]])", @"\${sym}", @"(?<exp>\w+(\|\w+)+)", "(${exp})", "(placeholder|dataType)", "\\w+", "name", @"[a-z]\w+" };
            for (int i = 0; i < steps.Length; i += 2)
            {
                pattern = Regex.Replace(pattern, steps[i], steps[i + 1]);
            }
            pattern += @"(;|\s=)";

            return pattern;
        }

        public string CreateClassIdentifier(string pattern)
        {
            string[] steps = { @"(?<sym>[$&*<>()[\]])", @"\${sym}", @"(?<exp>\w+(\|\w+)+)", "(${exp})", "(placeholder|dataType)", "\\w+", "name", @"[a-z]\w+" };
            for (int i = 0; i < steps.Length; i += 2)
            {
                pattern = Regex.Replace(pattern, steps[i], steps[i + 1]);
            }

            return pattern;
        }

        public string CreateMethodIdentifier(string pattern)
        {
            string[] steps = { @"(?<sym>[<>()[\]])", @"\${sym}", @"(?<exp>\w+(\|\w+)+)", "(${exp})",
                "Name", @"[A-Z]\w+", "name", @"[a-z]\w+", "(placeholder|dataType)", "\\w+", "args", ".*" };
            for (int i = 0; i < steps.Length; i += 2)
            {
                pattern = Regex.Replace(pattern, steps[i], steps[i + 1]);
            }

            return pattern;
        }

        public static string CreateRegularExpression(string pattern, string type)
        {
            string[] steps = { @"(?<sym>[$&*<>()[\]])", @"\${sym}", @"(?<exp>\w+(\|\w+)+)", "(${exp})",
                "Name", @"[A-Z]\w+", "name", @"[a-z]\w+", "dataType", @"(\w+|\w+(\.\w+)*)",  "placeholder", "\\w+", "args", ".*" };
            for (int i = 0; i < steps.Length; i += 2)
            {
                pattern = Regex.Replace(pattern, steps[i], steps[i + 1]);
            }
            if(type == "Variables")
            {
                pattern += @"(;|\s=)";
            }

            return pattern;

        }
    }
}
