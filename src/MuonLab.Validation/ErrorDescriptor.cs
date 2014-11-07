using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace MuonLab.Validation
{
	public sealed class ErrorDescriptor
	{
		public readonly string Key;
		public readonly IDictionary<string, Replacement> Replacements;

		public ErrorDescriptor(string key, IDictionary<string, Replacement> replacements)
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

			public override string ToString()
			{
				switch (Type)
				{
					case ReplacementType.Scalar:
						return Value.ToString();
					case ReplacementType.Member:
						return ((MemberExpression)Value).Member.Name;
					default:
						throw new ArgumentOutOfRangeException();
				}
			}
		}
	}
}