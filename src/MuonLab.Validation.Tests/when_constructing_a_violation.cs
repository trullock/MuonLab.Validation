using System;
using System.Linq.Expressions;
using NUnit.Framework;

namespace MuonLab.Validation.Tests
{
	[TestFixture]
	public class when_constructing_a_violation
	{
		private object o;
		private const string errorMessage = "error";
		private readonly Expression<Func<TestClass, string>> property = tc => tc.TestProp;

		[SetUp]
		public void SetUp()
		{
			this.o = new object();
		}

		[Test]
		public void Should_Construct_Correctly()
		{
			var SubjectUnderTest = new Violation(errorMessage, property, this.o);
			Assert.AreEqual(errorMessage, SubjectUnderTest.ErrorMessage);
			Assert.AreEqual(property, SubjectUnderTest.Property);
			Assert.AreEqual(this.o, SubjectUnderTest.AttemptedValue);
		}

		public class TestClass
		{
			public string TestProp { get; set; }
		}
	}
}