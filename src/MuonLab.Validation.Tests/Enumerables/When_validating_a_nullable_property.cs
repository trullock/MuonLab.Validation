using System.Collections;
using Xunit;

namespace MuonLab.Validation.Tests.Enumerables
{
	public class when_validating_an_enumerable_contains_elements
	{
		[Fact]
		public void an_empty_list_should_be_false()
		{
			var validator = new TestClassValidator();
			var testClass = new TestClass();

			var report = validator.Validate(testClass);

			report.IsValid.ShouldBeFalse();
		}


		[Fact]
		public void an_non_empty_list_should_be_true()
		{
			var validator = new TestClassValidator();
			var testClass = new TestClass
			{
				List = new[] {"an item"}
			};

			var report = validator.Validate(testClass);

			report.IsValid.ShouldBeTrue();
		}

		private class TestClass
		{
			public IEnumerable List { get; set; }
		}

		private class TestClassValidator : Validator<TestClass>
		{
			protected override void Rules()
			{
				Ensure(x => x.List.ContainsElements());
			}
		}
	}
}