using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MuonLab.Validation
{
	public abstract class BaseValidationRule<T, TValue> : IValidationRule<T>
	{
		protected MemberExpression property;
		protected readonly Expression<Func<T, ICondition<TValue>>> validationExpression;

		public MethodCallExpression Condition { get; protected set; }
		public Expression<Func<T, TValue>> PropertyExpression { get; protected set; }

		protected BaseValidationRule(Expression<Func<T, ICondition<TValue>>> validationExpression)
		{
			this.validationExpression = validationExpression;
			this.Condition = validationExpression.Body as MethodCallExpression;
		}

		public abstract Task<IEnumerable<IViolation>> Validate<TOuter>(T entity, Expression<Func<TOuter, T>> prefix);

		protected ParameterExpression FindParameter(Expression expression)
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
	}
}