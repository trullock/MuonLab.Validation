using System;
using System.Linq;
using Xunit;

namespace MuonLab.Validation.Tests.SemanticString
{
	public class When_validating_a_property_as_an_RFC_valid_email_address
	{
		[Theory]
		[InlineData(@"NotAnEmail", false)]
		[InlineData(@"@NotAnEmail", false)]
		[InlineData(@"""test\\blah""@example.com", true)]
		//[InlineData(@"""test\blah""@example.com", false)]
		[InlineData("\"test\\\rblah\"@example.com", true)]
		[InlineData("\"test\rblah\"@example.com", false)]
		[InlineData(@"""test\""blah""@example.com", true)]
		[InlineData(@"""test""blah""@example.com", false)]
		[InlineData(@"customer/department@example.com", true)]
		[InlineData(@"$A12345@example.com", true)]
		[InlineData(@"!def!xyz%abc@example.com", true)]
		[InlineData(@"_Yosemite.Sam@example.com", true)]
		[InlineData(@"~@example.com", true)]
		[InlineData(@".wooly@example.com", false)]
		[InlineData(@"wo..oly@example.com", false)]
		[InlineData(@"pootietang.@example.com", false)]
		[InlineData(@".@example.com", false)]
		[InlineData(@"""Austin@Powers""@example.com", true)]
		[InlineData(@"Ima.Fool@example.com", true)]
		[InlineData(@"""Ima.Fool""@example.com", true)]
		[InlineData(@"""Ima Fool""@example.com", true)]
		[InlineData(@"Ima Fool@example.com", false)]
		public void ensure_common_things_work(string email, bool result)
		{
			var testClass = new TestClass(email);
			var validationReport = new TestClassValidator().Validate(testClass).Result;
			if (result != validationReport.IsValid)
				Console.WriteLine(email);
			validationReport.IsValid.ShouldEqual(result);
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