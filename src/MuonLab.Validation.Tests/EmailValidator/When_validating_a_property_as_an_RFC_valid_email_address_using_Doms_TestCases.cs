using System.Text;
using System.Xml;
using NUnit.Framework;

namespace MuonLab.Validation.Tests.EmailValidator
{
	[TestFixture]
	public class When_validating_an_email_address
	{
		private XmlElement tests;

		[SetUp]
		public void SetUp()
		{

			var manifestResourceStream = this.GetType().Assembly.GetManifestResourceStream("MuonLab.Validation.Tests.EmailValidator.tests.xml");
			var xmlDocument = new XmlDocument();
			xmlDocument.Load(manifestResourceStream);
			this.tests = xmlDocument.DocumentElement;
		}

		[Test]
		public void ensure_common_things_work()
		{
			foreach(XmlElement test in tests.SelectNodes("test"))
			{
				var address = test.SelectSingleNode("address").InnerText;
				// replace nulls in xml
				address = address.Replace("&#x2400;", Encoding.Unicode.GetChars(new[] {(byte) 0,})[0].ToString());

				var result = bool.Parse(test.SelectSingleNode("valid").InnerText);

				Assert.AreEqual(result, new Validation.EmailValidator().IsEmailValid(address), address);
			}
		}

	}
}