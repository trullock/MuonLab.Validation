using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace MuonLab.Validation.Tests
{
	class when_nesting_validation
	{
		[Fact]
		public void CorrectPropertyChainGenerated()
		{
			var outerClass = new OuterClass
			{
				InnerClass = new InnerClass
				{
					InnerInnerClass = new InnerInnerClass
					{
						InnerInnerInnerClass = new InnerInnerInnerClass
						{
							Property = "Hello"
						}
					}
				}
			};

			var validator = new OuterClassValidator();

			var validationReport = validator.Validate(outerClass);

			validationReport.IsValid.ShouldBeFalse();

			var violations = validationReport.Violations.ToArray();

			var error1 = ReflectionHelper.PropertyChainToString(violations[0].Property, '.');

			error1.ShouldEqual("InnerClass.InnerInnerClass.InnerInnerInnerClass.Property");
		}

		public class OuterClassValidator : Validator<OuterClass>
		{
			protected override void Rules()
			{
				Ensure(x => x.InnerClass.Satisfies(new InnerClassValidator()));
			}
		}

		public class InnerClassValidator : Validator<InnerClass>
		{
			protected override void Rules()
			{
				Ensure(x => x.InnerInnerClass.Satisfies(new InnerInnerClassValidator()));
			}
		}

		public class InnerInnerClassValidator : Validator<InnerInnerClass>
		{
			protected override void Rules()
			{
				Ensure(x => x.InnerInnerInnerClass.Satisfies(new InnerInnerInnerClassValidator()));
			}
		}

		public class InnerInnerInnerClassValidator : Validator<InnerInnerInnerClass>
		{
			protected override void Rules()
			{
				Ensure(x => x.Property.IsNotEqualTo("Hello"));
			}
		}

		public class OuterClass
		{
			public InnerClass InnerClass { get; set; }
		}

		public class InnerClass
		{
			public InnerInnerClass InnerInnerClass { get; set; }
		}

		public class InnerInnerClass
		{
			public InnerInnerInnerClass InnerInnerInnerClass { get; set; }
		}

		public class InnerInnerInnerClass
		{
			public string Property { get; set; }
		}
	}
}