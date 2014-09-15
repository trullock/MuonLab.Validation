using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace MuonLab.Validation
{
	public static class NoDuplicatesRule
	{
		public static CollectionCondition<TItem, TCompare> DoesNotHaveDuplicates<TItem, TCompare>(this IList<TItem> self, Expression<Func<TItem, TCompare>> compareProperty)
		{
			return self.DoesNotHaveDuplicates(compareProperty, "{val} must be unique");
		}

		public static CollectionCondition<TItem, TCompare> DoesNotHaveDuplicates<TItem, TCompare>(this IList<TItem> self, Expression<Func<TItem, TCompare>> compareProperty, string errorMessage)
		{
			return new CollectionCondition<TItem, TCompare>(errorMessage, self, compareProperty, (list, item, getCompareValue) =>
			{
				var propertyValue = getCompareValue(item);

				if (propertyValue == null)
					return list.Any(x => !item.Equals(x) && getCompareValue(x) == null);

				return list.Any(x => !item.Equals(x) && propertyValue.Equals(getCompareValue(x)));
			});
		}
	}
}