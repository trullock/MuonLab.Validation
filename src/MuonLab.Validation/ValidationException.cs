using System;

namespace MuonLab.Validation
{
	public sealed class ValidationException : Exception
	{
		public ValidationReport Report { get; private set; }
		public ValidationException(ValidationReport report)
		{
			this.Report = report;
		}

		public ValidationException(IViolation violation)
		{
			this.Report = new ValidationReport(new[] {violation});
		}
	}
}