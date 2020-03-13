using System.Linq;
using Xunit;

namespace MuonLab.Validation.Tests
{
	public class Custom_error_messages
	{
		[Fact]
		public void ensure_true_returns_true()
		{
			var validator = new TestClassValidator();
			var testClass = new TestClass(true);

			var validationReport = validator.Validate(testClass);

			validationReport.IsValid.ShouldBeTrue();
		}

		[Fact]
		public void ensure_false_returns_false()
		{
			var validator = new TestClassValidator();
			var testClass = new TestClass(false);

			var validationReport = validator.Validate(testClass);

			var violations = validationReport.Violations.ToArray();

			violations[0].Error.Key.ShouldEqual("custom key");
		}

		private class TestClass
		{
			public bool value { get; set; }

			public TestClass(bool value)
			{
				this.value = value;
			}
		}
		private class TestClassValidator : Validator<TestClass>
		{
			protected override void Rules()
			{
				Ensure(x => x.value.Satisfies(v => new ConditionResult(v == true, "custom key")));
			}
		}
	}
}