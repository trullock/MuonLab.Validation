using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Xunit;

namespace MuonLab.Validation.Tests
{
	class when_nesting_validation_arrays
	{
		[Fact]
		public void CorrectPropertyChainGenerated()
		{
			var outerClass = new Language
			{
				TermCategories = new[]
				{
					new Language.TermCategory
					{
						Terms = new[]
						{
							new Language.TermCategory.Term
							{
								Translated = "Translation"
							},
							new Language.TermCategory.Term
							{
								Translated = ""
							}
						}
					}
				}
			};

			var validator = new OuterClassValidator();

			var validationReport = validator.Validate(outerClass).Result;

			validationReport.IsValid.ShouldBeFalse();

			var violations = validationReport.Violations.ToArray();

			var error1 = Name(violations[0]); //ReflectionHelper.PropertyChainToString(violations[0].Property, '.');

			error1.ShouldEqual("TermCategories[0].Terms[1].Translated");
		}

		public string Name(IViolation violation)
		{
			var expression = violation.Property as LambdaExpression;

			var propertyName = ReflectionHelper.PropertyChainToString(violation.Property, '.');

			if (!propertyName.EndsWith(".Value"))
				return propertyName;

			var memberExpression = expression.Body as MemberExpression;
			if (!memberExpression.Member.ReflectedType.IsGenericType ||
			    memberExpression.Member.ReflectedType.GetGenericTypeDefinition() != typeof(Nullable<>))
				return propertyName;

			return propertyName.Substring(0, propertyName.Length - 6);
		}

		public class OuterClassValidator : Validator<Language>
		{
			protected override void Rules()
			{
				Ensure(x => x.TermCategories.AllSatisfy(new InnerClassValidator()));
			}
		}

		public class InnerClassValidator : Validator<Language.TermCategory>
		{
			protected override void Rules()
			{
				Ensure(x => x.Terms.AllSatisfy(new InnerInnerClassValidator()));
			}
		}

		public class InnerInnerClassValidator : Validator<Language.TermCategory.Term>
		{
			protected override void Rules()
			{
				Ensure(x => x.Translated.IsNotNullOrEmpty());
			}
		}

		public class Language
		{
			public TermCategory[] TermCategories { get; set; }

			public class TermCategory
			{
				public Term[] Terms { get; set; }

				public class Term
				{
					public string Translated { get; set; }
				}
			}
		}
	}
}