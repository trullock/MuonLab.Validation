using System.Text;
using System.Xml;
using Xunit;

namespace MuonLab.Validation.Tests.EmailValidator
{
	public class When_validating_an_email_address
	{
		[Fact]
		public void ensure_common_things_work()
		{
			var manifestResourceStream = this.GetType().Assembly.GetManifestResourceStream("MuonLab.Validation.Tests.EmailValidator.tests.xml");
			var xmlDocument = new XmlDocument();
			xmlDocument.Load(manifestResourceStream);
			var tests = xmlDocument.DocumentElement;

			foreach (XmlElement test in tests.SelectNodes("test"))
			{
				var address = test.SelectSingleNode("address").InnerText;
				// replace nulls in xml
				address = address.Replace("&#x2400;", Encoding.Unicode.GetChars(new[] {(byte) 0,})[0].ToString());

				var result = bool.Parse(test.SelectSingleNode("valid").InnerText);

				result.ShouldEqual(new Validation.EmailValidator().IsEmailValid(address));
			}
		}
	}
}