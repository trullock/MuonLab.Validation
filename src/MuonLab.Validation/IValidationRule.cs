using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MuonLab.Validation
{
    public interface IValidationRule
    {
    }

    public interface IValidationRule<T> : IValidationRule
	{
		Task<IEnumerable<IViolation>> Validate<TOuter>(T entity, Expression<Func<TOuter, T>> prefix);
	}
}