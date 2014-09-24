using System.Text.RegularExpressions;

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

		/// <summary>
		/// Ensure the property is a valid BS 7666 PostCode as accoridng to http://en.wikipedia.org/wiki/UK_postcodes
		/// </summary>
		/// <param name="self"></param>
		/// <returns></returns>
		public static ICondition<string> IsAValidBS7666PostCode(this string self)
		{
			return self.IsAValidBS7666PostCode("ValidPostcode");
		}

		/// <summary>
		/// Ensure the property is a valid BS 7666 PostCode as accoridng to http://en.wikipedia.org/wiki/UK_postcodes
		/// </summary>
		/// <param name="self"></param>
		/// <param name="errorKey">The associated error message key</param>
		/// <returns></returns>
		public static ICondition<string> IsAValidBS7666PostCode(this string self, string errorKey)
		{
			return self.Matches(@"(GIR 0AA|[A-PR-UWYZ]([0-9]{1,2}|([A-HK-Y][0-9]|[A-HK-Y][0-9]([0-9]|[ABEHMNPRV-Y]))|[0-9][A-HJKS-UW]) ?[0-9][ABD-HJLNP-UW-Z]{2})", RegexOptions.IgnoreCase, errorKey);
		}
	}
}