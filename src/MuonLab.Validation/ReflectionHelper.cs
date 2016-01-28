using System;
using System.Linq.Expressions;
using System.Reflection;

namespace MuonLab.Validation
{
	public static class ReflectionHelper
    {
        public static string GetPropertyName<T, TResult>(Expression<Func<T, TResult>> property)
        {
            var body = GetBody(property);
            return body.Member.Name;
        }

        public static MemberInfo GetMemberInfo<T, TResult>(Expression<Func<T, TResult>> property)
        {
            var body = GetBody(property);
            return body.Member;
        }

        public static TValue GetPropertyValue<TEntity, TValue>(TEntity entity, Expression<Func<TEntity, TValue>> property)
        {
            return property.Compile().Invoke(entity);
        }

        public static void SetPropertyValue<TEntity, TProperty>(TEntity entity, Expression<Func<TEntity, TProperty>> property, TProperty value)
        {
            typeof (TEntity).GetProperty(GetPropertyName(property)).SetValue(entity, value, null);
        }

        public static object GetPropertyValue(object obj, string propertyName)
        {
            return obj.GetType().GetProperty(propertyName).GetValue(obj, null);
        }

        public static string PropertyChainToString<TEntity, TResult>(Expression<Func<TEntity, TResult>> property, char delimeter)
        {
            var body = GetBody(property);
            return propertyChainToString(body.Expression, delimeter) + body.Member.Name;
        }

        public static string PropertyChainToString(Expression expression, char delimeter)
        {
            if (expression is LambdaExpression)
            {
                var memberExpression = expression as LambdaExpression;
                return propertyChainToString(memberExpression.Body, delimeter).TrimEnd(delimeter);
            }

            throw new NotSupportedException("Probably a nullable type, need implementing! Debug: `" + expression + "`");
        }

		public static string propertyChainToString(Expression expression, char delimeter)
        {
            if (expression is MemberExpression)
            {
                var exp = expression as MemberExpression;
                return propertyChainToString(exp.Expression, delimeter) + exp.Member.Name + delimeter;
            }
			if(expression is MethodCallExpression)
			{
				var exp = expression as MethodCallExpression;
				if(exp.Method.Name == "get_Item")
				{
					var index = ((exp.Arguments[0] as MemberExpression).Member as FieldInfo).GetValue(((exp.Arguments[0] as MemberExpression).Expression as ConstantExpression).Value);

					return propertyChainToString((exp.Object as MemberExpression).Expression, delimeter) + (exp.Object as MemberExpression).Member.Name + "[" + index + "]" + delimeter;
				}
			}
            return string.Empty;
        }

        private static MemberExpression GetBody<TEntity, TResult>(Expression<Func<TEntity, TResult>> property)
        {
            if ((typeof (TResult).IsGenericType && typeof (TResult).GetGenericTypeDefinition().Equals(typeof (Nullable<>))) && property.Body is UnaryExpression)
            {
                var outerBody = property.Body as UnaryExpression;

                return outerBody.Operand as MemberExpression;
            }

            return property.Body as MemberExpression;
        }

        public static Expression<Func<T, TValue>> BuildGet<T, TValue>(PropertyInfo property)
        {
            Type type = typeof (T);
            ParameterExpression arg = Expression.Parameter(type, "x");
            var prop = Expression.Property(arg, property);

            return Expression.Lambda<Func<T, TValue>>(prop, arg);
        }

        public static bool MemberAccessExpressionsAreEqual<T, TProperty>(Expression<Func<T, TProperty>> expression1, Expression<Func<T, TProperty>> expression2)
        {
            return memberAccessExpressionsAreEqual(expression1.Body as MemberExpression, expression2.Body as MemberExpression);
        }

        private static bool memberAccessExpressionsAreEqual(MemberExpression expression1, MemberExpression expression2)
        {
            if (expression1 != null && expression2 != null)
                if (expression1.Member.Name == expression2.Member.Name)
                    return memberAccessExpressionsAreEqual(expression1.Expression as MemberExpression, expression2.Expression as MemberExpression);
                else
                    return false;
            else
                return true;
        }
    }
}