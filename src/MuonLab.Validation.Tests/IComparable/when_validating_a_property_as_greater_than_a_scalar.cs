using System.Linq;
using NUnit.Framework;

namespace MuonLab.Validation.Tests.IComparable
{
	[TestFixture]
	public class when_validating_a_property_as_greater_than_a_scalar
	{
		TestClassValidator validator;

		[SetUp]
		public void SetUp()
		{
			this.validator = new TestClassValidator();
		}

		[Test]
		public void test_1_greater_than_4_returns_false()
		{
			var testClass = new TestClass(1);

			var validationReport = this.validator.Validate(testClass);

			var violations = validationReport.Violations.ToArray();

			Assert.AreEqual("value must be greater than 4", violations[0].ErrorMessage);
		}

		[Test]
		public void test_8_greater_than_4_returns_true()
		{
			var testClass = new TestClass(8);

			var validationReport = this.validator.Validate(testClass);

			Assert.IsTrue(validationReport.IsValid);
		}

		[Test]
		public void test_4_greater_than_4_returns_false()
		{
			var testClass = new TestClass(4);

			var validationReport = this.validator.Validate(testClass);

			var violations = validationReport.Violations.ToArray();

			Assert.AreEqual("value must be greater than 4", violations[0].ErrorMessage);
		}

		private class TestClass
		{
			public int value { get; set; }
			public TestClass(int value)
			{
				this.value = value;
			}
		}

		private class TestClassValidator : Validator<TestClass> {
			protected override void Rules()
			{
				Ensure(x => x.value.IsGreaterThan(4));
			}
		}
	}
}