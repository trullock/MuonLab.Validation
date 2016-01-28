using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace MuonLab.Validation
{
	internal sealed class ChildListValidationRule<T, TValue> : IValidationRule<T>
	{
		protected MemberExpression property;
		protected readonly Expression<Func<T, ICondition<IList<TValue>>>> validationExpression;

		public MethodCallExpression Condition { get; protected set; }
		public Expression<Func<T, IList<TValue>>> PropertyExpression { get; protected set; }

		public ParameterExpression findParameter(Expression expression)
		{
			while (!(expression is ParameterExpression))
			{
				if (expression is MemberExpression)
					expression = (expression as MemberExpression).Expression;
				else
					// TODO: what cases are here?
					throw new NotSupportedException("Expression Type `" + expression.GetType() + "` is not supported. Do you have a validation condition not of the form `x => x.someproperty...`?");
			}

			return expression as ParameterExpression;
		}

		public ChildListValidationRule(Expression<Func<T, ICondition<IList<TValue>>>> validationExpression)
		{
			this.validationExpression = validationExpression;
			this.Condition = validationExpression.Body as MethodCallExpression;

			this.property = this.Condition.Arguments[0] as MemberExpression;
			this.PropertyExpression = Expression.Lambda<Func<T, IList<TValue>>>(this.property, findParameter(this.property));	
		}

		public IEnumerable<IViolation> Validate<TOuter>(T entity, Expression<Func<TOuter, T>> prefix)
		{
			// get the property value to be validated
			var value = this.PropertyExpression.Compile().Invoke(entity);

			// get validator from satisfies argumetn
			var lambda = Expression.Lambda(this.Condition.Arguments[1], this.validationExpression.Parameters[0]);
			var validator = lambda.Compile().DynamicInvoke(entity) as IValidator<TValue>;

			var list = value as IList;

			var violations = new List<IViolation>();

			for(var i = 0; i < list.Count; i++)
			{
				var j = i;

				ValidationReport report;

				if (prefix != null)
				{
					var indexer = prefix.Combine(this.PropertyExpression.Combine(xs => xs[j], true), true);
					report = validator.Validate(value[i], indexer);
				}
				else
				{
					var indexer = this.PropertyExpression.Combine(xs => xs[j], true);
					report = validator.Validate(value[i], indexer);
				}
				
				violations.AddRange(report.Violations);
			}

			return violations;
		}
	}
}