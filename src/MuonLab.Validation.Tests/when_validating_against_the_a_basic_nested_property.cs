using System;
using Xunit;

namespace MuonLab.Validation.Tests
{
	public class when_validating_against_the_a_basic_nested_property
	{
		[Fact]
		public void the_validation_report_should_be_valid()
		{
			var validator = new TestClassWrapperValidator();
			var report = validator.Validate(new TestClassWrapper());
			report.IsValid.ShouldBeTrue();
		}

		public class TestClassValidator : Validator<TestClass>
		{
			protected override void Rules()
			{
				Ensure(x => x.Age.Satisfies(p => true, "should work!"));
			}
		}

		public class TestClassWrapperValidator : Validator<TestClassWrapper>
		{
			private TestClassValidator classValidator;

			public TestClassWrapperValidator()
			{
				this.classValidator = new TestClassValidator();
			}

			protected override void Rules()
			{
				Ensure(x => x.TestClass.Satisfies(classValidator));
			}
		}
	}
}