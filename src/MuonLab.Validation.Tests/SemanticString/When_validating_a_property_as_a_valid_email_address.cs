using System.Linq;
using Xunit;

namespace MuonLab.Validation.Tests.SemanticString
{
	public class When_validating_a_property_as_a_valid_email_address
	{
		[Fact]
		public void ensure_nulls_fail_validation()
		{
			var validator = new TestClassValidator();
			var testClass = new TestClass(null);

			var validationReport = validator.Validate(testClass).Result;

			validationReport.Violations.First().Error.Key.ShouldEqual("ValidEmail");
		}

		[Fact]
		public void ensure_empty_string_fail_validation()
		{
			var validator = new TestClassValidator();
			var testClass = new TestClass(string.Empty);

			var validationReport = validator.Validate(testClass).Result;

			validationReport.Violations.First().Error.Key.ShouldEqual("ValidEmail");
		}

		[Fact]
		public void ensure_valid_email_passes_validation()
		{
			var validator = new TestClassValidator();
			var testClass = new TestClass("trullock@gmail.com");

			var validationReport = validator.Validate(testClass).Result;

			validationReport.IsValid.ShouldBeTrue();
		}

		[Fact]
		public void ensure_invalid_email2_fails_validation()
		{
			var validator = new TestClassValidator();
			var testClass = new TestClass("trullock@gmail@com");
			var validationReport = validator.Validate(testClass).Result;

			validationReport.Violations.First().Error.Key.ShouldEqual("ValidEmail");
		}

		[Fact]
		public void ensure_invalid_email3_fails_validation()
		{
			var validator = new TestClassValidator();
			var testClass = new TestClass("muonlab.com");
			var validationReport = validator.Validate(testClass).Result;

			validationReport.Violations.First().Error.Key.ShouldEqual("ValidEmail");
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