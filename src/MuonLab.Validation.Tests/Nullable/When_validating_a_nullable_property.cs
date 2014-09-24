using System.Linq;
using NUnit.Framework;

namespace MuonLab.Validation.Tests.Nullable
{
	[TestFixture]
	public class When_validating_a_nullable_property
	{
		private TestClassValidator validator;

		[SetUp]
		public void SetUp()
		{
			this.validator = new TestClassValidator();
		}

		[Test]
		public void ensure_false_returns_false()
		{
			var testClass = new TestClass(false);

			var validationReport = this.validator.Validate(testClass);

			validationReport.Violations.First().Error.Key.ShouldEqual("BeTrue");
		}

		[Test]
		public void ensure_true_returns_true()
		{
			var testClass = new TestClass(true);

			var validationReport = this.validator.Validate(testClass);

			validationReport.IsValid.ShouldBeTrue();
		}

		[Test]
		public void ensure_null_returns_true()
		{
			var testClass = new TestClass(null);

			var validationReport = this.validator.Validate(testClass);

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