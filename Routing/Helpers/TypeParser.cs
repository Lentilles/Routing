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

		/// <summary>
		/// Может получить тип из строкового значения
		/// Принимаемые шаблоны значений в строке те же, что и в TryParse этого типа.
		/// </summary>
		/// <param name="value"></param>
		/// <returns>Один из типов: 
		/// <see cref="int"/>, 
		/// <see cref="float"/>,
		/// <see cref="double"/>,
		/// <see cref="DateTime"/>,
		/// <see cref="Guid"/>,
		/// <see cref="string"/>,
		/// </returns>
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


		/// <summary>
		/// Может получить название типа из строкового значения
		/// Принимаемые шаблоны значений в строке те же, что и в TryParse этого типа.
		/// </summary>
		/// <param name="value"></param>
		/// <returns>Один из типов: 
		/// <see cref="int"/>, 
		/// <see cref="float"/>,
		/// <see cref="double"/>,
		/// <see cref="DateTime"/>,
		/// <see cref="Guid"/>,
		/// <see cref="string"/>,
		/// </returns>
		public static string GetTypeNameFromStringValue(string value)
		{
			return TypeAliases.GetValueOrDefault(GetTypeFromStringValue(value).Name);
		}


		/// <summary>
		/// Конвертирует строковый параметр в объект
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
        public static Task<object> ConvertFromStringToObject(string value)
        {
            if (int.TryParse(value, out var iValue))
                return Task.FromResult<object>(iValue);
            if (float.TryParse(value, out var fValue))
                return Task.FromResult<object>(fValue);
            if (double.TryParse(value, out var dValue))
                return Task.FromResult<object>(dValue);
            if (DateTime.TryParse(value, out var dtValue))
                return Task.FromResult<object>(dtValue);
            if (Guid.TryParse(value, out var gValue))
                return Task.FromResult<object>(gValue);

            return Task.FromResult<object>(value);
        }
    }
}
