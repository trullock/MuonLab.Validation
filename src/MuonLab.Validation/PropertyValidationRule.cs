using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace MuonLab.Validation
{
	internal sealed class PropertyValidationRule<T, TValue> : BaseValidationRule<T, TValue>
	{
		public PropertyValidationRule(Expression<Func<T, ICondition<TValue>>> validationExpression) : 
			base(validationExpression)
		{
			this.property = this.Condition.Arguments[0] as MemberExpression;
			this.PropertyExpression = Expression.Lambda<Func<T, TValue>>(this.property, findParameter(this.property));
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
			
			return new[] {createViolation(condition.ErrorMessage, value, entity, propExpr)};				
		}

		protected IViolation createViolation(string errorMessage, TValue value, T entity, Expression property)
		{
			errorMessage = errorMessage.Replace("{val}", getMemberName(this.property));
			
			errorMessage = errorMessage.Replace("{arg0}", ReferenceEquals(value, null) ? "NULL" : value.ToString());

			for (int i = 1; i < this.Condition.Arguments.Count; i++)
				errorMessage = errorMessage.Replace("{arg" + i + "}", evaluateExpression(this.Condition.Arguments[i], entity));

			return new Violation(errorMessage, property, value);
		}

		protected string getMemberName(MemberExpression member)
		{
			if (this.property.Member.DeclaringType.IsGenericType && this.property.Member.DeclaringType.GetGenericTypeDefinition() == typeof(Nullable<>))
			{
				if(member.Expression is MemberExpression)
					return (member.Expression as MemberExpression).Member.GetEnglishName();
			}

			return member.Member.GetEnglishName();
		}

		protected string evaluateExpression(Expression expression, T entity)
		{
			if (expression is MemberExpression)
				return getMemberName(expression as MemberExpression);
			
			var lambda = Expression.Lambda(expression, this.validationExpression.Parameters[0]);
			var value = lambda.Compile().DynamicInvoke(entity);

			return value != null ? value.ToString() : "NULL";
		}
	}
}