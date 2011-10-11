using System;
using System.Linq;
using NUnit.Framework;

namespace MuonLab.Validation.Tests.SemanticString
{
	[TestFixture]
	public class When_validating_a_property_as_an_RFC_valid_email_address
	{
		private TestClassValidator validator;

		[SetUp]
		public void SetUp()
		{
			this.validator = new TestClassValidator();
		}


		[Test]
		[TestCase(@"NotAnEmail", false)]
		[TestCase(@"@NotAnEmail", false)]
		[TestCase(@"""test\\blah""@example.com", true)]
		//[TestCase(@"""test\blah""@example.com", false)]
		[TestCase("\"test\\\rblah\"@example.com", true)]
		[TestCase("\"test\rblah\"@example.com", false)]
		[TestCase(@"""test\""blah""@example.com", true)]
		[TestCase(@"""test""blah""@example.com", false)]
		[TestCase(@"customer/department@example.com", true)]
		[TestCase(@"$A12345@example.com", true)]
		[TestCase(@"!def!xyz%abc@example.com", true)]
		[TestCase(@"_Yosemite.Sam@example.com", true)]
		[TestCase(@"~@example.com", true)]
		[TestCase(@".wooly@example.com", false)]
		[TestCase(@"wo..oly@example.com", false)]
		[TestCase(@"pootietang.@example.com", false)]
		[TestCase(@".@example.com", false)]
		[TestCase(@"""Austin@Powers""@example.com", true)]
		[TestCase(@"Ima.Fool@example.com", true)]
		[TestCase(@"""Ima.Fool""@example.com", true)]
		[TestCase(@"""Ima Fool""@example.com", true)]
		[TestCase(@"Ima Fool@example.com", false)]
		public void ensure_common_things_work(string email, bool result)
		{
			var testClass = new TestClass(email);
			var validationReport = new TestClassValidator().Validate(testClass);
			if (result != validationReport.IsValid)
				Console.WriteLine(email);
			Assert.AreEqual(result, validationReport.IsValid, email + " didn't pass");
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
				Ensure(x => x.value.IsAValidEmailAddress());
			}
		}
	}
}