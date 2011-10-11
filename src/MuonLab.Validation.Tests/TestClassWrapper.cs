namespace MuonLab.Validation.Tests
{
	public class TestClassWrapper
	{
		public TestClass TestClass { get; set; }

		public TestClassWrapper()
		{
			TestClass = new TestClass();
		}
	}
}