// ReSharper disable once CheckNamespace
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
			return self.IsTrue("BeTrue");
		}

		/// <summary>
		/// Ensure the property is true
		/// </summary>
		/// <param name="self"></param>
		/// <param name="errorKey">The associated error message key</param>
		/// <returns></returns>
		public static ICondition<bool> IsTrue(this bool self, string errorKey)
		{
			return self.Satisfies(x => x, errorKey);
		}

		/// <summary>
		/// Ensure the property is false
		/// </summary>
		/// <param name="self"></param>
		/// <returns></returns>
		public static ICondition<bool> IsFalse(this bool self)
		{
			return self.IsFalse("BeFalse");
		}

		/// <summary>
		/// Ensure the property is false
		/// </summary>
		/// <param name="self"></param>
		/// <param name="errorKey">The associated error message key</param>
		/// <returns></returns>
		public static ICondition<bool> IsFalse(this bool self, string errorKey)
		{
			return self.Satisfies(x => !x, errorKey);
		}
	}
}