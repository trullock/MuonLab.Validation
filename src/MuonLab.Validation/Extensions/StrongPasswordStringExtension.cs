using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace MuonLab.Validation
{
	public static class StrongPasswordStringExtension
	{
		/// <summary>
		/// Ensure the property is a strong password based on the provided criteria
		/// </summary>
		/// <param name="self"></param>
		/// <param name="minimumLength"></param>
		/// <param name="requireCaseVariation"></param>
		/// <param name="requireNumeric"></param>
		/// <param name="requireNonAlphanumeric"></param>
		/// <returns></returns>
		public static ICondition<string> IsAStrongPassword(this string self, int minimumLength, bool requireCaseVariation, bool requireNumeric, bool requireNonAlphanumeric)
		{
			return new StrongPasswordCondition(minimumLength, requireCaseVariation, requireNumeric, requireNonAlphanumeric);
		}

		private class StrongPasswordCondition : PropertyCondition<string>
		{
			public StrongPasswordCondition(int minimumLength, bool requireCaseVariation, bool requireNumeric, bool requireNonAlphanumeric) : 
				base(null, null)
			{
				this.Condition = s =>
					{
						if (s != null)
						{
							// Check length
							if (s.Length < minimumLength)
							{
								this.ErrorMessage = GetErrorMessage(minimumLength, requireCaseVariation, requireNumeric, requireNonAlphanumeric);
								return false;
							}

							// check case variation
							if (requireCaseVariation && !ValidateRegex(s, @"(?=.*[a-z])(?=.*[A-Z])"))
							{
								this.ErrorMessage = GetErrorMessage(minimumLength, requireCaseVariation, requireNumeric, requireNonAlphanumeric);
								return false;
							}
							else if (!requireCaseVariation && !ValidateRegex(s, @"(?=.*[a-zA-Z])"))
							{
								this.ErrorMessage = GetErrorMessage(minimumLength, requireCaseVariation, requireNumeric, requireNonAlphanumeric);
								return false;
							}

							// check numerics
							if (requireNumeric && !ValidateRegex(s, @"(?=.*[0-9])"))
							{
								this.ErrorMessage = GetErrorMessage(minimumLength, requireCaseVariation, requireNumeric, requireNonAlphanumeric);
								return false;
							}

							// check presence of non alphanumeric
							if (requireNonAlphanumeric && !ValidateRegex(s, @"(?=.*[^0-9a-zA-Z])"))
							{
								this.ErrorMessage = GetErrorMessage(minimumLength, requireCaseVariation, requireNumeric, requireNonAlphanumeric);
								return false;
							}
						}

						return true;
					};
			}

			static string GetErrorMessage(int minimumLength, bool requireCaseVariation, bool requireNumeric, bool requireNonAlphanumeric)
			{
				IList<string> errors = new List<string>();
				
				errors.Add("{val} must be at least " + minimumLength + " characters");

				// check case variation
				errors.Add(requireCaseVariation ? "contain both upper and lower case characters" : "contain at least 1 letter");

				// check numerics
				if (requireNumeric)
					errors.Add("contain at least 1 number");

				// check presence of non alphanumeric
				if (requireNonAlphanumeric)
					errors.Add("contain at least 1 non-alphanumeric character");

				string errorMessage = errors[0];

				for(int i = 1; i < errors.Count; i++)
				{
					if (i < errors.Count - 1)
						errorMessage += ", " + errors[i];
					else
						errorMessage += " and " + errors[i];
				}

				return errorMessage;
			}

			static bool ValidateRegex(string input, string pattern)
			{
				return Regex.Match(input, pattern).Success;
			}
		}
	}
}