using System;
using System.Text.RegularExpressions;

namespace MuonLab.Validation
{
	public static class StringExtensions
	{
		/// <summary>
		/// Ensure the property is not null or empty
		/// </summary>
		/// <param name="self"></param>
		/// <returns></returns>
		public static ICondition<string> IsNotNullOrEmpty(this string self)
		{
			return self.IsNotNullOrEmpty("{val} is required");
		}

		/// <summary>
		/// Ensure the property is not null or empty
		/// </summary>
		/// <param name="self"></param>
		/// <param name="errorMessage">The associated error message</param>
		/// <returns></returns>
		public static ICondition<string> IsNotNullOrEmpty(this string self, string errorMessage)
		{
			return self.Satisfies(s => !string.IsNullOrEmpty(s), errorMessage);
		}

		/// <summary>
		/// Ensure the property is not null or empty
		/// </summary>
		/// <param name="self"></param>
		/// <returns></returns>
		public static ICondition<string> IsNullOrIsEmpty(this string self)
		{
			return self.IsNullOrIsEmpty("{val} must not have a value");
		}

		/// <summary>
		/// Ensure the property is not null or empty
		/// </summary>
		/// <param name="self"></param>
		/// <param name="errorMessage">The associated error message</param>
		/// <returns></returns>
		public static ICondition<string> IsNullOrIsEmpty(this string self, string errorMessage)
		{
			return self.Satisfies(s => string.IsNullOrEmpty(s), errorMessage);
		}


		/// <summary>
		/// Ensure the property has a maximum character length
		/// </summary>
		/// <param name="self"></param>
		/// <param name="maxLength">the maximum character length</param>
		/// <returns></returns>
		public static ICondition<string> HasMaximumLength(this string self, int maxLength)
		{
			return self.HasMaximumLength(maxLength, "{val} must be at most {arg1} characters");
		}

		/// <summary>
		/// Ensure the property has a maximum character length
		/// </summary>
		/// <param name="self"></param>
		/// /// <param name="maxLength">the maximum character length</param>
		/// <param name="errorMessage">The associated error message</param>
		/// <returns></returns>
		public static ICondition<string> HasMaximumLength(this string self, int maxLength, string errorMessage)
		{
			return self.Satisfies(s => (s ?? string.Empty).Length <= maxLength, errorMessage);
		}

		/// <summary>
		/// Ensure the property has a minimum character length
		/// </summary>
		/// <param name="self"></param>
		/// <param name="minLength">the minimum character length</param>
		/// <returns></returns>
		public static ICondition<string> HasMinimumLength(this string self, int minLength)
		{
			return self.HasMinimumLength(minLength, "{val} must be at least {arg1} characters");
		}

		/// <summary>
		/// Ensure the property has a minimum character length
		/// </summary>
		/// <param name="self"></param>
		/// /// <param name="minLength">the minimum character length</param>
		/// <param name="errorMessage">The associated error message</param>
		/// <returns></returns>
		public static ICondition<string> HasMinimumLength(this string self, int minLength, string errorMessage)
		{
			return self.Satisfies(s => (s ?? string.Empty).Length >= minLength, errorMessage);
		}

		/// <summary>
		/// Ensure the property matches some regex
		/// </summary>
		/// <param name="self"></param>
		/// <param name="regex">The matching regex</param>
		/// <param name="errorMessage">The associated error message</param>
		/// <returns></returns>
		public static ICondition<string> Matches(this string self, string regex, string errorMessage)
		{
			return self.Matches(regex, RegexOptions.None, errorMessage);
		}

		/// <summary>
		/// Ensure the property matches some regex
		/// </summary>
		/// <param name="self"></param>
		/// <param name="regex">The matching regex</param>
		/// <param name="options">The Regex Options</param>
		/// <param name="errorMessage">The associated error message</param>
		/// <returns></returns>
		public static ICondition<string> Matches(this string self, string regex, RegexOptions options, string errorMessage)
		{
			return self.Satisfies(s => s != null ? Regex.Match(s, regex, options).Success : false, errorMessage);
		}

		/// <summary>
		/// Ensure the property matches some regex
		/// </summary>
		/// <param name="self"></param>
		/// <param name="regex">The matching regex</param>
		/// <param name="options">The Regex Options</param>
		/// <param name="errorMessage">The associated error message</param>
		/// <returns></returns>
		public static ICondition<string> Matches(this string self, Regex regex, string errorMessage)
		{
			return self.Satisfies(s => s != null ? regex.Match(s).Success : false, errorMessage);
		}

		/// <summary>
		/// Ensure the property matches a value with the given string comparison
		/// </summary>
		/// <param name="self"></param>
		/// <param name="value"></param>
		/// <param name="comparison"></param>
		/// <returns></returns>
		public static ICondition<string> IsEqualTo(this string self, string value, StringComparison comparison)
		{
			return self.Satisfies(s => s != null ? s.Equals(value, comparison) : false, "{val} must be the same as {arg1}");
		}

		/// <summary>
		/// Ensure the property matches a value with the given string comparison
		/// </summary>
		/// <param name="self"></param>
		/// <param name="value">The value to be equal to</param>
		/// <param name="comparison">The string comparison method</param>
		/// <param name="errorMessage">The error message</param>
		/// <returns></returns>
		public static ICondition<string> IsEqualTo(this string self, string value, StringComparison comparison, string errorMessage)
		{
			return self.Satisfies(s => s != null ? s.Equals(value, comparison) : false, errorMessage);
		}
	}
}