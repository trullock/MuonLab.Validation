using System.Collections.Generic;
using System.Linq;

namespace MuonLab.Validation
{
	public sealed class ValidationReport
	{
		public IEnumerable<IViolation> Violations { get; protected set; }

		public ValidationReport(IEnumerable<IViolation> violations)
		{
			this.Violations = violations;
		}

		public ValidationReport()
		{
			this.Violations = new List<IViolation>();
		}

		public bool IsValid
		{
			get
			{
				return !this.Violations.Any();
			}
		}
	}
}