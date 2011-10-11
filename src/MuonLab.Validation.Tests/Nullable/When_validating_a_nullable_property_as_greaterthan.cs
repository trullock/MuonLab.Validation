using System.Linq;
using NUnit.Framework;

namespace MuonLab.Validation.Tests.Nullable
{
	[TestFixture]
	public class When_validating_a_nullable_property_as_greaterthan
	{
		private TestClassValidator validator;

		[SetUp]
		public void SetUp()
		{
			this.validator = new TestClassValidator();
		}

		[Test]
		public void ensure_0_returns_false()
		{
			var testClass = new TestClass(0);

			var validationReport = this.validator.Validate(testClass);
			var violations = validationReport.Violations.ToArray();

			violations[0].ErrorMessage.ShouldEqual("Nullable int must be greater than or equal to 1");
		}

		[Test]
		public void ensure_1_returns_true()
		{
			var testClass = new TestClass(1);

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
			public int? NullableInt { get; set; }

			public TestClass(int? value)
			{
				this.NullableInt = value;
			}
		}

		private class TestClassValidator : Validator<TestClass>
		{
			protected override void Rules()
			{
				When(x => x.NullableInt.HasValue(), () => Ensure(x => x.NullableInt.Value.IsGreaterThanOrEqualTo(1)));
			}
		}
	}
}