using System.Collections;

// ReSharper disable once CheckNamespace
namespace MuonLab.Validation
{
	public static class IEnumerableExtensions
	{
		public static ICondition<IEnumerable> ContainsElements(this IEnumerable self)
		{
			return self.ContainsElements("NotEmpty");
		}

		public static ICondition<IEnumerable> ContainsElements(this IEnumerable self, string errorKey)
		{
			return self.Satisfies(x => (x ?? new object[] {}).GetEnumerator().MoveNext(), errorKey);
		}
	}
}