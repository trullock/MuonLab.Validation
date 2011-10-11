using System;
using NUnit.Framework;

namespace MuonLab.Validation.Tests
{
	[TestFixture]
	public abstract class given_a_test_class_with_data
	{
		private TestClass testClass;

		[SetUp]
		public void SetUp()
		{
			this.testClass = new TestClass();
			this.testClass.Age = 18;
			this.testClass.DateOfBirth = new DateTime(2008, 1, 1);
			this.testClass.Name = "Andrew";
		}
	}
}