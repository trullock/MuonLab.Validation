using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace MuonLab.Validation
{
	internal sealed class AnyValidationRule<T> : IValidationRule<T>
	{
		readonly IEnumerable<IValidationRule<T>> rules;

		public AnyValidationRule(IEnumerable<IValidationRule<T>> rules)
		{
			this.rules = rules;
		}

		public IEnumerable<IViolation> Validate<TOuter>(T entity, Expression<Func<TOuter, T>> prefix)
		{
			var violations = new List<IViolation>();

			foreach (var rule in this.rules)
			{
				var ruleViolations = rule.Validate(entity, prefix);
				violations.AddRange(ruleViolations);

				if(!ruleViolations.Any())
					return new IViolation[0];
			}

			return violations;
		}
	}
}