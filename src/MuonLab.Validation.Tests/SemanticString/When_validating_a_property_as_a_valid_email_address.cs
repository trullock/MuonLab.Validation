using System.Linq;
using NUnit.Framework;

namespace MuonLab.Validation.Tests.SemanticString
{
	[TestFixture]
	public class When_validating_a_property_as_a_valid_email_address
	{
		private TestClassValidator validator;

		[SetUp]
		public void SetUp()
		{
			this.validator = new TestClassValidator();
		}

		[Test]
		public void ensure_nulls_fail_validation()
		{
			var testClass = new TestClass(null);

			var validationReport = this.validator.Validate(testClass);

			var violations = validationReport.Violations.ToArray();

			Assert.AreEqual("value must be a valid email address", violations[0].ErrorMessage);
		}

		[Test]
		public void ensure_empty_string_fail_validation()
		{
			var testClass = new TestClass(string.Empty);

			var validationReport = this.validator.Validate(testClass);

			var violations = validationReport.Violations.ToArray();

			Assert.AreEqual("value must be a valid email address", violations[0].ErrorMessage);
		}

		[Test]
		public void ensure_valid_email_passes_validation()
		{
			var testClass = new TestClass("andrew.bullock@north-51.com");

			var validationReport = this.validator.Validate(testClass);

			Assert.IsTrue(validationReport.IsValid);
		}

		[Test]
		public void ensure_invalid_email2_fails_validation()
		{
			var testClass = new TestClass("andrew.bullock.north-51.com");
			var validationReport = this.validator.Validate(testClass);

			var violations = validationReport.Violations.ToArray();
			Assert.AreEqual("value must be a valid email address", violations[0].ErrorMessage);
		}

		[Test]
		public void ensure_invalid_email3_fails_validation()
		{
			var testClass = new TestClass("www.north-51.com");
			var validationReport = this.validator.Validate(testClass);

			var violations = validationReport.Violations.ToArray();
			Assert.AreEqual("value must be a valid email address", violations[0].ErrorMessage);
		}

		private class TestClass
		{
			public string value { get; set; }

			public TestClass(string value)
			{
				this.value = value;
			}
		}

		private class TestClassValidator : Validator<TestClass>
		{
			protected override void Rules()
			{
				Ensure(x => x.value.IsAValidEmailAddress());
			}
		}
	}
}