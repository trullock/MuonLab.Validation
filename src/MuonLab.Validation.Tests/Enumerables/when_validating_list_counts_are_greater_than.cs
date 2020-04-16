using System.Collections.Generic;
using Xunit;

namespace MuonLab.Validation.Tests.Enumerables
{
	public class when_validating_list_counts_are_greater_than
	{
		[Fact]
		public void list_counts_less_than_the_specified_count_should_be_false()
		{
			var testClass = new TestClass();
			testClass.List.Add("foo");

			var validator = new GreaterThanValidator();

			var report = validator.Validate(testClass).Result;
			report.IsValid.ShouldBeFalse();
		}

		[Fact]
		public void list_counts_greater_than_the_specified_count_should_be_true()
		{
			var testClass = new TestClass();
			testClass.List.Add("foo");
			testClass.List.Add("bar");

			var validator = new GreaterThanValidator();

			var report = validator.Validate(testClass).Result;
			report.IsValid.ShouldBeTrue();
		}

		[Fact]
		public void list_counts_less_than_the_specified_predicate_count_should_be_false()
		{
			var testClass = new TestClass();
			testClass.List.Add("foo");
			testClass.List.Add("bar");

			var validator = new GreaterThanWithPredicateValidator();

			var report = validator.Validate(testClass).Result;
			report.IsValid.ShouldBeFalse();
		}

		[Fact]
		public void list_counts_greater_than_the_specified_predicate_count_should_be_true()
		{
			var testClass = new TestClass();
			testClass.List.Add("foo");
			testClass.List.Add("bar");
			testClass.List.Add("baz");

			var validator = new GreaterThanWithPredicateValidator();

			var report = validator.Validate(testClass).Result;
			report.IsValid.ShouldBeTrue();
		}

		public class TestClass
		{
			public IList<string> List { get; } = new List<string>();
		}

		public class GreaterThanValidator : Validator<TestClass>
		{
			protected override void Rules()
			{
				this.Ensure(x => x.List.HasCountGreaterThan(1));
			}
		}

		public class GreaterThanWithPredicateValidator : Validator<TestClass>
		{
			protected override void Rules()
			{
				this.Ensure(x => x.List.HasCountGreaterThan(y => y.StartsWith("b"), 1));
			}
		}
	}
}