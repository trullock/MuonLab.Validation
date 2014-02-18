using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace MuonLab.Validation
{
	sealed class ConditionalPredicateValidationRule<T> : IValidationRule<T>
	{
		readonly Func<bool> condition;
		readonly IEnumerable<IValidationRule<T>> rules;

		public ConditionalPredicateValidationRule(Func<bool> condition, IEnumerable<IValidationRule<T>> rules)
		{
			this.condition = condition;
			this.rules = rules;
		}

		IEnumerable<IViolation> IValidationRule<T>.Validate<TOuter>(T entity, Expression<Func<TOuter, T>> prefix)
		{
			var violations1 = new List<IViolation>();

			if (condition())
			{
				foreach (var crule in this.rules)
					violations1.AddRange(crule.Validate(entity, prefix));
			}

			return violations1;
		}
	}
}