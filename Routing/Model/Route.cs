using Routing.Helpers;
using System.Reflection;

namespace Routing.Model
{
	public class Route
	{
		public readonly Delegate Method;
		/// <summary>
		///     ArgName - Type
		/// </summary>
		public readonly Dictionary<string, string> ArgumentsInRoute;
        /// <summary>
        ///     ArgName - Type
        /// </summary>
        public readonly Dictionary<string, string> ArgumentsInDelegate;
		public readonly string StaticPath;

		public readonly bool ArgumentNameMatch = true;

		public Route(string route, Delegate method)
		{
			Method = method;
			StaticPath = RouteParser.GetStaticPartFromRouteTemplate(route);
			ArgumentsInRoute = RouteParser.GetRegistratorArguments(route);
			ArgumentsInDelegate = GetParameterNamesFromDelegate(method.Method.GetParameters());

			foreach (var argument in ArgumentsInRoute)
			{
				if (!ArgumentsInDelegate.ContainsKey(argument.Key))
				{
					ArgumentNameMatch = false;
				}
			}
		}

		private Dictionary<string, string> GetParameterNamesFromDelegate(ParameterInfo[] parameters)
		{
			var result = new Dictionary<string, string>();
			foreach (var parameter in parameters)
			{
				if(parameter.Name != null)
					result.Add(parameter.Name, parameter.ParameterType.Name);
			}

			return result;
		}
	}
}
