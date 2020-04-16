using System;
using System.Collections.Generic;
using System.Threading.Tasks;

// ReSharper disable once CheckNamespace
namespace MuonLab.Validation
{
	public static class SatisfiesExtension
	{
		public static ICondition<TValue> Satisfies<TValue>(this TValue self, Func<TValue, bool> condition, string errorKey)
		{
			return new PropertyCondition<TValue>(v => Task.FromResult(condition(v)), errorKey);
		}

		public static ICondition<TValue> Satisfies<TValue>(this TValue self, Func<TValue, Task<bool>> condition, string errorKey)
		{
			return new PropertyCondition<TValue>(condition, errorKey);
		}

		public static ICondition<TValue> Satisfies<TValue>(this TValue self, Func<TValue, ConditionResult> condition)
		{
			return new PropertyCondition<TValue>(v => Task.FromResult(condition(v)));
		}

		public static ICondition<TValue> Satisfies<TValue>(this TValue self, Func<TValue, Task<ConditionResult>> condition)
		{
			return new PropertyCondition<TValue>(condition);
		}

		public static ChildValidationCondition<TValue> Satisfies<TValue>(this TValue self, IValidator<TValue> validator)
		{
			return new ChildValidationCondition<TValue>(validator);
		}

		/// <summary>
		/// Ensures all the values satisfy the given validator
		/// </summary>
		/// <typeparam name="TValue"></typeparam>
		/// <param name="self"></param>
		/// <param name="validator"></param>
		/// <param name="ignoreDefaultValues"></param>
		/// <returns></returns>
		public static ChildListValidationCondition<TValue> AllSatisfy<TValue>(this IList<TValue> self, IValidator<TValue> validator)
		{
			return new ChildListValidationCondition<TValue>(validator, false);
		}

		/// <summary>
		/// Ensures all* the values satisfy the given validator
		/// * If ignoreDefaultValues is true, then any value which is the default of its type will be ignored. Useful for ignoring nulls in partially populated ILists
		/// </summary>
		/// <typeparam name="TValue"></typeparam>
		/// <param name="self"></param>
		/// <param name="validator"></param>
		/// <param name="ignoreDefaultValues"></param>
		/// <returns></returns>
		public static ChildListValidationCondition<TValue> AllSatisfy<TValue>(this IList<TValue> self, IValidator<TValue> validator, bool ignoreDefaultValues)
		{
			return new ChildListValidationCondition<TValue>(validator, ignoreDefaultValues);
		}
	}
}