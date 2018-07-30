using System.Linq;
using Xunit;

namespace MuonLab.Validation.Tests.Nullable
{
	public class When_validating_a_nullable_property
	{
		[Fact]
		public void ensure_false_returns_false()
		{
			var validator = new TestClassValidator();
			var testClass = new TestClass(false);

			var validationReport = validator.Validate(testClass);

			validationReport.Violations.First().Error.Key.ShouldEqual("BeTrue");
		}

		[Fact]
		public void ensure_true_returns_true()
		{
			var validator = new TestClassValidator();
			var testClass = new TestClass(true);

			var validationReport = validator.Validate(testClass);

			validationReport.IsValid.ShouldBeTrue();
		}

		[Fact]
		public void ensure_null_returns_true()
		{
			var validator = new TestClassValidator();
			var testClass = new TestClass(null);

			var validationReport = validator.Validate(testClass);

			validationReport.IsValid.ShouldBeTrue();
		}

		private class TestClass
		{
			public bool? NullableBool { get; set; }

			public TestClass(bool? value)
			{
				this.NullableBool = value;
			}
		}

		private class TestClassValidator : Validator<TestClass>
		{
			protected override void Rules()
			{
				When(x => x.NullableBool.HasValue(), () => Ensure(x => x.NullableBool.Value.IsTrue()));
			}
		}
	}
}