using System.Linq;
using Xunit;

namespace MuonLab.Validation.Tests.IComparable
{
	public class When_validating_a_property_has_greater_than_or_equal_to_a_scalar
	{
		[Fact]
		public void test_1_greater_than_or_equal_4_returns_false()
		{
			var validator = new TestClassValidator();
			var testClass = new TestClass(1);

			var validationReport = validator.Validate(testClass).Result;
			validationReport.Violations.First().Error.Key.ShouldEqual("GreaterThanEq");
			validationReport.Violations.First().Error.Replacements["arg0"].Value.ShouldEqual("4");
		}

		[Fact]
		public void test_4_greater_than_or_equal_1_returns_true()
		{
			var validator = new TestClassValidator();
			var testClass = new TestClass(4);

			var validationReport = validator.Validate(testClass).Result;

			validationReport.IsValid.ShouldBeTrue();
		}

		[Fact]
		public void test_4_greater_than_or_equal_4_returns_true()
		{
			var validator = new TestClassValidator();
			var testClass = new TestClass(4);

			var validationReport = validator.Validate(testClass).Result;

			validationReport.IsValid.ShouldBeTrue();
		}

		private class TestClass
		{
			public int Value { get; set; }

			public TestClass(int value)
			{
				this.Value = value;
			}
		}

		private class TestClassValidator : Validator<TestClass>
		{
			protected override void Rules()
			{
				Ensure(x => x.Value.IsGreaterThanOrEqualTo(4));
			}
		}
	}
}