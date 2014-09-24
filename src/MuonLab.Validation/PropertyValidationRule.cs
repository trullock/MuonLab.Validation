using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace MuonLab.Validation
{
	sealed class PropertyValidationRule<T, TValue> : BaseValidationRule<T, TValue>
	{
		public PropertyValidationRule(Expression<Func<T, ICondition<TValue>>> validationExpression) : base(validationExpression)
		{
			this.property = this.Condition.Arguments[0] as MemberExpression;
			this.PropertyExpression = Expression.Lambda<Func<T, TValue>>(this.property, this.FindParameter(this.property));
		}

		public override IEnumerable<IViolation> Validate<TOuter>(T entity, Expression<Func<TOuter, T>> prefix)
		{
			var condition = this.validationExpression.Compile().Invoke(entity) as PropertyCondition<TValue>;

			Expression propExpr;

			if (prefix != null)
			{
				var combinedExpression = prefix.Combine(this.PropertyExpression, true);
				propExpr = combinedExpression;
			}
			else
				propExpr = this.PropertyExpression;

			var value = this.PropertyExpression.Compile().Invoke(entity);

			bool valid;

			try
			{
				valid = condition.Condition.Invoke(value);
			} 
			catch(NullReferenceException)
			{
				throw new ArgumentException("Unable to validate " + propExpr + " some part of the chain is null.\n\nValidation Expression: " + this.validationExpression + "\n\nEntity: " + entity);
			}

			if (valid)
				return new IViolation[0];
			
			return new[] {this.CreateViolation(condition.ErrorKey, value, entity, propExpr)};				
		}

		protected IViolation CreateViolation(string errorKey, TValue value, T entity, Expression property)
		{
			var replacements = new Dictionary<string, string>
			{
				{ "val", ReferenceEquals(value, null) ? "NULL" : value.ToString() }
			};

			for (var i = 1; i < this.Condition.Arguments.Count; i++)
				replacements.Add("arg" + (i - 1), this.EvaluateExpression(this.Condition.Arguments[i], entity));

			return new Violation(new ErrorDescriptior(errorKey, replacements), property, value);
		}

		protected string GetMemberName(MemberExpression member)
		{
			if (this.property.Member.DeclaringType.IsGenericType && this.property.Member.DeclaringType.GetGenericTypeDefinition() == typeof(Nullable<>))
			{
				if(member.Expression is MemberExpression)
					return (member.Expression as MemberExpression).Member.GetEnglishName();
			}

			return member.Member.GetEnglishName();
		}

		protected string EvaluateExpression(Expression expression, T entity)
		{
			if (expression is MemberExpression)
				return this.GetMemberName(expression as MemberExpression);
			
			var lambda = Expression.Lambda(expression, this.validationExpression.Parameters[0]);
			var value = lambda.Compile().DynamicInvoke(entity);

			return value != null ? value.ToString() : "NULL";
		}
	}
}