using System.Linq;
using NUnit.Framework;

namespace MuonLab.Validation.Tests.IComparable
{
	[TestFixture]
	public class When_validating_a_property_as_not_equal_to_another
	{
		private TestClassValidator validator;

		[SetUp]
		public void SetUp()
		{
			this.validator = new TestClassValidator();
		}

		[Test]
		public void test_1_not_equals_4_returns_true()
		{
			var testClass = new TestClass(1, 4);

			var validationReport = this.validator.Validate(testClass);

			Assert.IsTrue(validationReport.IsValid);
		}

		[Test]
		public void test_4_not_equals_1_returns_true()
		{
			var testClass = new TestClass(4, 1);

			var validationReport = this.validator.Validate(testClass);

			Assert.IsTrue(validationReport.IsValid);
		}

		[Test]
		public void test_2_not_equals_2_returns_false()
		{
			var testClass = new TestClass(2, 2);

			var validationReport = this.validator.Validate(testClass);

			var violations = validationReport.Violations.ToArray();

			Assert.AreEqual("value must not be the same as Value 2", violations[0].ErrorMessage);
		}

		private class TestClass
		{
			public int value { get; set; }
			public int Value2 { get; set; }

			public TestClass(int value, int value2)
			{
				this.value = value;
				this.Value2 = value2;
			}
		}

		private class TestClassValidator : Validator<TestClass>
		{
			protected override void Rules()
			{
				Ensure(x => x.value.IsNotEqualTo(x.Value2));
			}
		}
	}
}