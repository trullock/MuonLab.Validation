// ReSharper disable once CheckNamespace
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
			return self.HasValue("Required");
		}

		/// <summary>
		/// Ensure the property has a value
		/// </summary>
		/// <param name="self"></param>
		/// <param name="errorKey"></param>
		/// <returns></returns>
		public static ICondition<T?> HasValue<T>(this T? self, string errorKey) where T : struct
		{
			return self.Satisfies(x => x.HasValue, errorKey);
		}
	}
}