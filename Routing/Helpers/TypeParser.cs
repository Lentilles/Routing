using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Routing.Helpers
{
	public class TypeParser
	{
		public static readonly Dictionary<string, string> TypeAliases = new Dictionary<string, string>()
		{
			{typeof(int).Name, "int"},
			{typeof(float).Name, "float"},
			{typeof(double).Name, "double"},
			{typeof(DateTime).Name, "DateTime"},
			{typeof(Guid).Name, "Guid"},
			{typeof(string).Name, "string"}
		};


		public static Type GetTypeFromStringValue(string value)
		{
			if (int.TryParse(value, out var iValue))	
				return typeof(int);
			if (float.TryParse(value, out var fValue))
				return typeof(float);
			if (double.TryParse(value, out var dValue))
				return typeof(double);
			if (DateTime.TryParse(value, out var dtValue))
				return typeof(DateTime);
			if (Guid.TryParse(value, out var gValue))
				return typeof(Guid);

			return typeof(string);
		}

		public static string GetTypeNameFromStringValue(string value)
		{
			return TypeAliases.GetValueOrDefault(GetTypeFromStringValue(value).Name);
		}
	}
}
