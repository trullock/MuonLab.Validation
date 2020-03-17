using System.Collections.Generic;

namespace MuonLab.Validation
{
	public sealed class ChildListValidationCondition<TValue> : ICondition<IList<TValue>>
	{
		public IValidator<TValue> Validator { get; }
		public bool IgnoreDefaultValues { get; }

		public ChildListValidationCondition(IValidator<TValue> validator, bool ignoreDefaultValues)
		{
			this.Validator = validator;
			this.IgnoreDefaultValues = ignoreDefaultValues;
		}
	}
}