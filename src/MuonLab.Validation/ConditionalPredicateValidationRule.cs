using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

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

		async Task<IEnumerable<IViolation>> IValidationRule<T>.Validate<TOuter>(T entity, Expression<Func<TOuter, T>> prefix)
		{
			var violations1 = new List<IViolation>();

			if (condition())
			{
				foreach (var crule in this.rules)
					violations1.AddRange(await crule.Validate(entity, prefix));
			}

			return violations1;
		}
	}
}