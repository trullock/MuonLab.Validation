using System.Linq;
using NUnit.Framework;

namespace MuonLab.Validation.Tests.SemanticString
{
	[TestFixture]
	public class When_validating_a_property_as_a_valid_bs7666_post_code
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

			Assert.AreEqual("Postcode must be a valid postcode", violations[0].ErrorMessage);
		}

		[Test]
		public void ensure_empty_string_fail_validation()
		{
			var testClass = new TestClass(string.Empty);

			var validationReport = this.validator.Validate(testClass);

			var violations = validationReport.Violations.ToArray();

			Assert.AreEqual("Postcode must be a valid postcode", violations[0].ErrorMessage);
		}

		[Test]
		public void ensure_ANNAA_passes_validation()
		{
			var testClass = new TestClass("M1 1AA");

			var validationReport = this.validator.Validate(testClass);

			Assert.IsTrue(validationReport.IsValid);
		}

		[Test]
		public void ensure_ANNNAA_passes_validation()
		{
			var testClass = new TestClass("M60 1NW");

			var validationReport = this.validator.Validate(testClass);

			Assert.IsTrue(validationReport.IsValid);
		}

		[Test]
		public void ensure_AANNAA_passes_validation()
		{
			var testClass = new TestClass("CR2 6XH");

			var validationReport = this.validator.Validate(testClass);

			Assert.IsTrue(validationReport.IsValid);
		}

		[Test]
		public void ensure_AANNNAA_passes_validation()
		{
			var testClass = new TestClass("DN55 1PT");

			var validationReport = this.validator.Validate(testClass);

			Assert.IsTrue(validationReport.IsValid);
		}

		[Test]
		public void ensure_ANANAA_passes_validation()
		{
			var testClass = new TestClass("W1A 1HQ");

			var validationReport = this.validator.Validate(testClass);

			Assert.IsTrue(validationReport.IsValid);
		}

		[Test]
		public void ensure_AANANAA_passes_validation()
		{
			var testClass = new TestClass("EC1A 1BB");

			var validationReport = this.validator.Validate(testClass);

			Assert.IsTrue(validationReport.IsValid);
		}

		[Test]
		public void ensure_GIR0AA_passes_validation()
		{
			var testClass = new TestClass("GIR 0AA");

			var validationReport = this.validator.Validate(testClass);

			Assert.IsTrue(validationReport.IsValid);
		}

		private class TestClass
		{
			public string Postcode { get; set; }

			public TestClass(string postCode)
			{
				this.Postcode = postCode;
			}
		}

		private class TestClassValidator : Validator<TestClass>
		{
			protected override void Rules()
			{
				Ensure(x => x.Postcode.IsAValidBS7666PostCode());
			}
		}
	}
}