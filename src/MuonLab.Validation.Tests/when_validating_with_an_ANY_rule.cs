using System.Linq;
using NUnit.Framework;

namespace MuonLab.Validation.Tests
{
	[TestFixture]
	public class when_validating_with_an_ANY_rule
	{
		ConditionalValidator validator;

		[SetUp]
		public void SetUp()
		{
			this.validator = new ConditionalValidator();
		}

		[Test]
		public void when_the_first_condition_is_true_the_second_shoudlnt_be_run()
		{
			var testClass = new TestClass(1, 2);

			var validationReport = this.validator.Validate(testClass);

			//validationReport.Violations.First().ErrorMessage.ShouldEqual("Value must be the same as 1");
			validationReport.Violations.Count().ShouldEqual(0);
		}

		[Test]
		public void when_the_first_condition_is_false_and_the_second_is_true_there_should_be_no_errors()
		{
			var testClass = new TestClass(2, 3);

			var validationReport = this.validator.Validate(testClass);

			//validationReport.Violations.First().ErrorMessage.ShouldEqual("Value 2 must be the same as 3");
			validationReport.Violations.Count().ShouldEqual(0);
		}

		[Test]
		public void whe_all_conditions_are_false_all_errors_should_show()
		{
			var testClass = new TestClass(0, 2);

			var validationReport = this.validator.Validate(testClass);

			//validationReport.Violations.First().ErrorMessage.ShouldEqual("Value 2 must be the same as 3");
			validationReport.Violations.Count().ShouldEqual(2);
		}

		class TestClass
		{
			public int Value { get; set; }
			public int Value2 { get; set; }

			public TestClass(int value, int value2)
			{
				this.Value = value;
				this.Value2 = value2;
			}
		}

		class ConditionalValidator : Validator<TestClass>
		{
			protected override void Rules()
			{
				Any(() =>
					    {
						    Ensure(x => x.Value.IsEqualTo(1));
						    Ensure(x => x.Value2.IsEqualTo(3));
					    });
			}
		}
	}
}