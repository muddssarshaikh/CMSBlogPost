using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CMSBlogMaster_BL.Domain.Extension
{
    [ExcludeFromCodeCoverage]
    public static class PaginationSortFilter
    {
        private static readonly Type _stringType = typeof(string);
        private static readonly Type _enumerableType = typeof(IEnumerable<>);

        private static readonly MethodInfo _toStringMethod = typeof(object).GetMethod("ToString");

        private static readonly MethodInfo _stringContainsMethod = typeof(string).GetMethod("Contains"
            , new Type[] { typeof(string) });
        private static readonly MethodInfo _stringContainsMethodIgnoreCase = typeof(string).GetMethod("Contains"
            , new Type[] { typeof(string), typeof(StringComparison) });
        private static readonly MethodInfo _enumerableContainsMethod = typeof(Enumerable).GetMethods().Where(x => string.Equals(x.Name, "Contains", StringComparison.OrdinalIgnoreCase)).Single(x => x.GetParameters().Length == 2).MakeGenericMethod(typeof(string));
        private static readonly MethodInfo _dictionaryContainsKeyMethod = typeof(Dictionary<string, string>).GetMethods().Where(x => string.Equals(x.Name, "ContainsKey", StringComparison.OrdinalIgnoreCase)).Single();
        private static readonly MethodInfo _dictionaryContainsValueMethod = typeof(Dictionary<string, string>).GetMethods().Where(x => string.Equals(x.Name, "ContainsValue", StringComparison.OrdinalIgnoreCase)).Single();

        private static readonly MethodInfo _endsWithMethod
            = typeof(string).GetMethod("EndsWith", new Type[] { typeof(string) });

        private static readonly MethodInfo _isNullOrEmtpyMethod
        = typeof(string).GetMethod("IsNullOrEmpty", new Type[] { typeof(string) });

        private static readonly MethodInfo _startsWithMethod
                    = typeof(string).GetMethod("StartsWith", new Type[] { typeof(string) });

        public static Expression<Func<TEntity, bool>> GetPredicate<TEntity>(string property, FilterOperator op, object value)
        {
            var param = Expression.Parameter(typeof(TEntity));
            return Expression.Lambda<Func<TEntity, bool>>(GetFilter(param, property, op, value), param);
        }

        public static Expression<Func<TEntity, object>> GetPropertyGetter<TEntity>(string property)
        {
            if (property == null)
                throw new ArgumentNullException(nameof(property));

            var param = Expression.Parameter(typeof(TEntity));
            var prop = param.GetNestedProperty(property);
            var convertedProp = Expression.Convert(prop, typeof(object));
            return Expression.Lambda<Func<TEntity, object>>(convertedProp, param);
        }

        internal static Expression GetFilter(ParameterExpression param, string property, FilterOperator op, object value)
        {
            var constant = Expression.Constant(value);
            var prop = param.GetNestedProperty(property);
            return CreateFilter(prop, op, constant);
        }

        internal static MemberExpression GetNestedProperty(this Expression param, string property)
        {
            var propNames = property.Split('.');
            var propExpr = Expression.Property(param, propNames[0]);

            for (int i = 1; i < propNames.Length; i++)
            {
                propExpr = Expression.Property(propExpr, propNames[i]);
            }

            return propExpr;
        }

        public enum FilterOperator
        {
            Equals,
            DoesntEqual,
            GreaterThan,
            GreaterThanOrEqual,
            LessThan,
            LessThanOrEqual,
            Contains,
            NotContains,
            StartsWith,
            EndsWith,
            ContainsIgnoreCase,
            IsEmpty,
            IsNotEmpty,
            ContainsKey,
            NotContainsKey,
            ContainsValue,
            NotContainsValue
        }

        private static Expression CreateFilter(MemberExpression prop, FilterOperator op, ConstantExpression constant)
        {
            return op switch
            {
                FilterOperator.Equals => RobustEquals(prop, constant),
                FilterOperator.GreaterThan => Expression.GreaterThan(prop, constant),
                FilterOperator.LessThan => Expression.LessThan(prop, constant),
                FilterOperator.ContainsIgnoreCase => Expression.Call(prop, _stringContainsMethodIgnoreCase, PrepareConstant(constant), Expression.Constant(StringComparison.OrdinalIgnoreCase)),
                FilterOperator.Contains => GetContainsMethodCallExpression(prop, constant),
                FilterOperator.NotContains => Expression.Not(GetContainsMethodCallExpression(prop, constant)),
                FilterOperator.ContainsKey => Expression.Call(prop, _dictionaryContainsKeyMethod, PrepareConstant(constant)),
                FilterOperator.NotContainsKey => Expression.Not(Expression.Call(prop, _dictionaryContainsKeyMethod, PrepareConstant(constant))),
                FilterOperator.ContainsValue => Expression.Call(prop, _dictionaryContainsValueMethod, PrepareConstant(constant)),
                FilterOperator.NotContainsValue => Expression.Not(Expression.Call(prop, _dictionaryContainsValueMethod, PrepareConstant(constant))),
                FilterOperator.StartsWith => Expression.Call(prop, _startsWithMethod, PrepareConstant(constant)),
                FilterOperator.EndsWith => Expression.Call(prop, _endsWithMethod, PrepareConstant(constant)),
                FilterOperator.DoesntEqual => Expression.Not(RobustEquals(prop, constant)),
                FilterOperator.GreaterThanOrEqual => Expression.GreaterThanOrEqual(prop, constant),
                FilterOperator.LessThanOrEqual => Expression.LessThanOrEqual(prop, constant),
                FilterOperator.IsEmpty => Expression.Call(_isNullOrEmtpyMethod, prop),
                FilterOperator.IsNotEmpty => Expression.Not(Expression.Call(_isNullOrEmtpyMethod, prop)),
                _ => throw new NotImplementedException()
            };
        }

        private static Expression RobustEquals(MemberExpression prop, ConstantExpression constant)
        {
            if (prop.Type == typeof(bool) && bool.TryParse(constant.Value.ToString(), out var val))
            {
                return Expression.Equal(prop, Expression.Constant(val));
            }
            return Expression.Equal(prop, constant);
        }

        private static Expression GetContainsMethodCallExpression(MemberExpression prop, ConstantExpression constant)
        {
            if (prop.Type == _stringType)
                return Expression.Call(prop, _stringContainsMethod, PrepareConstant(constant));
            else if (prop.Type.GetInterfaces().Contains(typeof(IDictionary)))
                return Expression.Or(Expression.Call(prop, _dictionaryContainsKeyMethod, PrepareConstant(constant)), Expression.Call(prop, _dictionaryContainsValueMethod, PrepareConstant(constant)));
            else if (prop.Type.GetInterfaces().Contains(typeof(IEnumerable)))
                return Expression.Call(_enumerableContainsMethod, prop, PrepareConstant(constant));

            throw new NotImplementedException($"{prop.Type} contains is not implemented.");


        }

        private static Expression PrepareConstant(ConstantExpression constant)
        {

            if (constant.Type == _stringType)
                return constant;

            var convertedExpr = Expression.Convert(constant, typeof(object));
            return Expression.Call(convertedExpr, _toStringMethod);
        }
    }

    [ExcludeFromCodeCoverage]
    public class DynamicFilterBuilder<TEntity>
    {
        private readonly ParameterExpression _param;

        public DynamicFilterBuilder() : this(Expression.Parameter(typeof(TEntity))) { }

        DynamicFilterBuilder(ParameterExpression param)
        {
            _param = param;
        }

        private Expression Expression { get; set; }

        public DynamicFilterBuilder<TEntity> And(string property, PaginationSortFilter.FilterOperator op, object value)
        {
            var newExpr = PaginationSortFilter.GetFilter(_param, property, op, value);
            Expression = Expression == null ? newExpr : Expression.AndAlso(Expression, newExpr);
            return this;
        }

        public DynamicFilterBuilder<TEntity> And(Action<DynamicFilterBuilder<TEntity>> action)
        {
            var builder = new DynamicFilterBuilder<TEntity>(_param);
            action(builder);

            if (builder.Expression == null)
                throw new Exception("Empty builder");

            Expression = Expression == null ? builder.Expression : Expression.AndAlso(Expression, builder.Expression);
            return this;
        }

        public DynamicFilterBuilder<TEntity> Or(string property, PaginationSortFilter.FilterOperator op, object value)
        {
            var newExpr = PaginationSortFilter.GetFilter(_param, property, op, value);
            Expression = Expression == null ? newExpr : Expression.OrElse(Expression, newExpr);
            return this;
        }

        public DynamicFilterBuilder<TEntity> Or(Action<DynamicFilterBuilder<TEntity>> action)
        {
            var builder = new DynamicFilterBuilder<TEntity>(_param);
            action(builder);

            if (builder.Expression == null)
                throw new Exception("Empty builder");

            Expression = Expression == null ? builder.Expression : Expression.OrElse(Expression, builder.Expression);
            return this;
        }

        public Expression<Func<TEntity, bool>> Build()
        {
            return Expression.Lambda<Func<TEntity, bool>>(Expression, _param);
        }

        public Func<TEntity, bool> Compile() => Build().Compile();
    }
}
