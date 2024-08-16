using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace STCTesting.Routing.Helpers
{
    public class RouteParser
    {
        public static Dictionary<string, string> GetRegistratorArguments(string path)
        {
            var arguments = new Dictionary<string, string>();

            var matches = Regex.Matches(path, "{\\w*:\\w*}");

            if (matches == null)
            {
                return arguments;
            }

            foreach (Match match in matches)
            {
                if (!match.Success)
                    continue;

                var argument = match.Value.Split(":");
                //Наименование переменной, значение
                arguments.Add(argument[0], argument[1]);
            }

            return arguments;
        }
    }


}
    