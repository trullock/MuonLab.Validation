using System;
using System.Runtime.Serialization;

namespace MuonLab.Validation
{
	[Serializable]
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

		protected ValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}