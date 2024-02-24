using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using CMSBlogMaster_BL.Domain.Common;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace CMSBlogMaster_BL.Domain.Extension
{
    [ExcludeFromCodeCoverage]
    public static class PaginationExtension
    {
        /// <summary>
        /// Paginate the IEnumerable
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="list">IEnumerable</param>
        /// <param name="query">PaginationQuery</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IEnumerable<T> Paginate<T>(this IEnumerable<T> list, PaginationQuery query)
        {
            if (query.PageNumber < 0)
                throw new ArgumentException(ValidationMessages.PAGE_NUMBER_CANNOT_BE_NEGATIVE);
            if (list == null)
            {
                throw new CustomException(ValidationMessages.RECORD_NOT_FOUND);
            }
            int skip = (query.PageNumber) * query.PageSize;
            return list.Skip(skip).Take(query.PageSize);
        }

        //public static Expression<Func<T, bool>> LikeLambdaString<T>(string propertyName, string value)
        //{

        //    var linqParam = Expression.Parameter(typeof(T), propertyName);
        //    var linqProp = GetProperty<T>(linqParam, propertyName);

        //    var containsFunc = Expression.Call(linqProp,
        //        typeof(string).GetMethod("Contains"),
        //        new Expression[] { Expression.Constant(value) });

        //    return Expression.Lambda<Func<T, bool>>(containsFunc,
        //        new ParameterExpression[] { linqParam });
        //}

    }
}
