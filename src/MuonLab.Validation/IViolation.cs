using System.Linq.Expressions;

namespace MuonLab.Validation
{
	public interface IViolation
	{
		Expression Property { get; }
		string ErrorMessage { get; }
		object AttemptedValue { get; }
	}
}