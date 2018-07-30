using System;
using System.Linq;
using Xunit;

namespace MuonLab.Validation.Tests.String
{
	public class When_validating_a_property_as_equal_to
	{
		[Fact]
		public void ensure_mismatch_fail_validation()
		{
			var validator = new TestClassValidator();
			var testClass = new TestClass("different");

			var validationReport = validator.Validate(testClass);

			validationReport.Violations.First().Error.Key.ShouldEqual("error");
		}


		[Fact]
		public void ensure_match_passes_validation()
		{
			var validator = new TestClassValidator();
			var testClass = new TestClass("HeLlO");

			var validationReport = validator.Validate(testClass);

			validationReport.IsValid.ShouldBeTrue();
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
				Ensure(x => x.value.IsEqualTo("hello", StringComparison.InvariantCultureIgnoreCase, "error"));
			}
		}
	}
}