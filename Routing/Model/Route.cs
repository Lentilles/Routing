using Routing.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Routing.Model
{
	public class Route
	{
		public readonly Delegate Method;
		/// <summary>
		///     ArgName - Type
		/// </summary>
		public readonly Dictionary<string, string> ArgumentsInRoute;
		public readonly Dictionary<string, string> ArgumentsInDelegate;
		public readonly string StaticPath;


		public Route(string route, Delegate method)
		{
			Method = method;
			StaticPath = RouteParser.GetStaticPartFromRouteTemplate(route);
			ArgumentsInRoute = RouteParser.GetRegistratorArguments(route);
			ArgumentsInDelegate = GetParameterNamesFromDelegate(method.Method.GetParameters());
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
