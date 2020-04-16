using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MuonLab.Validation
{
	sealed class ConditionalValidationRule<T, TValue> : BaseValidationRule<T, TValue>
	{
		readonly Expression<Func<T, ICondition<TValue>>> condition;
		readonly IEnumerable<IValidationRule<T>> rules;

		public ConditionalValidationRule(Expression<Func<T, ICondition<TValue>>> expression, IEnumerable<IValidationRule<T>> rules) : base(expression)
		{
			this.condition = expression;
			this.rules = rules;
		}

		public override async Task<IEnumerable<IViolation>> Validate<TOuter>(T entity, Expression<Func<TOuter, T>> prefix)
		{
			var methodCallExpression = this.condition.Body as MethodCallExpression;
			var genericTypeDefinition = methodCallExpression.Method.ReturnType.GetGenericTypeDefinition();
			var rule = this.GetRule(genericTypeDefinition, methodCallExpression);

			var violations = await rule.Validate(entity, prefix);

			// TODO: wtf? I dont understand why this seems backwards
			if (violations.Any()) 
				return new IViolation[0];

			var violations1 = new List<IViolation>();
			foreach (var crule in this.rules)
				violations1.AddRange(await crule.Validate(entity, prefix));
			return violations1;
		}

		IValidationRule<T> GetRule(Type genericTypeDefinition, MethodCallExpression methodCallExpression)
		{
			if (genericTypeDefinition == typeof (ChildValidationCondition<>))
				return new ChildValidationRule<T, TValue>(this.condition);

			if (genericTypeDefinition == typeof (ChildListValidationCondition<>))
			{
				var listItemType = typeof (TValue).GetGenericArguments()[0];
				var ruleType = typeof (ChildListValidationRule<,>).MakeGenericType(typeof (T), listItemType);
				return Activator.CreateInstance(ruleType, this.condition) as IValidationRule<T>;
			}
			
			if (methodCallExpression.Arguments[0] is MemberExpression)
				return new PropertyValidationRule<T, TValue>(this.condition);
			
			if (methodCallExpression.Arguments[0] is ParameterExpression)
				return new ParameterValidationRule<T>(this.condition as Expression<Func<T, ICondition<T>>>);
			
			throw new NotSupportedException();
		}
	}
}