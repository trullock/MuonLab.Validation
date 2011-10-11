using System;

namespace MuonLab.Validation
{
	public static class NullableExtensions
	{
		/// <summary>
		/// Ensure the property has a value
		/// </summary>
		/// <param name="self"></param>
		/// <returns></returns>
		public static ICondition<T?> HasValue<T>(this T? self) where T : struct
		{
			return self.HasValue("{val} must have a value");
		}

		/// <summary>
		/// Ensure the property has a value
		/// </summary>
		/// <param name="self"></param>
		/// <param name="errorMessage"></param>
		/// <returns></returns>
		public static ICondition<T?> HasValue<T>(this T? self, string errorMessage) where T : struct
		{
			return self.Satisfies(x => x.HasValue, errorMessage);
		}

		/// <summary>
		/// Ensure the property does not have a value
		/// </summary>
		/// <param name="self"></param>
		/// <returns></returns>
		public static ICondition<T?> DoesNotHaveValue<T>(this T? self) where T : struct
		{
			return self.DoesNotHaveValue("{val} must not have a value");
		}

		/// <summary>
		/// Ensure the property does not have a value
		/// </summary>
		/// <param name="self"></param>
		/// <param name="errorMessage"></param>
		/// <returns></returns>
		public static ICondition<T?> DoesNotHaveValue<T>(this T? self, string errorMessage) where T : struct
		{
			return self.Satisfies(x => !x.HasValue, errorMessage);
		}
	}
}