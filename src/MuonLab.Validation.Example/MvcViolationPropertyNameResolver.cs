using System.Linq.Expressions;

namespace MuonLab.Validation.Example
{
	public class MvcViolationPropertyNameResolver : IViolationPropertyNameResolver
	{
		public string ResolvePropertyName(IViolation violation)
		{
			return System.Web.Mvc.ExpressionHelper.GetExpressionText(violation.Property as LambdaExpression);
		}
	}
}