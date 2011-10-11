using System;
using NUnit.Framework;

namespace MuonLab.Validation.Tests
{
	[TestFixture]
	public class when_validating_against_the_a_basic_nested_property 
	{
		private TestClassWrapperValidator validator;
		private ValidationReport report;

		[SetUp]
		public void SetUp()
		{
			this.validator = new TestClassWrapperValidator();
			this.report = this.validator.Validate(new TestClassWrapper());
		}

		[Test]
		public void the_validation_report_should_be_valid()
		{
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