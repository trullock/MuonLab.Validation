using System;
using System.Linq;

namespace MuonLab.Validation.Tests.Enums
{
	public static class EnumExtensions
	{
		public static ICondition<T> IsAValidEnumValue<T>(this T self) where T : Enum
		{
			return self.Satisfies(e => Enum.GetValues(self.GetType()).Cast<Enum>().Contains(self), "Invalid");
		}
	}
}