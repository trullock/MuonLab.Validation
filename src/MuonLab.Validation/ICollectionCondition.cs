using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace MuonLab.Validation
{
	public interface ICollectionCondition<TItem>
	{
		IEnumerable<Expression> GetViolations<T, TOuter>(Expression<Func<TOuter, T>> expression, Expression<Func<T, IList<TItem>>> propertyExpression);
	}
}