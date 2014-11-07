using System.Globalization;

namespace MuonLab.Validation
{
	public interface IErrorMessageResolver
	{
		string GetErrorMessage(ErrorDescriptor error, CultureInfo culture);
	}
}