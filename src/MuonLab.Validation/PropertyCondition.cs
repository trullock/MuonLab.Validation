using System;

namespace MuonLab.Validation
{
	public class PropertyCondition<TValue> : ICondition<TValue>
	{
		public Func<TValue, bool> Condition { get; protected set; }
		public string ErrorMessage { get; protected set; }

		public PropertyCondition(Func<TValue, bool> condition, string errorMessage)
		{
			this.Condition = condition;
			this.ErrorMessage = errorMessage;
		}
	}
}