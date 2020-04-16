using System.Linq;
using Xunit;

namespace MuonLab.Validation.Tests.IComparable
{
	public class When_validating_a_property_as_not_equal_to_a_scalar
	{
		[Fact]
		public void test_1_not_equals_4_returns_true()
		{
			var validator = new TestClassValidator();
			var testClass = new TestClass(1);

			var validationReport = validator.Validate(testClass).Result;

			validationReport.IsValid.ShouldBeTrue();
		}

		[Fact]
		public void test_8_not_equals_4_returns_true()
		{
			var validator = new TestClassValidator();
			var testClass = new TestClass(8);

			var validationReport = validator.Validate(testClass).Result;

			validationReport.IsValid.ShouldBeTrue();
		}

		[Fact]
		public void test_4_not_equals_4_returns_false()
		{
			var validator = new TestClassValidator();
			var testClass = new TestClass(4);

			var validationReport = validator.Validate(testClass).Result;

			validationReport.Violations.First().Error.Key.ShouldEqual("NotEqualTo");
			validationReport.Violations.First().Error.Replacements["arg0"].Value.ShouldEqual("4");
		}

		private class TestClass
		{
			public int value { get; set; }

			public TestClass(int value)
			{
				this.value = value;
			}
		}

		private class TestClassValidator : Validator<TestClass>
		{
			protected override void Rules()
			{
				Ensure(x => x.value.IsNotEqualTo(4));
			}
		}
	}
}