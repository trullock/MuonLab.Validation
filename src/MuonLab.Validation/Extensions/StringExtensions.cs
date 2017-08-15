using System;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
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
			return self.IsNotNullOrEmpty("Required");
		}

		/// <summary>
		/// Ensure the property is not null or empty
		/// </summary>
		/// <param name="self"></param>
		/// <param name="errorKey">The associated error message</param>
		/// <returns></returns>
		public static ICondition<string> IsNotNullOrEmpty(this string self, string errorKey)
		{
			return self.Satisfies(s => !string.IsNullOrEmpty(s), errorKey);
		}
		
		/// <summary>
		/// Ensure the property has a maximum character length
		/// </summary>
		/// <param name="self"></param>
		/// <param name="maxLength">the maximum character length</param>
		/// <returns></returns>
		public static ICondition<string> HasMaximumLength(this string self, int maxLength)
		{
			return self.HasMaximumLength(maxLength, "MaxLength");
		}

		/// <summary>
		/// Ensure the property has a maximum character length
		/// </summary>
		/// <param name="self"></param>
		/// /// <param name="maxLength">the maximum character length</param>
		/// <param name="errorKey">The associated error message key</param>
		/// <returns></returns>
		public static ICondition<string> HasMaximumLength(this string self, int maxLength, string errorKey)
		{
			return self.Satisfies(s => (s ?? string.Empty).Length <= maxLength, errorKey);
		}

		/// <summary>
		/// Ensure the property has a minimum character length
		/// </summary>
		/// <param name="self"></param>
		/// <param name="minLength">the minimum character length</param>
		/// <returns></returns>
		public static ICondition<string> HasMinimumLength(this string self, int minLength)
		{
			return self.HasMinimumLength(minLength, "MinLength");
		}

		/// <summary>
		/// Ensure the property has a minimum character length
		/// </summary>
		/// <param name="self"></param>
		/// /// <param name="minLength">the minimum character length</param>
		/// <param name="errorKey">The associated error message key</param>
		/// <returns></returns>
		public static ICondition<string> HasMinimumLength(this string self, int minLength, string errorKey)
		{
			return self.Satisfies(s => (s ?? string.Empty).Length >= minLength, errorKey);
		}

		/// <summary>
		/// Ensure the property matches some regex
		/// </summary>
		/// <param name="self"></param>
		/// <param name="regex">The matching regex</param>
		/// <param name="errorKey">The associated error message</param>
		/// <returns></returns>
		public static ICondition<string> Matches(this string self, string regex, string errorKey)
		{
			return self.Matches(regex, RegexOptions.None, errorKey);
		}

		/// <summary>
		/// Ensure the property matches some regex
		/// </summary>
		/// <param name="self"></param>
		/// <param name="regex">The matching regex</param>
		/// <param name="options">The Regex Options</param>
		/// <param name="errorKey">The associated error message</param>
		/// <returns></returns>
		public static ICondition<string> Matches(this string self, string regex, RegexOptions options, string errorKey)
		{
			return self.Satisfies(s => s != null && Regex.Match(s, regex, options).Success, errorKey);
		}

		/// <summary>
		/// Ensure the property matches some regex
		/// </summary>
		/// <param name="self"></param>
		/// <param name="regex">The matching regex</param>
		/// <param name="errorKey">The associated error message</param>
		/// <returns></returns>
		public static ICondition<string> Matches(this string self, Regex regex, string errorKey)
		{
			return self.Satisfies(s => s != null && regex.Match(s).Success, errorKey);
		}

		/// <summary>
		/// Ensure the property matches a value with the given string comparison
		/// </summary>
		/// <param name="self"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static ICondition<string> IsEqualTo(this string self, string value)
		{
			return self.IsEqualTo(value, StringComparison.Ordinal);
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
			return self.Satisfies(s => (s == null && value == null) || (s != null && s.Equals(value, comparison)), "EqualTo");
		}

		/// <summary>
		/// Ensure the property matches a value with the given string comparison
		/// </summary>
		/// <param name="self"></param>
		/// <param name="value">The value to be equal to</param>
		/// <param name="comparison">The string comparison method</param>
		/// <param name="errorKey">The error message</param>
		/// <returns></returns>
		public static ICondition<string> IsEqualTo(this string self, string value, StringComparison comparison, string errorKey)
		{
			return self.Satisfies(s => (s == null && value == null) || (s != null && s.Equals(value, comparison)), errorKey);
		}

		/// <summary>
		/// Ensure the property matches a value with the given string comparison
		/// </summary>
		/// <param name="self"></param>
		/// <param name="value">The value to not be equal to</param>
		/// <param name="comparison">The string comparison method</param>
		/// <param name="errorKey">The error message</param>
		/// <returns></returns>
		public static ICondition<string> IsNotEqualTo(this string self, string value, StringComparison comparison, string errorKey)
		{
			return self.Satisfies(s => (s == null && value != null) || (s != null && !s.Equals(value, comparison)), errorKey);
		}
	}
}