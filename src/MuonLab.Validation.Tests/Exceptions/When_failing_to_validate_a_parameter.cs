using System;
using System.Linq;
using Xunit;

namespace MuonLab.Validation.Tests.Exceptions
{
	public class When_failing_to_validate_a_parameter
	{
		[Fact]
		public void ensure_exception_is_caught_and_reported()
		{
			var validator = new TestClassValidator();
			var testClass = new TestClass();

			var validationReport = validator.Validate(testClass);

			var errorDescriptor = validationReport.Violations.First().Error;
			errorDescriptor.Key.ShouldEqual("ValidationError");
		}


		private class TestClass
		{
		}

		private class TestClassValidator : Validator<TestClass>
		{
			protected override void Rules()
			{
				Ensure(x => x.Satisfies(b => Throw(), "foo"));
			}

			static bool Throw()
			{
				throw new Exception();
			}
		}
	}
}