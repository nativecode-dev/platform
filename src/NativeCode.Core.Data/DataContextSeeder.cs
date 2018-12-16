namespace NativeCode.Core.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;
    using AutoMapper;
    using Core.Extensions;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;

    public class DataContextSeeder<T> : IDataContextSeeder<T>
        where T : DbContext
    {
        public DataContextSeeder(T context, JsonSerializerSettings settings, IMapper mapper,
            ILogger<IDataContextSeeder<T>> logger)
        {
            this.Context = context;
            this.Logger = logger;
            this.Mapper = mapper;
            this.Settings = settings;
        }

        protected T Context { get; }

        protected ILogger<IDataContextSeeder<T>> Logger { get; }

        protected IMapper Mapper { get; }

        protected JsonSerializerSettings Settings { get; }

        public void Clear<TEntity>()
            where TEntity : class
        {
            var dbset = this.Context.Set<TEntity>();
            dbset.RemoveRange(dbset.ToList());
        }

        public Task<int> SaveChangesAsync()
        {
            return this.Context.SaveChangesAsync();
        }

        public Task<bool> SeedAsync<TModel, TEntity>(string name, Func<TModel, DbSet<TEntity>, Task<TEntity>> select,
            Func<TModel, Task<TEntity>> converter, Func<TModel, TEntity, Task> callback = null)
            where TEntity : class
        {
            return this.SeedAsync(Assembly.GetEntryAssembly(), name, select, converter, callback);
        }

        public async Task<bool> SeedAsync<TModel, TEntity>(Assembly assembly, string name,
            Func<TModel, DbSet<TEntity>, Task<TEntity>> select, Func<TModel, Task<TEntity>> converter,
            Func<TModel, TEntity, Task> callback = null)
            where TEntity : class
        {
            var models = assembly.JsonFromManifest<IEnumerable<TModel>>(name, this.Settings);
            var dbset = this.Context.Set<TEntity>();
            var exceptions = new List<Exception>();

            foreach (var model in models)
            {
                try
                {
                    var existing = await select(model, dbset);
                    this.Logger.LogTrace("{@existing}", existing);

                    if (existing == null)
                    {
                        var entity = await converter.Invoke(model);

                        if (callback != null)
                        {
                            await callback.Invoke(model, entity);
                        }

                        dbset.Add(entity);
                        this.Logger.LogTrace("Created: {@model}", model);
                    }
                    else
                    {
                        /*
                        this.Mapper.Map(model, existing);
                        this.Logger.LogTrace("Updated: {@model}", model);
                        */
                    }
                }
                catch (Exception ex)
                {
                    this.Logger.LogCritical("{@ex}", ex);
                    exceptions.Add(ex);
                }
            }

            if (exceptions.Any())
            {
                throw new AggregateException(exceptions);
            }

            return true;
        }
    }
}
