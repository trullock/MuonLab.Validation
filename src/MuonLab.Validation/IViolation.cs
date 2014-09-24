using System.Linq.Expressions;

namespace MuonLab.Validation
{
	public interface IViolation
	{
		Expression Property { get; }
		ErrorDescriptior Error { get; }
		object AttemptedValue { get; }
	}
}