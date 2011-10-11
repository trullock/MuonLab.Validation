using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace MuonLab.Validation
{
	internal sealed class ConditionalValidationRule<T, TValue> : BaseValidationRule<T, TValue>
	{
		private readonly Expression<Func<T, ICondition<TValue>>> condition;
		private readonly IEnumerable<IValidationRule<T>> rules;

		public ConditionalValidationRule(Expression<Func<T, ICondition<TValue>>> expression, IEnumerable<IValidationRule<T>> rules) : base(expression)
		{
			this.condition = expression;
			this.rules = rules;
		}

		public override IEnumerable<IViolation> Validate<TOuter>(T entity, Expression<Func<TOuter, T>> prefix)
		{
			IValidationRule<T> rule;

			var methodCallExpression = this.condition.Body as MethodCallExpression;
			var genericTypeDefinition = methodCallExpression.Method.ReturnType.GetGenericTypeDefinition();
			if (genericTypeDefinition == typeof(ChildValidationCondition<>))
				rule = new ChildValidationRule<T, TValue>(this.condition);
			else if (genericTypeDefinition == typeof(ChildListValidationCondition<>))
			{
				var listItemType = typeof(TValue).GetGenericArguments()[0];
				var ruleType = typeof(ChildListValidationRule<,>).MakeGenericType(typeof(T), listItemType);
				rule = Activator.CreateInstance(ruleType, this.condition) as IValidationRule<T>;
			}
			else
			{
				if (methodCallExpression.Arguments[0] is MemberExpression)
					rule = new PropertyValidationRule<T, TValue>(this.condition);
				else if (methodCallExpression.Arguments[0] is ParameterExpression)
					rule = new ParameterValidationRule<T>(this.condition as Expression<Func<T, ICondition<T>>>);
				else
					// TODO: what causes this
					throw new NotSupportedException();
			}

			var violations = rule.Validate(entity, prefix);

			var violations1 = new List<IViolation>();
            
			if(violations.Count() == 0)
			{
				foreach (var crule in this.rules)
					violations1.AddRange(crule.Validate(entity, prefix));
			}

			return violations1;
		}
	}
}