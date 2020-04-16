using System;
using Xunit;

namespace MuonLab.Validation.Tests
{
	public class when_validating_against_the_parameter_with_no_property
	{
		[Fact]
		public void the_validation_report_should_be_invalid()
		{
			var validator = new TestValidator();
			var report = validator.Validate(new TestClass()).Result;
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