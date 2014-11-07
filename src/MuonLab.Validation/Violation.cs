using System.Linq.Expressions;

namespace MuonLab.Validation
{
	public sealed class Violation : IViolation
	{
		public Expression Property { get; set; }
		public ErrorDescriptor Error { get; set; }
		public object AttemptedValue { get; set; }

		public Violation(ErrorDescriptor error, Expression property, object attemptedValue)
		{
			this.Property = property;
			this.Error = error;
			this.AttemptedValue = attemptedValue;
		}
	}
}