using System.Collections.Generic;

namespace MuonLab.Validation
{
	public sealed class ErrorDescriptior
	{
		public readonly string Key;
		public readonly IDictionary<string, Replacement> Replacements;

		public ErrorDescriptior(string key, IDictionary<string, Replacement> replacements)
		{
			this.Key = key;
			this.Replacements = replacements;
		}

		public sealed class Replacement
		{
			public readonly ReplacementType Type;
			public readonly object Value;

			public Replacement(ReplacementType type, object value)
			{
				this.Type = type;
				this.Value = value;
			}

			public enum ReplacementType
			{
				Scalar = 1,
				Member = 2
			}
		}
	}
}