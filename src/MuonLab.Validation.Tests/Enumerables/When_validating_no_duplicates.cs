using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using NUnit.Framework;

namespace MuonLab.Validation.Tests.Enumerables
{
	public class When_validating_no_duplicates
	{
		[Test]
		public void DuplicateRecordsShouldFail()
		{
			var testClass = new TestClass
			{
				List = new[] { new InnerClass { DupeVal = "hello" }, new InnerClass { DupeVal = "hello" }, new InnerClass() }
			};

			var testClassValidator = new TestClassValidator();

			var validationReport = testClassValidator.Validate(testClass);

			validationReport.IsValid.ShouldBeFalse();

			var violations = validationReport.Violations.ToArray();

			var error1 = ReflectionHelper.PropertyChainToString(violations[0].Property, '.');

			error1.ShouldEqual("List[0].DupeVal");

			var error2 = ReflectionHelper.PropertyChainToString(violations[1].Property, '.');

			error2.ShouldEqual("List[1].DupeVal");
		}

		[Test]
		public void PropertyChainShouldContainPrefixes()
		{
			var testContainer = new OuterTestClass
			{
				TestClasses = new []
				{
					new TestClass
					{
						List = new[] {new InnerClass {DupeVal = "hello"}, new InnerClass {DupeVal = "hello"}, new InnerClass()}
					},
					new TestClass
					{
						List = new[] {new InnerClass {DupeVal = "hello1"}, new InnerClass {DupeVal = "hello2"}, new InnerClass()}
					},
					new TestClass
					{
						List = new[] {new InnerClass {DupeVal = "hello"}, new InnerClass {DupeVal = "hello"}, new InnerClass {DupeVal = "hello"}}
					}
				}
			};

			var outerTestClassValidator = new OuterTestClassValidator();

			var validationReport = outerTestClassValidator.Validate(testContainer);

			validationReport.IsValid.ShouldBeFalse();

			var violations = validationReport.Violations.ToArray();

			violations.Count().ShouldEqual(5);

			var error1 = ReflectionHelper.PropertyChainToString(violations[0].Property, '.');

			error1.ShouldEqual("TestClasses[0].List[0].DupeVal");

			var error3 = ReflectionHelper.PropertyChainToString(violations[3].Property, '.');

			error3.ShouldEqual("TestClasses[2].List[1].DupeVal");
		}

		[Test]
		public void NonDuplicateRecordsShouldPass()
		{
			var testClass = new TestClass
			{
				List = new[] { new InnerClass { DupeVal = "hello0" }, new InnerClass { DupeVal = "hello1" }, new InnerClass { DupeVal = "hello2" } }
			};

			var testClassValidator = new TestClassValidator();

			var validationReport = testClassValidator.Validate(testClass);
			
			validationReport.IsValid.ShouldBeTrue();
		}


		public class TestClass
		{
			public IList<InnerClass> List { get; set; }
		}

		public class OuterTestClass
		{
			public IList<TestClass> TestClasses { get; set; }
		}

		public class InnerClass
		{
			public string DupeVal { get; set; }
		}

		public class TestClassValidator : Validator<TestClass>
		{
			protected override void Rules()
			{
				Ensure(x => x.List.DoesNotHaveDuplicates(y => y.DupeVal));
			}
		}

		public class OuterTestClassValidator : Validator<OuterTestClass>
		{
			protected override void Rules()
			{
				Ensure(x => x.TestClasses.AllSatisfy(new TestClassValidator()));
			}
		}
	}
}