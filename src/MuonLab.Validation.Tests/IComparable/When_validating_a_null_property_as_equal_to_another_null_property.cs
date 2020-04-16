using System.Linq;
using Xunit;

namespace MuonLab.Validation.Tests.IComparable
{
	public class When_validating_a_null_property_as_equal_to_another_null_property
	{
		[Fact]
		public void should_be_valid()
		{
			var validator = new TestClassValidator();
			var testClass = new TestClass();

			var validationReport = validator.Validate(testClass).Result;

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