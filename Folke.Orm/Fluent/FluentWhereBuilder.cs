﻿namespace Folke.Orm.Fluent
{
    using System;
    using System.Linq.Expressions;

    public class FluentWhereBuilder<T, TMe> : FluentQueryableBuilder<T, TMe>
    {
        public FluentWhereBuilder(BaseQueryBuilder queryBuilder, Expression<Func<T, bool>> expression)
            : base(queryBuilder)
        {
            queryBuilder.AppendWhere();
            queryBuilder.AddExpression(expression.Body);
        }

        public FluentWhereBuilder(BaseQueryBuilder queryBuilder, Expression<Func<T, TMe, bool>> expression)
            : base(queryBuilder)
        {
            queryBuilder.AppendWhere();
            queryBuilder.AddExpression(expression.Body);
        }

        public FluentGroupByBuilder<T, TMe, TU> GroupBy<TU>(Expression<Func<T, TU>> column)
        {
            return new FluentGroupByBuilder<T, TMe, TU>(QueryBuilder, column);
        }

        public FluentOrderByBuilder<T, TMe> OrderBy(Expression<Func<T, object>> column)
        {
            return new FluentOrderByBuilder<T, TMe>(QueryBuilder, column);
        }

        public FluentLimitBuilder<T, TMe> Limit(int offset, int count)
        {
            return new FluentLimitBuilder<T, TMe>(QueryBuilder, offset, count);
        }

        public FluentWhereBuilder<T, TMe> Where(Expression<Func<T, bool>> expression)
        {
            return new FluentWhereBuilder<T, TMe>(QueryBuilder, expression);
        }

        public FluentWhereBuilder<T, TMe> WhereSub(Action<FluentWhereExpressionBuilder<T, TMe>> expression)
        {
            QueryBuilder.AppendWhere();
            QueryBuilder.Append("(");
            var whereExpressionBuilder = new FluentWhereExpressionBuilder<T, TMe>(QueryBuilder);
            expression(whereExpressionBuilder);
            QueryBuilder.Append(")");
            return this;
        }
    }
}
