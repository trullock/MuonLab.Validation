namespace MuonLab.Validation
{
	public static class BooleanExtensions
	{
		/// <summary>
		/// Ensure the property is true
		/// </summary>
		/// <param name="self"></param>
		/// <returns></returns>
		public static ICondition<bool> IsTrue(this bool self)
		{
			return self.IsTrue("{val} must be true");
		}

		/// <summary>
		/// Ensure the property is true
		/// </summary>
		/// <param name="self"></param>
		/// <param name="errorMessage">The associated error message</param>
		/// <returns></returns>
		public static ICondition<bool> IsTrue(this bool self, string errorMessage)
		{
			return self.Satisfies(x => x, errorMessage);
		}

		/// <summary>
		/// Ensure the property is false
		/// </summary>
		/// <param name="self"></param>
		/// <returns></returns>
		public static ICondition<bool> IsFalse(this bool self)
		{
			return self.IsFalse("{val} must be false");
		}

		/// <summary>
		/// Ensure the property is false
		/// </summary>
		/// <param name="self"></param>
		/// <param name="errorMessage">The associated error message</param>
		/// <returns></returns>
		public static ICondition<bool> IsFalse(this bool self, string errorMessage)
		{
			return self.Satisfies(x => !x, errorMessage);
		}
	}
}