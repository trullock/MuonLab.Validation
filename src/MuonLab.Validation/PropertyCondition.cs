using System;

namespace MuonLab.Validation
{
	public abstract class PropertyCondition
	{
		public string ErrorMessage { get; protected set; }
	}

	public class PropertyCondition<TValue> : PropertyCondition, ICondition<TValue>
	{
		public Func<TValue, bool> Condition { get; protected set; }

		public PropertyCondition(Func<TValue, bool> condition, string errorMessage)
		{
			this.Condition = condition;
			this.ErrorMessage = errorMessage;
		}
	}
}