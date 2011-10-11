using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace MuonLab.Validation
{
	public interface IValidationRule<T> : IValidationRule
	{
		IEnumerable<IViolation> Validate<TOuter>(T entity, Expression<Func<TOuter, T>> prefix);
	}

	public interface IValidationRule
	{
		MethodCallExpression Condition { get; }
	}
}