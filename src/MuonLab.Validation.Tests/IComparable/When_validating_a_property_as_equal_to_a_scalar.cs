using System.Linq;
using Xunit;

namespace MuonLab.Validation.Tests.IComparable
{
	public class When_validating_a_property_as_equal_to_a_scalar
	{
		[Fact]
		public void test_1_equals_4_returns_false()
		{
			var validator = new TestClassValidator();
			var testClass = new TestClass(1);

			var validationReport = validator.Validate(testClass).Result;

			validationReport.Violations.First().Error.Key.ShouldEqual("EqualTo");
			validationReport.Violations.First().Error.Replacements["arg0"].Value.ShouldEqual("4");
		}

		[Fact]
		public void test_8_equals_4_returns_false()
		{
			var validator = new TestClassValidator();
			var testClass = new TestClass(8);

			var validationReport = validator.Validate(testClass).Result;

			validationReport.IsValid.ShouldBeFalse();
		}

		[Fact]
		public void test_4_equals_4_returns_true()
		{
			var validator = new TestClassValidator();
			var testClass = new TestClass(4);

			var validationReport = validator.Validate(testClass).Result;

			validationReport.IsValid.ShouldBeTrue();
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
				Ensure(x => x.value.IsEqualTo(4));
			}
		}
	}
}