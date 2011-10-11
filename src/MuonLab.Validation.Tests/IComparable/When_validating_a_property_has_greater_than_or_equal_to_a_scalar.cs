using System.Linq;
using NUnit.Framework;

namespace MuonLab.Validation.Tests.IComparable
{
	[TestFixture]
	public class When_validating_a_property_has_greater_than_or_equal_to_a_scalar
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
			var testClass = new TestClass(1);

			var validationReport = this.validator.Validate(testClass);

			var violations = validationReport.Violations.ToArray();

			Assert.AreEqual("Value must be greater than or equal to 4", violations[0].ErrorMessage);
		}

		[Test]
		public void test_4_greater_than_or_equal_1_returns_true()
		{
			var testClass = new TestClass(4);

			var validationReport = this.validator.Validate(testClass);

			Assert.IsTrue(validationReport.IsValid);
		}

		[Test]
		public void test_4_greater_than_or_equal_4_returns_true()
		{
			var testClass = new TestClass(4);

			var validationReport = this.validator.Validate(testClass);

			Assert.IsTrue(validationReport.IsValid);
		}

		private class TestClass
		{
			public int Value { get; set; }

			public TestClass(int value)
			{
				this.Value = value;
			}
		}

		private class TestClassValidator : Validator<TestClass>
		{
			protected override void Rules()
			{
				Ensure(x => x.Value.IsGreaterThanOrEqualTo(4));
			}
		}
	}
}