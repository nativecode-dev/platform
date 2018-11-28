namespace NativeCode.Core.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;

    public interface IContextSeeder<TContext>
        where TContext : DbContext
    {
        Task Seed<TEntity>(IEnumerable<TEntity> entities, Expression<Func<TEntity, bool>> exists)
            where TEntity : class;

        Task Seed<TEntity, TModel>(IEnumerable<TModel> models, Expression<Func<TEntity, bool>> exists)
            where TEntity : class;
    }
}