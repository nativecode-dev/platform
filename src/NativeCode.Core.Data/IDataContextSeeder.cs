namespace NativeCode.Core.Data
{
    using System;
    using System.Reflection;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    public interface IDataContextSeeder<T>
        where T : DbContext
    {
        void Clear<TEntity>()
            where TEntity : class;

        Task<int> SaveChangesAsync();

        Task<bool> SeedAsync<TModel, TEntity>(
            string name,
            Func<TModel, DbSet<TEntity>, Task<TEntity>> projection,
            Func<TModel, DbSet<TEntity>, Task<TEntity>> converter,
            Func<TModel, TEntity, Task> callback = null)
            where TEntity : class;

        Task<bool> SeedAsync<TModel, TEntity>(
            Assembly assembly,
            string name,
            Func<TModel, DbSet<TEntity>, Task<TEntity>> projection,
            Func<TModel, DbSet<TEntity>, Task<TEntity>> converter,
            Func<TModel, TEntity, Task> callback = null)
            where TEntity : class;
    }
}
