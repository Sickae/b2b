using System;
using System.Linq.Expressions;
using System.Reflection;

namespace B2B.Shared.Helpers
{
    public static class ExpressionHelper
    {
        public static Expression<Func<TModel, TProperty>> CreateMemberExpression<TModel, TProperty>(
            PropertyInfo propertyInfo)
        {
            if (propertyInfo == null)
                throw new ArgumentNullException(nameof(propertyInfo));

            var entityParam = Expression.Parameter(typeof(TModel), "x");
            Expression columnExpr = Expression.Property(entityParam, propertyInfo);

            if (propertyInfo.PropertyType != typeof(TProperty))
                columnExpr = Expression.Convert(columnExpr, typeof(TProperty));

            return Expression.Lambda<Func<TModel, TProperty>>(columnExpr, entityParam);
        }

        public static object CreateMemberExpressionForProperty<TModel>(PropertyInfo property)
        {
            var methodInfo =
                typeof(ExpressionHelper).GetMethod("CreateMemberExpression", BindingFlags.Static | BindingFlags.Public);
            var argumentTypes = new[] {typeof(TModel), property.PropertyType};
            var genericMethod = methodInfo.MakeGenericMethod(argumentTypes);
            return genericMethod.Invoke(null, new object[] {property});
        }
    }
}
