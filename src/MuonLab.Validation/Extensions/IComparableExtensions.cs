using System;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
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
			return self.IsGreaterThan(comparison, "GreaterThan");
		}

		/// <summary>
		/// Ensure the property is greater than some value
		/// </summary>
		/// <typeparam name="TValue"></typeparam>
		/// <param name="self"></param>
		/// <param name="comparison">The value to be greater than</param>
		/// <param name="errorKey">The associated error message key</param>
		/// <returns></returns>
		public static ICondition<TValue> IsGreaterThan<TValue>(this TValue self, TValue comparison, string errorKey) where TValue : IComparable
		{
			return self.Satisfies(x => Comparer<TValue>.Default.Compare(x, comparison) > 0, errorKey);
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
			return self.IsGreaterThanOrEqualTo(comparison, "GreaterThanEq");
		}

		/// <summary>
		/// Ensure the property is greater than or equal to some value
		/// </summary>
		/// <typeparam name="TValue"></typeparam>
		/// <param name="self"></param>
		/// <param name="comparison">The value to be greater than or equal to</param>
		/// <param name="errorKey">The associated error message</param>
		/// <returns></returns>
		public static ICondition<TValue> IsGreaterThanOrEqualTo<TValue>(this TValue self, TValue comparison, string errorKey) where TValue : IComparable
		{
			return self.Satisfies(x => Comparer<TValue>.Default.Compare(x, comparison) >= 0, errorKey);
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
			return self.IsEqualTo(comparison, "EqualTo");
		}

		/// <summary>
		/// Ensure the property is equal to some value
		/// </summary>
		/// <typeparam name="TValue"></typeparam>
		/// <param name="self"></param>
		/// <param name="comparison">The value to be equal to</param>
		/// <param name="errorKey">The associated error message key</param>
		/// <returns></returns>
		public static ICondition<TValue> IsEqualTo<TValue>(this TValue self, TValue comparison, string errorKey) where TValue : IComparable
		{
			return self.Satisfies(x => Comparer<TValue>.Default.Compare(x, comparison) == 0, errorKey);
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
			return self.IsNotEqualTo(comparison, "NotEqualTo");
		}

		/// <summary>
		/// Ensure the property is not equal to some value
		/// </summary>
		/// <typeparam name="TValue"></typeparam>
		/// <param name="self"></param>
		/// <param name="comparison">The value to be not equal to</param>
		/// <param name="errorKey">The associated error message key</param>
		/// <returns></returns>
		public static ICondition<TValue> IsNotEqualTo<TValue>(this TValue self, TValue comparison, string errorKey) where TValue : IComparable
		{
			return self.Satisfies(x => Comparer<TValue>.Default.Compare(x, comparison) != 0, errorKey);
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
			return self.IsBetween(lower, upper, "Between");
		}

		/// <summary>
		/// Ensure the property is between some values (inclusive)
		/// </summary>
		/// <typeparam name="TValue"></typeparam>
		/// <param name="self"></param>
		/// <param name="lower">The lower bound to compare to</param>
		/// <param name="upper">The upper bound to compare to</param>
		/// <param name="errorKey">The associated error message key</param>
		/// <returns></returns>
		public static ICondition<TValue> IsBetween<TValue>(this TValue self, TValue lower, TValue upper, string errorKey) where TValue : IComparable
		{
			return self.Satisfies(x => Comparer<TValue>.Default.Compare(x, lower) >= 0 && Comparer<TValue>.Default.Compare(x, upper) <= 0, errorKey);
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
			return self.IsLessThan(comparison, "LessThan");
		}

		/// <summary>
		/// Ensure the property is less than some value
		/// </summary>
		/// <typeparam name="TValue"></typeparam>
		/// <param name="self"></param>
		/// <param name="comparison">The value to be less than</param>
		/// <param name="errorKey">The associated error message key</param>
		/// <returns></returns>
		public static ICondition<TValue> IsLessThan<TValue>(this TValue self, TValue comparison, string errorKey) where TValue : IComparable
		{
			return self.Satisfies(x => Comparer<TValue>.Default.Compare(x, comparison) < 0, errorKey);
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
			return self.IsLessThanOrEqualTo(comparison, "LessThanEq");
		}

		/// <summary>
		/// Ensure the property is less than or equal to some value
		/// </summary>
		/// <typeparam name="TValue"></typeparam>
		/// <param name="self"></param>
		/// <param name="comparison">The value to be less than or equal to</param>
		/// <param name="errorKey">The associated error message key</param>
		/// <returns></returns>
		public static ICondition<TValue> IsLessThanOrEqualTo<TValue>(this TValue self, TValue comparison, string errorKey) where TValue : IComparable
		{
			return self.Satisfies(x => Comparer<TValue>.Default.Compare(x, comparison) <= 0, errorKey);
		}
	}
}