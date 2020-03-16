using System;
using System.Linq;

namespace MuonLab.Validation.Extensions
{
	public static class EnumExtensions
	{
		/// <summary>
		/// Ensures the value is a valid (declared) Enum value
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="self"></param>
		/// <returns></returns>
		public static ICondition<T> IsAValidEnumValue<T>(this T self) where T : Enum
		{
			return self.IsAValidEnumValue("Invalid");
		}

		/// <summary>
		/// Ensures the value is a valid (declared) Enum value
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="self"></param>
		/// <param name="errorKey"></param>
		/// <returns></returns>
		public static ICondition<T> IsAValidEnumValue<T>(this T self, string errorKey) where T : Enum
		{
			return self.Satisfies(e => Enum.GetValues(self.GetType()).Cast<Enum>().Contains(self), errorKey);
		}
	}
}