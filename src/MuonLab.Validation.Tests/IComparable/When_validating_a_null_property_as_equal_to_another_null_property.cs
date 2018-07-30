using System.Linq;
using Xunit;

namespace MuonLab.Validation.Tests.IComparable
{
	public class When_validating_a_null_property_as_equal_to_another_null_property
	{
		private TestClassValidator validator;

		[SetUp]
		public void SetUp()
		{
			this.validator = new TestClassValidator();
		}

		[Fact]
		public void should_be_valid()
		{
			var testClass = new TestClass();

			var validationReport = this.validator.Validate(testClass);

			validationReport.IsValid.ShouldBeTrue();
		}


		private class TestClass
		{
			public string Value { get; set; }
			public string Value2 { get; set; }
		}

		private class TestClassValidator : Validator<TestClass>
		{
			protected override void Rules()
			{
				Ensure(x => x.Value.IsEqualTo(x.Value2));
			}
		}
	}
}