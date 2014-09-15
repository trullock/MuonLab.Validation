using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace MuonLab.Validation
{
	/// <summary>
	/// Abstract base class for Validators
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public abstract class Validator<T> : IValidator<T>
	{
		protected IList<IValidationRule<T>> vRules;
		protected abstract void Rules();

		public IEnumerable<IValidationRule<T>> ValidationRules
		{
			get
			{
				if(this.vRules == null)
				{
					this.vRules = new List<IValidationRule<T>>();
					Rules();
				}
				return this.vRules;
			}
		}

		public virtual ValidationReport Validate(T entity)
		{
			return Validate<object>(entity, null);
		}

		public virtual ValidationReport Validate<TOuter>(T entity, Expression<Func<TOuter, T>> prefix)
		{
			var violations = new List<IViolation>();

			foreach (var rule in this.ValidationRules)
			{
				var results = rule.Validate(entity, prefix);
				violations.AddRange(results);
			}

			return new ValidationReport(violations);
		}

		ValidationReport IValidator.Validate(object entity)
		{
			return Validate((T)entity);
		}

		/// <summary>
		/// WARNING This will currently fail for child and parameter-only rules
		/// </summary>
		/// <typeparam name="TProperty"></typeparam>
		/// <param name="property"></param>
		/// <returns></returns>
		public IEnumerable<IValidationRule<T>> GetRulesFor<TProperty>(Expression<Func<T, TProperty>> property)
		{
			var foundRules = new List<IValidationRule<T>>();

			foreach (IValidationRule<T> rule in this.ValidationRules)
			{
				// TODO: handle children
				var castRule = rule as PropertyValidationRule<T, TProperty>;

				if(castRule != null)
				{
					if (ReflectionHelper.MemberAccessExpressionsAreEqual(castRule.PropertyExpression, property))
						foundRules.Add(rule);
				}
			}

			return foundRules;
		}

		protected ConditionalChain<TValue> Ensure<TValue>(Expression<Func<T, ICondition<TValue>>> propertyCondition)
		{
			var methodCallExpression = propertyCondition.Body as MethodCallExpression;

			var genericTypeDefinition = methodCallExpression.Method.ReturnType.GetGenericTypeDefinition();
			this.AddRule(propertyCondition, genericTypeDefinition, methodCallExpression);

			return new ConditionalChain<TValue>(this, propertyCondition);
		}

		protected ConditionalChain<TValue> Ensure<TValue>(Expression<Func<T, ICondition<IList<TValue>>>> propertyCondition)
		{
			var methodCallExpression = propertyCondition.Body as MethodCallExpression;

			var genericTypeDefinition = methodCallExpression.Method.ReturnType.GetGenericTypeDefinition();
			
			this.AddRule(propertyCondition, genericTypeDefinition);

			return new ConditionalChain<TValue>(this, propertyCondition as Expression<Func<T, ICondition<TValue>>>);
		}

		void AddRule<TValue>(Expression<Func<T, ICondition<TValue>>> propertyCondition, Type genericTypeDefinition, MethodCallExpression methodCallExpression)
		{
			if (genericTypeDefinition == typeof (ChildValidationCondition<>))
			{
				this.vRules.Add(new ChildValidationRule<T, TValue>(propertyCondition));
				return;
			}

			if (methodCallExpression.Arguments[0] is MemberExpression)
			{
				this.vRules.Add(new PropertyValidationRule<T, TValue>(propertyCondition));
				return;
			}

			if (methodCallExpression.Arguments[0] is ParameterExpression)
			{
				this.vRules.Add(new ParameterValidationRule<T>(propertyCondition as Expression<Func<T, ICondition<T>>>));
				return;
			}
			
			throw new NotSupportedException();
		}

		void AddRule<TValue>(Expression<Func<T, ICondition<IList<TValue>>>> propertyCondition, Type genericTypeDefinition)
		{
			if (genericTypeDefinition == typeof (ChildListValidationCondition<>))
			{
				this.vRules.Add(new ChildListValidationRule<T, TValue>(propertyCondition));
				return;
			}

			if (genericTypeDefinition == typeof (CollectionCondition<,>))
			{
				this.vRules.Add(new CollectionValidationRule<T, TValue>(propertyCondition));
				return;
			}
			
			throw new NotSupportedException();
		}

		protected void When<TValue>(Expression<Func<T, ICondition<TValue>>> whenCondition, Action rules)
		{
			var otherRules = this.vRules;

			this.vRules = new List<IValidationRule<T>>();

			rules();

			var conditionalRules = this.vRules;

			this.vRules = otherRules;

			this.vRules.Add(new ConditionalValidationRule<T, TValue>(whenCondition, conditionalRules));
		}

		protected void When(Func<bool> whenCondition, Action rules)
		{
			var otherRules = this.vRules;

			this.vRules = new List<IValidationRule<T>>();

			rules();

			var conditionalRules = this.vRules;

			this.vRules = otherRules;

			this.vRules.Add(new ConditionalPredicateValidationRule<T>(whenCondition, conditionalRules));
		}

		protected void Any(Action rules)
		{
			var otherRules = this.vRules;

			this.vRules = new List<IValidationRule<T>>();

			rules();

			var anyRules = this.vRules;

			this.vRules = otherRules;

			this.vRules.Add(new AnyValidationRule<T>(anyRules));
		}

		public class ConditionalChain<TValue>
		{
			readonly Validator<T> validator;
			readonly Expression<Func<T, ICondition<TValue>>> whenCondition;

			public ConditionalChain(Validator<T> validator, Expression<Func<T, ICondition<TValue>>> propertyCondition)
			{
				this.validator = validator;
				this.whenCondition = propertyCondition;
			}

			public void And(Action conditionalRules)
			{
				validator.When(whenCondition, conditionalRules);
			}
		}
	}
}