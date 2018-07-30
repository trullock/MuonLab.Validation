using Xunit;

namespace MuonLab.Validation.Tests.NullProperties
{
	public class when_validating_a_null_property
	{
		[Fact]
		public void the_validation_report_should_be_valid()
		{
			var validator = new TestClassValidator();
			var report = validator.Validate(new TestClass());
			report.IsValid.ShouldBeTrue();
		}

		public class TestClassValidator : Validator<TestClass>
		{
			protected override void Rules()
			{
				Ensure(x => x.Name.Satisfies(p => true, "should work!"));
			}
		}
	}
}