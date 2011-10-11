using System.Collections;

namespace MuonLab.Validation
{
	public static class IEnumerableExtensions
	{
		public static ICondition<IEnumerable> ContainsElements(this IEnumerable self)
		{
			return self.ContainsElements("{val} must contain at least one entry");
		}

		public static ICondition<IEnumerable> ContainsElements(this IEnumerable self, string errorMessage)
		{
			return self.Satisfies(x => (x ?? new object[] {}).GetEnumerator().MoveNext(), errorMessage);
		}
	}
}