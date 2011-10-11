using System;
using System.Collections.Generic;

namespace MuonLab.Validation.Tests
{
	public class TestClass
	{
		public int Age { get; set; }
		public string Name { get; set; }
		public DateTime DateOfBirth { get; set; }
		public IEnumerable<object> List { get; set; }
	}
}