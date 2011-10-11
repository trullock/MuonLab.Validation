using System.Linq;
using NUnit.Framework;

namespace MuonLab.Validation.Tests.ReferenceType
{
	[TestFixture]
	public class When_validating_a_property_as_not_null
	{
		private TestClassValidator validator;

		[SetUp]
		public void SetUp()
		{
			this.validator = new TestClassValidator();
		}

		[Test]
		public void ensure_not_null_returns_true()
		{
			var testClass = new TestClass(new object());

			var validationReport = this.validator.Validate(testClass);

			Assert.IsTrue(validationReport.IsValid);
		}

		[Test]
		public void ensure_not_null_returns_false()
		{
			var testClass = new TestClass(null);

			var validationReport = this.validator.Validate(testClass);

			var violations = validationReport.Violations.ToArray();

			Assert.AreEqual("value is required", violations[0].ErrorMessage);
			Assert.AreEqual("test message", violations[1].ErrorMessage);
		}

		private class TestClass
		{
			public object value { get; set; }

			public TestClass(object value)
			{
				this.value = value;
			}
		}

		private class TestClassValidator : Validator<TestClass>
		{
			protected override void Rules()
			{
				Ensure(x => x.value.IsNotNull());
				Ensure(x => x.value.IsNotNull("test message"));
			}
		}
	}
}