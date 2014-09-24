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

			var valid = condition.Condition.Invoke(entity);

			if (!valid)
			{
				if (prefix != null)
					return new[] {this.CreateViolation(condition, entity, prefix)};
				
				return new[] {this.CreateViolation(condition, entity, this.PropertyExpression)};
			}
			
			return new IViolation[0];
		}

		protected IViolation CreateViolation(PropertyCondition<T> condition, T entity, Expression property)
		{
			var replacements = new Dictionary<string, string>
			{
				{ "val", ReferenceEquals(entity, null) ? "NULL" : entity.ToString() }
			};

			for (var i = 1; i < this.Condition.Arguments.Count; i++)
				replacements.Add("arg" + (i - 1), this.EvaluateExpression(this.Condition.Arguments[i], entity));

			return new Violation(new ErrorDescriptior(condition.ErrorKey, replacements), property, entity);
		}

		protected string GetMemberName(MemberExpression member)
		{
			if (this.property.Member.DeclaringType.IsGenericType && this.property.Member.DeclaringType.GetGenericTypeDefinition() == typeof(Nullable<>))
				return (member.Expression as MemberExpression).Member.GetEnglishName();
			
			return member.Member.GetEnglishName();
		}

		protected string EvaluateExpression(Expression expression, T entity)
		{
			if (expression is MemberExpression)
				return this.GetMemberName(expression as MemberExpression);
			
			var lambda = Expression.Lambda(expression, this.validationExpression.Parameters[0]);
			var value = lambda.Compile().DynamicInvoke(entity);

			return ReferenceEquals(value, null) ? "NULL" : value.ToString();
		}
	}
}