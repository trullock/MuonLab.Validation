namespace MuonLab.Validation
{
	public sealed class ConditionResult
	{
		public bool Valid { get; }
		public string ErrorKey { get; }

		public ConditionResult(bool valid, string errorKey)
		{
			Valid = valid;
			ErrorKey = errorKey;
		}
	}
}