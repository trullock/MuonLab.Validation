using System.Linq.Expressions;

namespace MuonLab.Validation
{
	public interface IViolation
	{
		Expression Property { get; }
		ErrorDescriptor Error { get; }
		object AttemptedValue { get; }
	}
}