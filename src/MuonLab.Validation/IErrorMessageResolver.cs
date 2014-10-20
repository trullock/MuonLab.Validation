using System.Globalization;

namespace MuonLab.Validation
{
	public interface IErrorMessageResolver
	{
		string GetErrorMessage(ErrorDescriptior error, CultureInfo culture);
	}
}