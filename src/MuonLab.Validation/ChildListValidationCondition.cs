using System.Collections.Generic;

namespace MuonLab.Validation
{
	public sealed class ChildListValidationCondition<TValue> : ICondition<IList<TValue>>
	{
		public IValidator<TValue> Validator { get; protected set; }

		public ChildListValidationCondition(IValidator<TValue> validator)
		{
			this.Validator = validator;
		}
	}
}