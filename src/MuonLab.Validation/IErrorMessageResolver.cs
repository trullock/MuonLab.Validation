using System.Globalization;

namespace MuonLab.Validation
{
	public interface IErrorMessageResolver
	{
		string GetErrorMessage(string key, CultureInfo culture);
	}
}