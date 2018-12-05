using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

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
			return self.Satisfies(x => (x ?? new object[] { }).GetEnumerator().MoveNext(), errorKey);
		}

		public static ICondition<IEnumerable<TValue>> HasCountGreaterThan<TValue>(this IEnumerable<TValue> self,
			int count)
		{
			return self.HasCountGreaterThan(x => true, count);
		}

		public static ICondition<IEnumerable<TValue>> HasCountGreaterThan<TValue>(this IEnumerable<TValue> self,
			Func<TValue, bool> predicate, int count)
		{
			return self.HasCountGreaterThan(predicate, count, "GreaterThan");
		}

		public static ICondition<IEnumerable<TValue>> HasCountGreaterThan<TValue>(this IEnumerable<TValue> self,
			Func<TValue, bool> predicate, int count, string errorKey)
		{
			return self.Satisfies(x => x.Count(predicate) > count, errorKey);
		}

		public static ICondition<IEnumerable<TValue>> HasCountGreaterThanOrEqualTo<TValue>(
			this IEnumerable<TValue> self, int count)
		{
			return self.HasCountGreaterThanOrEqualTo(x => true, count);
		}

		public static ICondition<IEnumerable<TValue>> HasCountGreaterThanOrEqualTo<TValue>(
			this IEnumerable<TValue> self, Func<TValue, bool> predicate, int count)
		{
			return self.HasCountGreaterThanOrEqualTo(predicate, count, "GreaterThanEq");
		}

		public static ICondition<IEnumerable<TValue>> HasCountGreaterThanOrEqualTo<TValue>(
			this IEnumerable<TValue> self, Func<TValue, bool> predicate, int count, string errorKey)
		{
			return self.Satisfies(x => x.Count(predicate) >= count, errorKey);
		}

		public static ICondition<IEnumerable<TValue>> HasCountLessThan<TValue>(this IEnumerable<TValue> self, int count)
		{
			return self.HasCountLessThan(x => true, count);
		}

		public static ICondition<IEnumerable<TValue>> HasCountLessThan<TValue>(this IEnumerable<TValue> self,
			Func<TValue, bool> predicate, int count)
		{
			return self.HasCountLessThan(predicate, count, "LessThan");
		}

		public static ICondition<IEnumerable<TValue>> HasCountLessThan<TValue>(this IEnumerable<TValue> self,
			Func<TValue, bool> predicate, int count, string errorKey)
		{
			return self.Satisfies(x => x.Count(predicate) < count, errorKey);
		}

		public static ICondition<IEnumerable<TValue>> HasCountLessThanOrEqualTo<TValue>(this IEnumerable<TValue> self,
			int count)
		{
			return self.HasCountLessThanOrEqualTo(x => true, count);
		}

		public static ICondition<IEnumerable<TValue>> HasCountLessThanOrEqualTo<TValue>(this IEnumerable<TValue> self,
			Func<TValue, bool> predicate, int count)
		{
			return self.HasCountLessThanOrEqualTo(predicate, count, "LessThanEq");
		}

		public static ICondition<IEnumerable<TValue>> HasCountLessThanOrEqualTo<TValue>(this IEnumerable<TValue> self,
			Func<TValue, bool> predicate, int count, string errorKey)
		{
			return self.Satisfies(x => x.Count(predicate) <= count, errorKey);
		}
	}
}