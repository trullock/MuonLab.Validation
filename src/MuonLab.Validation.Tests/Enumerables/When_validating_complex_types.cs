using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace MuonLab.Validation.Tests.Enumerables
{
	public class When_validating_complex_types
	{
		[Fact]
		public void InnerViolationsShouldBeReportedCorrectly()
		{
			var testClass = new TestClass
			{
				List = new[] {new InnerClass(), new InnerClass {Name = "hello"}, new InnerClass()}
			};

			var testClassValidator = new TestClassValidator();

			var validationReport = testClassValidator.Validate(testClass).Result;

			validationReport.IsValid.ShouldBeFalse();

			var violations = validationReport.Violations.ToArray();

			var error1 = ReflectionHelper.PropertyChainToString(violations[0].Property, '.');
			error1.ShouldEqual("List[0].Name");

			var error2 = ReflectionHelper.PropertyChainToString(violations[1].Property, '.');
			error2.ShouldEqual("List[2].Name");
		}


		public class TestClass
		{
			public IList<InnerClass> List { get; set; }
		}

		public class InnerClass
		{
			public string Name { get; set; }
		}

		public class TestClassValidator : Validator<TestClass>
		{
			private readonly InnerClassValidator innerClassValidator;

			public TestClassValidator()
			{
				this.innerClassValidator = new InnerClassValidator();
			}

			protected override void Rules()
			{
				Ensure(x => x.List.AllSatisfy(innerClassValidator));
			}
		}

		public class InnerClassValidator : Validator<InnerClass>
		{
			protected override void Rules()
			{
				Ensure(x => x.Name.IsNotNullOrEmpty());
			}
		}
	}
}