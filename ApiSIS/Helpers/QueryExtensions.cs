
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ApiSIS.Helpers;

public static class QueryExtensions
{
    // Search ke beberapa field sekaligus
    public static IQueryable<T> ApplySearch<T>(
        this IQueryable<T> query,
        string? search,
        params Expression<Func<T, string?>>[] fields)
    {
        if (string.IsNullOrWhiteSpace(search) || fields.Length == 0)
            return query;

        var parameter = Expression.Parameter(typeof(T), "x");
        Expression? body = null;
        var searchValue = Expression.Constant(search.Trim());

        foreach (var field in fields)
        {
            var fieldExpression = new ParameterReplacer(
                field.Parameters[0], parameter
            ).Visit(field.Body)!;

            var notNull = Expression.NotEqual(
                fieldExpression,
                Expression.Constant(null, typeof(string))
            );

            var contains = Expression.Call(
                fieldExpression,
                typeof(string).GetMethod(
                    nameof(string.Contains),
                    new[] { typeof(string) }
                )!,
                searchValue
            );

            var condition = Expression.AndAlso(notNull, contains);

            body = body == null
                ? condition
                : Expression.OrElse(body, condition);
        }

        var predicate = Expression.Lambda<Func<T, bool>>(
            body!, parameter
        );

        return query.Where(predicate);
    }

    // Filter berdasarkan field tertentu
    public static IQueryable<T> ApplyContainsFilter<T>(
        this IQueryable<T> query,
        string? value,
        Expression<Func<T, string?>> field)
    {
        if (string.IsNullOrWhiteSpace(value))
            return query;

        var parameter = field.Parameters[0];
        var notNull = Expression.NotEqual(
            field.Body,
            Expression.Constant(null, typeof(string))
        );

        var contains = Expression.Call(
            field.Body,
            typeof(string).GetMethod(
                nameof(string.Contains),
                new[] { typeof(string) }
            )!,
            Expression.Constant(value.Trim())
        );

        var predicate = Expression.Lambda<Func<T, bool>>(
            Expression.AndAlso(notNull, contains),
            parameter
        );

        return query.Where(predicate);
    }

    // Paging dan hitung total data
    public static async Task<PagedResult<T>> ToPagedResultAsync<T>(
     this IQueryable<T> query,
     PaginationRequest request)
    {
        int totalData = await query.CountAsync();

        int page = Math.Max(1, request.Page);
        int limit = Math.Clamp(request.Limit, 2, 100);

        var data = await query
            .Skip((page - 1) * limit)
            .Take(limit)
            .ToListAsync();

        return new PagedResult<T>
        {
            Page = page,
            Limit = limit,
            TotalData = totalData,
            TotalPages = (int)Math.Ceiling(totalData / (double)limit),
            Data = data
        };
    }

    private sealed class ParameterReplacer : ExpressionVisitor
    {
        private readonly ParameterExpression _oldParameter;
        private readonly ParameterExpression _newParameter;

        public ParameterReplacer(
            ParameterExpression oldParameter,
            ParameterExpression newParameter)
        {
            _oldParameter = oldParameter;
            _newParameter = newParameter;
        }

        protected override Expression VisitParameter(
            ParameterExpression node)
        {
            return node == _oldParameter
                ? _newParameter
                : base.VisitParameter(node);
        }
    }
}
