// ReSharper disable once CheckNamespace
namespace MuonLab.Validation
{
	public static class ClassExtensions
	{
		/// <summary>
		/// Ensure the property is not null
		/// </summary>
		/// <typeparam name="TValue"></typeparam>
		/// <param name="self"></param>
		/// <returns></returns>
		public static ICondition<TValue> IsNotNull<TValue>(this TValue self) where TValue : class
		{
			return self.IsNotNull("Required");
		}

		/// <summary>
		/// Ensure the property is not null
		/// </summary>
		/// <typeparam name="TValue"></typeparam>
		/// <param name="self"></param>
		/// <param name="errorKey">The associated error message key</param>
		/// <returns></returns>
		public static ICondition<TValue> IsNotNull<TValue>(this TValue self, string errorKey) where TValue : class
		{
			return self.Satisfies(x => x != null, errorKey);
		}
	}
}