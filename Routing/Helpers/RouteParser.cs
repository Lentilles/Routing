using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Routing.Helpers
{
    public class RouteParser
    {
        /// <summary>
        /// Находит определение динамических сегментов из шаблона маршрута
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
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

		/// <summary>
		/// Находит статическую часть из маршрута
		/// </summary>
		/// <param name="route"></param>
		/// <returns></returns>
		public static string GetStaticPartFromRouteTemplate(string route)
		{
			var dynamicSymbolIndex = route.IndexOf('{');
			if (dynamicSymbolIndex < 1)
			{
				throw new ArgumentException($"{route} has bad template");
			}

			return route.Remove(dynamicSymbolIndex);
		}



        public static IEnumerable<string> GetArgumentsFromRoute(string route)
        {
            var arguments = new List<string>();

            return arguments;    
        }
	}


}
