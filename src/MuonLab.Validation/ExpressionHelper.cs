using System;
using System.Linq.Expressions;

namespace MuonLab.Validation
{
	internal static class ExpressionHelper
	{
        /// <summary>
        /// Combines two delegate expressions into a single delegate
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <param name="outer"></param>
        /// <param name="inner"></param>
        /// <param name="inline"></param>
        /// <returns></returns>
		public static Expression<Func<T1, T3>> Combine<T1, T2, T3>(this Expression<Func<T1, T2>> outer, Expression<Func<T2, T3>> inner, bool inline)
		{
			var invoke = Expression.Invoke(inner, outer.Body);
			var body = inline ? new ExpressionRewriter().AutoInline(invoke) : invoke;

			return Expression.Lambda<Func<T1, T3>>(body, outer.Parameters);
		}

	}
}