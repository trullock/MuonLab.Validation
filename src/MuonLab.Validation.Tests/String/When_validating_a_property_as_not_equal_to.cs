using System;
using System.Linq;
using NUnit.Framework;

namespace MuonLab.Validation.Tests.String
{
	[TestFixture]
	public class When_validating_a_property_as_not_equal_to
	{
		private TestClassValidator validator;
		private TestClassValidatorWithNullValueParameter validatorWithTestClassValidatorWithNull;

		[SetUp]
		public void SetUp()
		{
			this.validator = new TestClassValidator();
			this.validatorWithTestClassValidatorWithNull = new TestClassValidatorWithNullValueParameter();
		}

		[Test]
		public void ensure_mismatch_fail_validation()
		{
			var testClass = new TestClass("HeLlo");

			var validationReport = this.validator.Validate(testClass);

			validationReport.Violations.First().Error.Key.ShouldEqual("error");
		}

		[Test]
		public void ensure_match_passes_validation()
		{
			var testClass = new TestClass("different");

			var validationReport = this.validator.Validate(testClass);

			Assert.IsTrue(validationReport.IsValid);
		}

		[Test]
		public void ensure_one_null_value_pass_validation()
		{
			var testClass = new TestClass(null);

			var validationReport = this.validator.Validate(testClass);

			Assert.IsTrue(validationReport.IsValid);
		}

		[Test]
		public void ensure_matching_null_values_fail_validation()
		{
			var testClass = new TestClass(null);

			var validationReport = this.validatorWithTestClassValidatorWithNull.Validate(testClass);

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
