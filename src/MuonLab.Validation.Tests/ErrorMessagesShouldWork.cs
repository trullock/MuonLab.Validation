using System;
using System.Linq;
using NUnit.Framework;

namespace MuonLab.Validation.Tests
{
	[TestFixture]
	public class ErrorMessagesShouldWork
	{
		private TestValidator validator;
		private ValidationReport report;

		[SetUp]
		public void SetUp()
		{
			this.validator = new TestValidator();
			this.report = this.validator.Validate(new TestClass { Age = 12});
		}

		[Test]
		public void the_validation_report_should_be_valid()
		{
			report.Violations.First().ErrorMessage.ShouldEqual("Age 12 10");
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
			return self.Satisfies(x => false, "{val} {arg0} {arg1}");
		}
	}
}