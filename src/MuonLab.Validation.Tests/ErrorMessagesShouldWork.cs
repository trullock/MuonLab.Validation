using System.Linq;
using Xunit;

namespace MuonLab.Validation.Tests
{
	public class ErrorMessagesShouldWork
	{
		TestValidator validator;
		ValidationReport report;

		[SetUp]
		public void SetUp()
		{
			this.validator = new TestValidator();
			this.report = this.validator.Validate(new TestClass {Age = 12});
		}

		[Fact]
		public void the_validation_report_should_be_valid()
		{
			report.Violations.First().Error.Key.ShouldEqual("Key");
		}

		public class TestValidator : Validator<TestClass>
		{
			protected override void Rules()
			{
				Ensure(x => x.Age.ShouldProduceErrorMessage(10));
			}
		}
	}

	public static class TestExtensions
	{
		public static ICondition<int> ShouldProduceErrorMessage(this int self, int someArg)
		{
			return self.Satisfies(x => false, "Key");
		}
	}
}