using System;
using System.Collections;

namespace MuonLab.Validation
{
	public static class IComparableExtensions
	{
		/// <summary>
		/// Ensure the property is greater than some value
		/// </summary>
		/// <typeparam name="TValue"></typeparam>
		/// <param name="self"></param>
		/// <param name="comparison">The value to be greater than</param>
		/// <returns></returns>
		public static ICondition<TValue> IsGreaterThan<TValue>(this TValue self, TValue comparison) where TValue : IComparable
		{
			return self.IsGreaterThan(comparison, "{val} must be greater than {arg1}");
		}

		/// <summary>
		/// Ensure the property is greater than some value
		/// </summary>
		/// <typeparam name="TValue"></typeparam>
		/// <param name="self"></param>
		/// <param name="comparison">The value to be greater than</param>
		/// <param name="errorMessage">The associated error message</param>
		/// <returns></returns>
		public static ICondition<TValue> IsGreaterThan<TValue>(this TValue self, TValue comparison, string errorMessage) where TValue : IComparable
		{
			return self.Satisfies(x => Comparer.Default.Compare(x, comparison) > 0, errorMessage);
		}

		/// <summary>
		/// Ensure the property is greater than or equal to some value
		/// </summary>
		/// <typeparam name="TValue"></typeparam>
		/// <param name="self"></param>
		/// <param name="comparison">The value to be greater than or equal to</param>
		/// <returns></returns>
		public static ICondition<TValue> IsGreaterThanOrEqualTo<TValue>(this TValue self, TValue comparison) where TValue : IComparable
		{
			return self.IsGreaterThanOrEqualTo(comparison, "{val} must be greater than or equal to {arg1}");
		}

		/// <summary>
		/// Ensure the property is greater than or equal to some value
		/// </summary>
		/// <typeparam name="TValue"></typeparam>
		/// <param name="self"></param>
		/// <param name="comparison">The value to be greater than or equal to</param>
		/// <param name="errorMessage">The associated error message</param>
		/// <returns></returns>
		public static ICondition<TValue> IsGreaterThanOrEqualTo<TValue>(this TValue self, TValue comparison, string errorMessage) where TValue : IComparable
		{
			return self.Satisfies(x => Comparer.Default.Compare(x, comparison) >= 0, errorMessage);
		}

		/// <summary>
		/// Ensure the property is equal to some value
		/// </summary>
		/// <typeparam name="TValue"></typeparam>
		/// <param name="self"></param>
		/// <param name="comparison">The value to be equal to</param>
		/// <returns></returns>
		public static ICondition<TValue> IsEqualTo<TValue>(this TValue self, TValue comparison) where TValue : IComparable
		{
			return self.IsEqualTo(comparison, "{val} must be the same as {arg1}");
		}

		/// <summary>
		/// Ensure the property is equal to some value
		/// </summary>
		/// <typeparam name="TValue"></typeparam>
		/// <param name="self"></param>
		/// <param name="comparison">The value to be equal to</param>
		/// <param name="errorMessage">The associated error message</param>
		/// <returns></returns>
		public static ICondition<TValue> IsEqualTo<TValue>(this TValue self, TValue comparison, string errorMessage) where TValue : IComparable
		{
			return self.Satisfies(x => Comparer.Default.Compare(x, comparison) == 0, errorMessage);
		}

		/// <summary>
		/// Ensure the property is not equal to some value
		/// </summary>
		/// <typeparam name="TValue"></typeparam>
		/// <param name="self"></param>
		/// <param name="comparison">The value to be not equal to</param>
		/// <returns></returns>
		public static ICondition<TValue> IsNotEqualTo<TValue>(this TValue self, TValue comparison) where TValue : IComparable
		{
			return self.IsNotEqualTo(comparison, "{val} must not be the same as {arg1}");
		}

