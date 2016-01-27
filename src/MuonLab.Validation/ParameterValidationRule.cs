using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace MuonLab.Validation
{
	public class ParameterValidationRule<T> : BaseValidationRule<T, T>
	{
		public ParameterValidationRule(Expression<Func<T, ICondition<T>>> validationExpression) : base(validationExpression)
		{
			var param = this.Condition.Arguments[0] as ParameterExpression;
			this.PropertyExpression = Expression.Lambda<Func<T, T>>(param, param);
		}

		public override IEnumerable<IViolation> Validate<TOuter>(T entity, Expression<Func<TOuter, T>> prefix)
		{
			var condition = this.validationExpression.Compile().Invoke(entity) as PropertyCondition<T>;

			bool valid;

			try
			{
				valid = condition.Condition.Invoke(entity);
			}
			catch (Exception e)
			{
				if (prefix != null)
					return new[] { this.CreateViolation("ValidationError", entity, prefix) };

				return new[] { this.CreateViolation("ValidationError", entity, this.PropertyExpression) };
			}

			if (!valid)
			{
				if (prefix != null)
					return new[] {this.CreateViolation(condition.ErrorKey, entity, prefix)};
				
				return new[] {this.CreateViolation(condition.ErrorKey, entity, this.PropertyExpression)};
			}
			
			return new IViolation[0];
		}

		IViolation CreateViolation(string errorKey, T entity, Expression property)
		{
			var replacements = new Dictionary<string, ErrorDescriptor.Replacement>
			{
				{ "val", new ErrorDescriptor.Replacement(ErrorDescriptor.Replacement.ReplacementType.Scalar, ReferenceEquals(entity, null) ? "NULL" : entity.ToString()) }
			};

			for (var i = 1; i < this.Condition.Arguments.Count; i++)
				replacements.Add("arg" + (i - 1), this.EvaluateExpression(this.Condition.Arguments[i], entity));

			return new Violation(new ErrorDescriptor(errorKey, replacements), property, entity);
		}

		ErrorDescriptor.Replacement EvaluateExpression(Expression expression, T entity)
		{
			if (expression is MemberExpression)
				return new ErrorDescriptor.Replacement(ErrorDescriptor.Replacement.ReplacementType.Member, expression as MemberExpression);

			var lambda = Expression.Lambda(expression, this.validationExpression.Parameters[0]);
			var value = lambda.Compile().DynamicInvoke(entity);

			return new ErrorDescriptor.Replacement(ErrorDescriptor.Replacement.ReplacementType.Scalar, value != null ? value.ToString() : "NULL");
		}
	}
}