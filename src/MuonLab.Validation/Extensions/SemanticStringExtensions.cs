// ReSharper disable once CheckNamespace
namespace MuonLab.Validation
{
	public static class SemanticStringExtensions
	{
		/// <summary>
		/// Ensure the property is a valid email address
		/// </summary>
		/// <param name="self"></param>
		/// <returns></returns>
		public static ICondition<string> IsAValidEmailAddress(this string self)
		{
			return self.IsAValidEmailAddress("ValidEmail");
		}

		/// <summary>
		/// Ensure the property is a valid email address
		/// </summary>
		/// <param name="self"></param>
		/// <param name="errorKey">The associated error message key</param>
		/// <returns></returns>
		public static ICondition<string> IsAValidEmailAddress(this string self, string errorKey)
		{
			return self.Satisfies(s => new EmailValidator().IsEmailValid(self), errorKey);
		}
	}
}