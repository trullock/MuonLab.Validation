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
			return new PropertyCondition<TValue>(condition, errorKey);
		}

		public static ICondition<TValue> Satisfies<TValue>(this TValue self, Func<TValue, Task<bool>> condition, string errorKey)
		{
			return new PropertyCondition<TValue>(value => Task.Run(async () => await condition(value)).Result, errorKey);
		}

		public static ChildValidationCondition<TValue> Satisfies<TValue>(this TValue self, IValidator<TValue> validator)
		{
			return new ChildValidationCondition<TValue>(validator);
		}

		public static ChildListValidationCondition<TValue> AllSatisfy<TValue>(this IList<TValue> self, IValidator<TValue> validator)
		{
			return new ChildListValidationCondition<TValue>(validator);
		}
	}
}