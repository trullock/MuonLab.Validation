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
			return self.IsNotNull("{val} is required");
		}

		/// <summary>
		/// Ensure the property is not null
		/// </summary>
		/// <typeparam name="TValue"></typeparam>
		/// <param name="self"></param>
		/// <param name="errorMessage">The associated error message</param>
		/// <returns></returns>
		public static ICondition<TValue> IsNotNull<TValue>(this TValue self, string errorMessage) where TValue : class
		{
			return self.Satisfies(x => x != null, errorMessage);
		}


		/// <summary>
		/// Ensure the property is null
		/// </summary>
		/// <typeparam name="TValue"></typeparam>
		/// <param name="self"></param>
		/// <returns></returns>
		public static ICondition<TValue> IsNull<TValue>(this TValue self) where TValue : class
		{
			return self.IsNull("{val} must be null");
		}

		/// <summary>
		/// Ensure the property is null
		/// </summary>
		/// <typeparam name="TValue"></typeparam>
		/// <param name="self"></param>
		/// <param name="errorMessage">The associated error message</param>
		/// <returns></returns>
		public static ICondition<TValue> IsNull<TValue>(this TValue self, string errorMessage) where TValue : class
		{
			return self.Satisfies(x => x == null, errorMessage);
		}
	}
}