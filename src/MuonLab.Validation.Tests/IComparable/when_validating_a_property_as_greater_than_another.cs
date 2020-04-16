using System.Linq;
using Xunit;

namespace MuonLab.Validation.Tests.IComparable
{
	public class when_validating_a_property_as_greater_than_another
	{
		[Fact]
		public void test_1_greater_than_4_returns_false()
		{
			var validator = new TestClassValidator();
			var testClass = new TestClass(1, 4);

			var validationReport = validator.Validate(testClass).Result;

			validationReport.Violations.First().Error.Key.ShouldEqual("GreaterThan");
			validationReport.Violations.First().Error.Replacements["arg0"].Value.ToString().ShouldEqual("x.Value2");
		}

		[Fact]
		public void test_4_greater_than_1_returns_true()
		{
			var validator = new TestClassValidator();
			var testClass = new TestClass(4, 1);

			var validationReport = validator.Validate(testClass).Result;

			validationReport.IsValid.ShouldBeTrue();
		}

		[Fact]
		public void test_2_greater_than_2_returns_false()
		{
			var validator = new TestClassValidator();
			var testClass = new TestClass(2, 2);

			var validationReport = validator.Validate(testClass).Result;

			validationReport.Violations.First().Error.Key.ShouldEqual("GreaterThan");
			validationReport.Violations.First().Error.Replacements["arg0"].Value.ToString().ShouldEqual("x.Value2");
		}

		private class TestClass
		{
			public int value { get; set; }
			public int Value2 { get; set; }

			public TestClass(int value, int value2)
			{
				this.value = value;
				this.Value2 = value2;
			}
		}

		private class TestClassValidator : Validator<TestClass>
		{
			protected override void Rules()
			{
				Ensure(x => x.value.IsGreaterThan(x.Value2));
			}
		}
	}
}