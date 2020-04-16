using Xunit;

namespace MuonLab.Validation.Tests
{
	public class when_conditionally_validating_against_the_parameter_with_no_property
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
				When(x => x.Satisfies(p => true, ""), () =>
					Ensure(x => x.Name.IsNotNullOrEmpty()));
			}
		}
	}
}