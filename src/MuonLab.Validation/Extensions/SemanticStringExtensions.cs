using System.Text.RegularExpressions;

namespace MuonLab.Validation
{
	public static class SemanticStringExtensions
	{
//		static readonly Regex EmailRegex;
//		static SemanticStringExtensions()
//		{
			// http://www.iamcal.com/publish/articles/php/parsing_email/
//
//			const string qtext = "[^\\x0d\\x22\\x5c\\x80-\\xff]";
//
//			const string dtext = "[^\\x0d\\x5b-\\x5d\\x80-\\xff]";
//
//			const string atom = "[^\\x00-\\x20\\x22\\x28\\x29\\x2c\\x2e\\x3a-\\x3c\\x3e\\x40\\x5b-\\x5d\\x7f-\\xff]+";
//
//			const string quoted_pair = "\\x5c[\\x00-\\x7f]";
//
//			var domain_literal = string.Format("\\x5b({0}|{1})*\\x5d", dtext, quoted_pair);
//
//			var quoted_string = string.Format("\\x22({0}|{1})*\\x22", qtext, quoted_pair);
//
//			const string domain_ref = atom;
//
//			var sub_domain = string.Format("({0}|{1})", domain_ref, domain_literal);
//
//			var word = string.Format("({0}|{1})", atom, quoted_string);
//
//			var domain = string.Format("{0}(\\x2e{0})*", sub_domain);
//
//			var local_part = string.Format("{0}(\\x2e{0})*", word);
//
//			var addr_spec = string.Format("^{0}\\x40{1}$", local_part, domain);
//
//			EmailRegex = new Regex(addr_spec, RegexOptions.IgnoreCase);
//		}

		/// <summary>
		/// Ensure the property is a valid email address
		/// </summary>
		/// <param name="self"></param>
		/// <returns></returns>
		public static ICondition<string> IsAValidEmailAddress(this string self)
		{
			return self.IsAValidEmailAddress("{val} must be a valid email address");
		}

		/// <summary>
		/// Ensure the property is a valid email address
		/// </summary>
		/// <param name="self"></param>
		/// <param name="errorMessage">The associated error message</param>
		/// <returns></returns>
		public static ICondition<string> IsAValidEmailAddress(this string self, string errorMessage)
		{
			return self.Satisfies(s => new EmailValidator().IsEmailValid(self), errorMessage);
		}

		/// <summary>
		/// Ensure the property is a valid BS 7666 PostCode as accoridng to http://en.wikipedia.org/wiki/UK_postcodes
		/// </summary>
		/// <param name="self"></param>
		/// <returns></returns>
		public static ICondition<string> IsAValidBS7666PostCode(this string self)
		{
			return self.IsAValidBS7666PostCode("{val} must be a valid postcode");
		}

		/// <summary>
		/// Ensure the property is a valid BS 7666 PostCode as accoridng to http://en.wikipedia.org/wiki/UK_postcodes
		/// </summary>
		/// <param name="self"></param>
		/// <param name="errorMessage">The associated error message</param>
		/// <returns></returns>
		public static ICondition<string> IsAValidBS7666PostCode(this string self, string errorMessage)
		{
			return self.Matches(@"(GIR 0AA|[A-PR-UWYZ]([0-9]{1,2}|([A-HK-Y][0-9]|[A-HK-Y][0-9]([0-9]|[ABEHMNPRV-Y]))|[0-9][A-HJKS-UW]) ?[0-9][ABD-HJLNP-UW-Z]{2})", RegexOptions.IgnoreCase, errorMessage);
		}
	}
}