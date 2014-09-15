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
			return new CollectionCondition<TItem, TCompare>(self, compareProperty);
		}

		public interface ICollectionCondition<TItem>
		{
			IEnumerable<Expression> GetViolations<T, TOuter>(Expression<Func<TOuter, T>> expression, Expression<Func<T, IList<TItem>>> propertyExpression);
		}

		public sealed class CollectionCondition<TItem, TCompare> : PropertyCondition<IList<TItem>>, ICollectionCondition<TItem>
		{
			readonly TItem[] values;
			readonly Expression<Func<TItem, TCompare>> compareProperty;

			public CollectionCondition(IEnumerable<TItem> values, Expression<Func<TItem, TCompare>> compareProperty) : base(null, null)
			{
				this.values = values.ToArray();
				this.compareProperty = compareProperty;

				this.ErrorMessage = "Duplicate value not allowed in field";
			}

			IList<int> GetDuplicateIndexes()
			{
				var duplicateIndexes = new List<int>();
				var comparePropertyFunc = compareProperty.Compile();

				for (var i = 0; i < values.Length; i++)
				{
					foreach (var val in values)
					{
						if (comparePropertyFunc(values[i]) == null)
						{
							if (!values[i].Equals(val) && comparePropertyFunc(val) == null)
								duplicateIndexes.Add(i);
							continue;
						}

						var property1 = comparePropertyFunc(values[i]);
						var property2 = comparePropertyFunc(val);

						if (!values[i].Equals(val) && ((property1 == null && property2 == null) || (property1 != null && property1.Equals(property2))))
							duplicateIndexes.Add(i);
					}
				}

				var uniqueDuplicateIndexes = duplicateIndexes.Distinct().ToList();

				return uniqueDuplicateIndexes;
			}

			public IEnumerable<Expression> GetViolations<T, TOuter>(Expression<Func<TOuter, T>> prefix, Expression<Func<T, IList<TItem>>> propertyExpression)
			{
				var duplicates = this.GetDuplicateIndexes();

				var violations = new List<Expression>();

				for (var i = 0; i < duplicates.Count; i++)
				{
					var propExpr = this.GetExpression(prefix, propertyExpression, i);

					violations.Add(propExpr);
				}

				return violations;
			}

			Expression GetExpression<T, TOuter>(Expression<Func<TOuter, T>> prefix, Expression<Func<T, IList<TItem>>> propertyExpression, int index)
			{
				Expression<Func<IList<TItem>, TItem>> indexer = y => y[index];
				var errorProperty = propertyExpression.Combine(indexer, true).Combine(this.compareProperty, true);

				if (prefix != null)
				{
					var combinedExpression = prefix.Combine(errorProperty, true);
					return combinedExpression;
				}

				return errorProperty;
			}
		}
	}
}