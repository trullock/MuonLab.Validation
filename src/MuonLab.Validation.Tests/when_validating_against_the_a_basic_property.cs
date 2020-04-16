using Xunit;

namespace MuonLab.Validation.Tests
{
	public class when_validating_against_the_a_basic_property
	{
		[Fact]
		public void the_validation_report_should_be_valid()
		{
			var validator = new TestValidator();
			var report = validator.Validate(new TestClass()).Result;
			report.IsValid.ShouldBeTrue();
		}

		public class TestValidator : Validator<TestClass>
		{
			protected override void Rules()
			{
				Ensure(x => x.Age.Satisfies(p => true, "should work!"));
			}
		}
	}
}