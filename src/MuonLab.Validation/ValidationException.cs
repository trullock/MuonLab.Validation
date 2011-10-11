using System;

namespace MuonLab.Validation
{
	public sealed class ValidationException : Exception
	{
		public ValidationReport Report { get; private set; }

		internal ValidationException(ValidationReport report)
		{
			this.Report = report;
		}

		internal ValidationException(IViolation violation)
		{
			this.Report = new ValidationReport(new[] {violation});
		}
	}
}