using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace MuonLab.Validation
{
	public interface IValidator<TEntity> : IValidator
	{
		ValidationReport Validate(TEntity entity);
		ValidationReport Validate<TOuter>(TEntity entity, Expression<Func<TOuter, TEntity>> prefix);

		IEnumerable<IValidationRule<TEntity>> GetRulesFor<TProperty>(Expression<Func<TEntity, TProperty>> property);
		IEnumerable<IValidationRule<TEntity>> ValidationRules { get; }
	}

	public interface IValidator
	{
		ValidationReport Validate(object entity);
	}
}