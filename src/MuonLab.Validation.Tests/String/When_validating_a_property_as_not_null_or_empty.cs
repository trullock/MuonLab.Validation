using System.Linq;
using Xunit;

namespace MuonLab.Validation.Tests.String
{
	public class When_validating_a_property_as_not_null_or_empty
	{
		[Fact]
		public void ensure_nulls_fail_validation()
		{
			var validator = new TestClassValidator();
			var testClass = new TestClass(null);

			var validationReport = validator.Validate(testClass).Result;
			validationReport.Violations.First().Error.Key.ShouldEqual("Required");
		}

		[Fact]
		public void ensure_empty_string_fail_validation()
		{
			var validator = new TestClassValidator();
			var testClass = new TestClass(string.Empty);

			var validationReport = validator.Validate(testClass).Result;

			validationReport.Violations.First().Error.Key.ShouldEqual("Required");
		}

		[Fact]
		public void ensure_not_null_or_empty_passes_validation()
		{
			var validator = new TestClassValidator();
			var testClass = new TestClass("a");

			var validationReport = validator.Validate(testClass).Result;

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
				Ensure(x => x.value.IsNotNullOrEmpty());
			}
		}
	}
}