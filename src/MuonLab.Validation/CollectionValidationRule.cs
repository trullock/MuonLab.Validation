using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace MuonLab.Validation
{
	class CollectionValidationRule<T, TValue> : BaseValidationRule<T, IList<TValue>>
	{
		public CollectionValidationRule(Expression<Func<T, ICondition<IList<TValue>>>> validationExpression)
			: base(validationExpression)
		{
			this.property = this.Condition.Arguments[0] as MemberExpression;
			this.PropertyExpression = Expression.Lambda<Func<T, IList<TValue>>>(this.property, this.FindParameter(this.property));
		}

		public override IEnumerable<IViolation> Validate<TOuter>(T entity, Expression<Func<TOuter, T>> prefix)
		{
			var condition = this.validationExpression.Compile().Invoke(entity) as ICollectionCondition<TValue>;

			var value = this.PropertyExpression.Compile().Invoke(entity);

			var expressions = condition.GetViolations(prefix, this.PropertyExpression);

			var propertyCondition = condition as PropertyCondition;

			return expressions.Select(expression => this.CreateViolation(propertyCondition.ErrorKey, value, entity, expression)).ToArray();
		}

		protected IViolation CreateViolation(string errorKey, IEnumerable<TValue> value, T entity, Expression property)
		{
			var replacements = new Dictionary<string, ErrorDescriptior.Replacement>
			{
				{ "val", new ErrorDescriptior.Replacement(ErrorDescriptior.Replacement.ReplacementType.Scalar, ReferenceEquals(value, null) ? "NULL" : value.ToString()) }
			};

			for (var i = 1; i < this.Condition.Arguments.Count; i++)
				replacements.Add("arg" + (i - 1), this.EvaluateExpression(this.Condition.Arguments[i], entity));
			
			return new Violation(new ErrorDescriptior(errorKey, replacements), property, value);
		}

		protected ErrorDescriptior.Replacement EvaluateExpression(Expression expression, T entity)
		{
			if (expression is MemberExpression)
				return new ErrorDescriptior.Replacement(ErrorDescriptior.Replacement.ReplacementType.Member, expression as MemberExpression);

			var lambda = Expression.Lambda(expression, this.validationExpression.Parameters[0]);
			var value = lambda.Compile().DynamicInvoke(entity);

			return new ErrorDescriptior.Replacement(ErrorDescriptior.Replacement.ReplacementType.Scalar, value != null ? value.ToString() : "NULL");
		}
	}
}