		/// <summary>
		/// Ensure the property is not equal to some value
		/// </summary>
		/// <typeparam name="TValue"></typeparam>
		/// <param name="self"></param>
		/// <param name="comparison">The value to be not equal to</param>
		/// <param name="errorMessage">The associated error message</param>
		/// <returns></returns>
		public static ICondition<TValue> IsNotEqualTo<TValue>(this TValue self, TValue comparison, string errorMessage) where TValue : IComparable
		{
			return self.Satisfies(x => Comparer.Default.Compare(x, comparison) != 0, errorMessage);
		}


		/// <summary>
		/// Ensure the property is between some values (inclusive)
		/// </summary>
		/// <typeparam name="TValue"></typeparam>
		/// <param name="self"></param>
		/// <param name="lower">The lower bound to compare to</param>
		/// <param name="upper">The upper bound to compare to</param>
		/// <returns></returns>
		public static ICondition<TValue> IsBetween<TValue>(this TValue self, TValue lower, TValue upper) where TValue : IComparable
		{
			return self.IsBetween(lower, upper, "{val} must be between {arg1} and {arg2}");
		}

		/// <summary>
		/// Ensure the property is between some values (inclusive)
		/// </summary>
		/// <typeparam name="TValue"></typeparam>
		/// <param name="self"></param>
		/// <param name="lower">The lower bound to compare to</param>
		/// <param name="upper">The upper bound to compare to</param>
		/// <param name="errorMessage">The associated error message</param>
		/// <returns></returns>
		public static ICondition<TValue> IsBetween<TValue>(this TValue self, TValue lower, TValue upper, string errorMessage) where TValue : IComparable
		{
			return self.Satisfies(x => Comparer.Default.Compare(x, lower) >= 0 && Comparer.Default.Compare(x, upper) <= 0, errorMessage);
		}


		/// <summary>
		/// Ensure the property is less than some value
		/// </summary>
		/// <typeparam name="TValue"></typeparam>
		/// <param name="self"></param>
		/// <param name="comparison">The value to be less than</param>
		/// <returns></returns>
		public static ICondition<TValue> IsLessThan<TValue>(this TValue self, TValue comparison) where TValue : IComparable
		{
			return self.IsLessThan(comparison, "{val} must be less than {arg1}");
		}

		/// <summary>
		/// Ensure the property is less than some value
		/// </summary>
		/// <typeparam name="TValue"></typeparam>
		/// <param name="self"></param>
		/// <param name="comparison">The value to be less than</param>
		/// <param name="errorMessage">The associated error message</param>
		/// <returns></returns>
		public static ICondition<TValue> IsLessThan<TValue>(this TValue self, TValue comparison, string errorMessage) where TValue : IComparable
		{
			return self.Satisfies(x => Comparer.Default.Compare(x, comparison) < 0, errorMessage);
		}

		/// <summary>
		/// Ensure the property is less than or equal to some value
		/// </summary>
		/// <typeparam name="TValue"></typeparam>
		/// <param name="self"></param>
		/// <param name="comparison">The value to be less than or equal to</param>
		/// <returns></returns>
		public static ICondition<TValue> IsLessThanOrEqualTo<TValue>(this TValue self, TValue comparison) where TValue : IComparable
		{
			return self.IsLessThanOrEqualTo(comparison, "{val} must be less than or equal to {arg1}");
		}

		/// <summary>
		/// Ensure the property is less than or equal to some value
		/// </summary>
		/// <typeparam name="TValue"></typeparam>
		/// <param name="self"></param>
		/// <param name="comparison">The value to be less than or equal to</param>
		/// <param name="errorMessage">The associated error message</param>
		/// <returns></returns>
		public static ICondition<TValue> IsLessThanOrEqualTo<TValue>(this TValue self, TValue comparison, string errorMessage) where TValue : IComparable
		{
			return self.Satisfies(x => Comparer.Default.Compare(x, comparison) <= 0, errorMessage);
		}
	}
}