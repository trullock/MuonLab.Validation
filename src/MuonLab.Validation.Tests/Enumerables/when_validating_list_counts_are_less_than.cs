using System.Collections.Generic;
using Xunit;

namespace MuonLab.Validation.Tests.Enumerables
{
	public class when_validating_list_counts_are_less_than
	{
		[Fact]
		public void list_counts_greater_than_the_specified_count_should_be_false()
		{
			var testClass = new TestClass();
			testClass.List.Add("foo");
			testClass.List.Add("foo");
			testClass.List.Add("foo");

			var validator = new LessThanValidator();

			var report = validator.Validate(testClass);
			report.IsValid.ShouldBeFalse();
		}

		[Fact]
		public void list_counts_less_than_the_specified_count_should_be_true()
		{
			var testClass = new TestClass();
			testClass.List.Add("foo");

			var validator = new LessThanValidator();

			var report = validator.Validate(testClass);
			report.IsValid.ShouldBeTrue();
		}

		[Fact]
		public void list_counts_less_than_the_specified_predicate_count_should_be_true()
		{
			var testClass = new TestClass();
			testClass.List.Add("foo");
			testClass.List.Add("far");
			testClass.List.Add("baz");

			var validator = new LessThanWithPredicateValidator();

			var report = validator.Validate(testClass);
			report.IsValid.ShouldBeTrue();
		}

		[Fact]
		public void list_counts_greater_than_the_specified_predicate_count_should_be_false()
		{
			var testClass = new TestClass();
			testClass.List.Add("foo");
			testClass.List.Add("bar");
			testClass.List.Add("baz");

			var validator = new LessThanWithPredicateValidator();

			var report = validator.Validate(testClass);
			report.IsValid.ShouldBeFalse();
		}

		public class TestClass
		{
			public IList<string> List { get; } = new List<string>();
		}

		public class LessThanValidator : Validator<TestClass>
		{
			protected override void Rules()
			{
				this.Ensure(x => x.List.HasCountLessThan(2));
			}
		}

		public class LessThanWithPredicateValidator : Validator<TestClass>
		{
			protected override void Rules()
			{
				this.Ensure(x => x.List.HasCountLessThan(y => y.StartsWith("b"), 2));
			}
		}
	}
}