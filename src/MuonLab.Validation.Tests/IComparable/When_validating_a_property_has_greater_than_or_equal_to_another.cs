using System.Linq;
using NUnit.Framework;

namespace MuonLab.Validation.Tests.IComparable
{
	[TestFixture]
	public class When_validating_a_property_has_greater_than_or_equal_to_another
	{
		private TestClassValidator validator;

		[SetUp]
		public void SetUp()
		{
			this.validator = new TestClassValidator();
		}

		[Test]
		public void test_1_greater_than_or_equal_4_returns_false()
		{
			var testClass = new TestClass(1, 4);

			var validationReport = this.validator.Validate(testClass);

			validationReport.Violations.First().Error.Key.ShouldEqual("GreaterThanEq");
			validationReport.Violations.First().Error.Replacements["arg0"].ShouldEqual("Value 2");
		}

		[Test]
		public void test_4_greater_than_or_equal_1_returns_true()
		{
			var testClass = new TestClass(4, 1);

			var validationReport = this.validator.Validate(testClass);

			Assert.IsTrue(validationReport.IsValid);
		}

		[Test]
		public void test_2_greater_than_or_equal_2_returns_true()
		{
			var testClass = new TestClass(2, 2);

			var validationReport = this.validator.Validate(testClass);

			Assert.IsTrue(validationReport.IsValid);
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

		private class TestClassValidator : Validator<TestClass>
		{
			protected override void Rules()
			{
				Ensure(x => x.Value.IsGreaterThanOrEqualTo(x.Value2));
			}
		}
	}
}