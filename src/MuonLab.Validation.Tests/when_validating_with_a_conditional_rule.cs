using Xunit;

namespace MuonLab.Validation.Tests
{
	public class when_validating_with_a_conditional_rule
	{
		private ConditionalValidator validator;

		[SetUp]
		public void SetUp()
		{
			this.validator = new ConditionalValidator();
		}

		[Fact]
		public void when_a_condition_is_false_the_validation_rule_should_not_be_run()
		{
			var testClass = new TestClass(2, 2);

			var validationReport = this.validator.Validate(testClass);

			Assert.IsTrue(validationReport.IsValid);
		}

		[Fact]
		public void when_a_condition_is_true_the_validation_rule_should_be_run()
		{
			var testClass = new TestClass(1, 2);

			var validationReport = this.validator.Validate(testClass);

			Assert.IsFalse(validationReport.IsValid);
		}

		private class TestClass
		{
			public int Value { get; set; }
			public int Value2 { get; set; }

			public TestClass(int value, int value2)
			{
				this.Value = value;
				this.Value2 = value2;
			}
		}

		private class ConditionalValidator : Validator<TestClass>
		{
			protected override void Rules()
			{
				When(x => x.Value.IsEqualTo(1), () => Ensure(x => x.Value2.IsEqualTo(3)));
			}
		}
	}
}