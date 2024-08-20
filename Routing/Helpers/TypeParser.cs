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
		public static Task<Type> GetTypeFromStringValueAsync(string value)
		{
			if (int.TryParse(value, out var iValue))	
				return Task.FromResult(typeof(int));
			if (float.TryParse(value, out var fValue))
				return Task.FromResult(typeof(float));
			if (double.TryParse(value, out var dValue))
				return Task.FromResult(typeof(double));
			if (DateTime.TryParse(value, out var dtValue))
				return Task.FromResult(typeof(DateTime));
			if (Guid.TryParse(value, out var gValue))
				return Task.FromResult(typeof(Guid));

			return Task.FromResult(typeof(string));
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
		public static async Task<string> GetTypeNameFromStringValueAsync(string value)
		{
			var typeName = (await GetTypeFromStringValueAsync(value)).Name;

			if (typeName == null)
				throw new ArgumentException($"{value} is not valid argument");

            return TypeAliases.GetValueOrDefault(typeName) ?? throw new Exception($"{typeName} type doesnt exist in {nameof(TypeAliases)}");
		}


		/// <summary>
		/// Конвертирует строковый параметр в объект
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
        public static Task<object> ConvertFromStringToObjectAsync(string value)
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
