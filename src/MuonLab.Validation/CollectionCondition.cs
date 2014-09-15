using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace MuonLab.Validation
{
	public sealed class CollectionCondition<TItem, TCompare> : PropertyCondition<IList<TItem>>, ICollectionCondition<TItem>
	{
		readonly TItem[] values;
		readonly Func<IList<TItem>, TItem, Func<TItem, TCompare>, bool> testFunction;
		readonly Expression<Func<TItem, TCompare>> compareProperty;

		public CollectionCondition(string errorMessage, IEnumerable<TItem> values, Expression<Func<TItem, TCompare>> compareProperty, Func<IList<TItem>, TItem, Func<TItem, TCompare>, bool> testFunction)
			: base(null, null)
		{
			this.values = values.ToArray();
			this.testFunction = testFunction;
			this.compareProperty = compareProperty;
			
			this.ErrorMessage = errorMessage;
		}

		IList<int> GetInvalidIndexes()
		{
			var comparePropertyFunc = this.compareProperty.Compile();
			var invalidIndexes = new List<int>();

			for (var i = 0; i < this.values.Length; i++)
			{
				if (this.testFunction(this.values, this.values[i], comparePropertyFunc))
					invalidIndexes.Add(i);
			}

			return invalidIndexes;
		}

		public IEnumerable<Expression> GetViolations<T, TOuter>(Expression<Func<TOuter, T>> prefix, Expression<Func<T, IList<TItem>>> propertyExpression)
		{
			var invalidIndexes = this.GetInvalidIndexes();
			var violations = new List<Expression>();

			for (var i = 0; i < invalidIndexes.Count; i++)
			{
				var propExpr = this.GetViolationExpression(prefix, propertyExpression, i);
				violations.Add(propExpr);
			}

			return violations;
		}

		Expression GetViolationExpression<T, TOuter>(Expression<Func<TOuter, T>> prefix, Expression<Func<T, IList<TItem>>> propertyExpression, int index)
		{
			Expression<Func<IList<TItem>, TItem>> indexer = y => y[index];
			var errorProperty = propertyExpression.Combine(indexer, true).Combine(this.compareProperty, true);

			if (prefix == null)
				return errorProperty;

			var combinedExpression = prefix.Combine(errorProperty, true);
			return combinedExpression;
		}
	}
}