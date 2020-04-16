using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MuonLab.Validation
{
	public interface IValidator<TEntity> : IValidator
	{
		Task<ValidationReport> Validate(TEntity entity);
		Task<ValidationReport> Validate<TOuter>(TEntity entity, Expression<Func<TOuter, TEntity>> prefix);

		IEnumerable<IValidationRule<TEntity>> GetRulesFor<TProperty>(Expression<Func<TEntity, TProperty>> property);
		IEnumerable<IValidationRule<TEntity>> ValidationRules { get; }
	}

	public interface IValidator
	{
		Task<ValidationReport> Validate(object entity);
	}
}