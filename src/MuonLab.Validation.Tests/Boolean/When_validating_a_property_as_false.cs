using System.Linq;
using NUnit.Framework;

namespace MuonLab.Validation.Tests.Boolean
{
	[TestFixture]
	public class When_validating_a_property_as_false
	{
		private TestClassValidator validator;

		[SetUp]
		public void SetUp()
		{
			this.validator = new TestClassValidator();
		}

		[Test]
		public void ensure_false_returns_true()
		{
			var testClass = new TestClass(false);

			var validationReport = this.validator.Validate(testClass);

			Assert.IsTrue(validationReport.IsValid);
		}

		[Test]
		public void ensure_true_returns_false()
		{
			var testClass = new TestClass(true);

			var validationReport = this.validator.Validate(testClass);

			validationReport.Violations.First().Error.Key.ShouldEqual("BeFalse");
			validationReport.Violations.First().Error.Replacements["prop"].ShouldEqual("value");
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
				Ensure(x => x.value.IsFalse());
			}
		}
	}
}