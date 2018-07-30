using System.Linq;
using Xunit;

namespace MuonLab.Validation.Tests
{
	public class ErrorMessagesShouldWork
	{
		[Fact]
		public void the_validation_report_should_be_valid()
		{
			var validator = new TestValidator();
			var report = validator.Validate(new TestClass { Age = 12 });
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