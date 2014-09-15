using System;
using System.Collections.Generic;
using System.Linq.Expressions;

// ReSharper disable once CheckNamespace
namespace MuonLab.Validation
{
	public static class SatisfiesExtension
	{
		public static ICondition<TValue> Satisfies<TValue>(this TValue self, Expression<Func<TValue, bool>> condition, string errorMessage)
		{
			return new PropertyCondition<TValue>(condition.Compile(), errorMessage);
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