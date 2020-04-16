using System.Linq;
using Xunit;

namespace MuonLab.Validation.Tests.ReferenceType
{
	public class When_validating_a_property_as_not_null
	{
		[Fact]
		public void ensure_not_null_returns_true()
		{
			var validator = new TestClassValidator();
			var testClass = new TestClass(new object());

			var validationReport = validator.Validate(testClass).Result;

			validationReport.IsValid.ShouldBeTrue();
		}

		[Fact]
		public void ensure_not_null_returns_false()
		{
			var validator = new TestClassValidator();
			var testClass = new TestClass(null);

			var validationReport = validator.Validate(testClass).Result;

			validationReport.Violations.First().Error.Key.ShouldEqual("Required");
			validationReport.Violations.Skip(1).First().Error.Key.ShouldEqual("test key");
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
				Ensure(x => x.value.IsNotNull("test key"));
			}
		}
	}
}