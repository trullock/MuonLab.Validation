using System.Collections.Generic;

namespace MuonLab.Validation
{
	public sealed class ErrorDescriptior
	{
		public readonly string Key;
		public readonly IDictionary<string, string> Replacements;

		public ErrorDescriptior(string key, IDictionary<string, string> replacements)
		{
			this.Key = key;
			this.Replacements = replacements;
		}
	}
}