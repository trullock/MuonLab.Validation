using System.Linq;
using Xunit;

namespace MuonLab.Validation.Tests.Enums
{
	public class When_validating_a_property_as_a_valid_enum
	{
		[Fact]
		public void ensure_false_returns_true()
		{
			var validator = new TestClassValidator();
			var testClass = new TestClass(TestClass.MyEnum.One);

			var validationReport = validator.Validate(testClass).Result;

			validationReport.IsValid.ShouldBeTrue();
		}

		[Fact]
		public void ensure_true_returns_false()
		{
			var validator = new TestClassValidator();
			var testClass = new TestClass((TestClass.MyEnum)5);

			var validationReport = validator.Validate(testClass).Result;

			validationReport.Violations.First().Error.Key.ShouldEqual("Invalid");
		}

		private class TestClass
		{
			public MyEnum enumm { get; set; }

			public TestClass(MyEnum enumm)
			{
				this.enumm = enumm;
			}

			public enum MyEnum
			{
				One = 1,
				Two = 2
			}
		}

		private class TestClassValidator : Validator<TestClass>
		{
			protected override void Rules()
			{
				Ensure(x => x.enumm.IsAValidEnumValue());
			}
		}
	}
}