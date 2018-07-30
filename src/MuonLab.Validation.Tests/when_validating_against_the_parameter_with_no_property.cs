using System;
using Xunit;

namespace MuonLab.Validation.Tests
{
	public class when_validating_against_the_parameter_with_no_property
	{
		private TestValidator validator;
		private ValidationReport report;

		[SetUp]
		public void SetUp()
		{
			this.validator = new TestValidator();
			this.report = this.validator.Validate(new TestClass());
		}

		[Fact]
		public void the_validation_report_should_be_invalid()
		{
			report.IsValid.ShouldBeFalse();
		}

		public class TestValidator : Validator<TestClass>
		{
			protected override void Rules()
			{
				Ensure(x => x.Satisfies(p => false, "should fail"));
			}
		}
	}
}