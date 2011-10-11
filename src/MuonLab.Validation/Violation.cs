using System.Linq.Expressions;

namespace MuonLab.Validation
{
	public sealed class Violation : IViolation
	{
		public Expression Property { get; set; }
		public string ErrorMessage { get; set; }
		public object AttemptedValue { get; set; }

		public Violation(string errorMessage, Expression property, object attemptedValue)
		{
			this.Property = property;
			this.ErrorMessage = errorMessage;
			this.AttemptedValue = attemptedValue;
		}
	}
}