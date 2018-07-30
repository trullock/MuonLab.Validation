using System;
using System.Linq;
using Xunit;

namespace MuonLab.Validation.Tests.String
{
	public class When_validating_a_property_as_not_equal_to
	{
		[Fact]
		public void ensure_mismatch_fail_validation()
		{
			var validator = new TestClassValidator();
			var testClass = new TestClass("HeLlo");

			var validationReport = validator.Validate(testClass);

			validationReport.Violations.First().Error.Key.ShouldEqual("error");
		}

		[Fact]
		public void ensure_match_passes_validation()
		{
			var validator = new TestClassValidator();
			var testClass = new TestClass("different");

			var validationReport = validator.Validate(testClass);

			validationReport.IsValid.ShouldBeTrue();
		}

		[Fact]
		public void ensure_one_null_value_pass_validation()
		{
			var validator = new TestClassValidator();
			var testClass = new TestClass(null);

			var validationReport = validator.Validate(testClass);

			validationReport.IsValid.ShouldBeTrue();
		}

		[Fact]
		public void ensure_matching_null_values_fail_validation()
		{
			var validatorWithTestClassValidatorWithNull = new TestClassValidatorWithNullValueParameter();
			var testClass = new TestClass(null);

			var validationReport = validatorWithTestClassValidatorWithNull.Validate(testClass);

			validationReport.Violations.First().Error.Key.ShouldEqual("error");
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
				Ensure(x => x.value.IsNotEqualTo("hello", StringComparison.InvariantCultureIgnoreCase, "error"));
			}
		}

		private class TestClassValidatorWithNullValueParameter : Validator<TestClass>
		{
			protected override void Rules()
			{
				Ensure(x => x.value.IsNotEqualTo(null, StringComparison.InvariantCultureIgnoreCase, "error"));
			}
		}
	}
}