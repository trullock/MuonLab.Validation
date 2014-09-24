using System;
using System.Linq;
using NUnit.Framework;

namespace MuonLab.Validation.Tests.String
{
	[TestFixture]
	public class When_validating_a_property_as_equal_to
	{
		private TestClassValidator validator;

		[SetUp]
		public void SetUp()
		{
			this.validator = new TestClassValidator();
		}

		[Test]
		public void ensure_mismatch_fail_validation()
		{
			var testClass = new TestClass("different");

			var validationReport = this.validator.Validate(testClass);

			validationReport.Violations.First().Error.Key.ShouldEqual("error");
		}


		[Test]
		public void ensure_match_passes_validation()
		{
			var testClass = new TestClass("HeLlO");

			var validationReport = this.validator.Validate(testClass);

			Assert.IsTrue(validationReport.IsValid);
		}


		private class TestClass
		{
			public string value { get; set; }

			public TestClass(string value)
			{
				this.value = value;
			}
		}

		private class TestClassValidator : Validator<TestClass>
		{
			protected override void Rules()
			{
				Ensure(x => x.value.IsEqualTo("hello", StringComparison.InvariantCultureIgnoreCase, "error"));
			}
		}
	}
}