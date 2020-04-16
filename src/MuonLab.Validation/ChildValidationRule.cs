using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MuonLab.Validation
{
	sealed class ChildValidationRule<T, TValue> : BaseValidationRule<T, TValue>
	{
		public ChildValidationRule(Expression<Func<T, ICondition<TValue>>> validationExpression) : 
			base(validationExpression)
		{
			this.property = this.Condition.Arguments[0] as MemberExpression;
			this.PropertyExpression = Expression.Lambda<Func<T, TValue>>(this.property, this.FindParameter(this.property));
		}

		public override async Task<IEnumerable<IViolation>> Validate<TOuter>(T entity, Expression<Func<TOuter, T>> prefix)
		{
			// get the property value to be validated
			var value = this.PropertyExpression.Compile().Invoke(entity);

			// get validator from satisfies argumetn
			var lambda = Expression.Lambda(this.Condition.Arguments[1], this.validationExpression.Parameters[0]);
			var validator = lambda.Compile().DynamicInvoke(entity) as IValidator<TValue>;

			ValidationReport report;

			if(prefix != null)
			{
				var nextPrefix = prefix.Combine(this.PropertyExpression, true);
				report = await validator.Validate(value, nextPrefix);
			}
			else
			{
				report = await validator.Validate(value, this.PropertyExpression);
			}

			
			return report.Violations;
		}
	}
}