using System.Collections.Generic;
using NUnit.Framework;

namespace MuonLab.Validation.Tests.Enumerables
{
	public class When_validating_no_duplicates
	{





		public class TestClass
		{
			public IList<InnerClass> List { get; set; }
		}

		public class InnerClass
		{
			public string Name { get; set; }
		}

		public class TestClassValidator : Validator<TestClass>
		{
			private readonly InnerClassValidator innerClassValidator;

			public TestClassValidator()
			{
				this.innerClassValidator = new InnerClassValidator();
			}

			protected override void Rules()
			{
				Ensure(x => x.List.AllSatisfy(innerClassValidator));
			}
		}

		public class InnerClassValidator : Validator<InnerClass>
		{
			protected override void Rules()
			{
				Ensure(x => x.Name.IsNotNullOrEmpty());
			}
		}

	}
}