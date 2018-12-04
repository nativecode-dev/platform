namespace NativeCode.Core.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using AutoMapper;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Options;

    public class ContextSeeder<TContext> : IContextSeeder<TContext>
        where TContext : DbContext
    {
        protected ContextSeeder(TContext context, IMapper mapper, IOptions<ContextSeederOpens> options)
        {
            this.Context = context;
            this.Mapper = mapper;
            this.Options = options.Value;
        }

        protected TContext Context { get; }

        protected IMapper Mapper { get; }

        protected ContextSeederOpens Options { get; }

        public async Task Seed<TEntity>(IEnumerable<TEntity> entities, Expression<Func<TEntity, bool>> exists)
            where TEntity : class
        {
            var dbset = this.Context.Set<TEntity>();

            switch (this.Options.Strategy)
            {
                case ContextSeederStrategy.OnlyNewRecords:
                    var existing = await dbset.Where(exists)
                        .ToListAsync();

                    var exceptions = entities.Except(existing);

                    dbset.AddRange(exceptions);
                    return;

                default:
                    if (await dbset.AnyAsync())
                    {
                        dbset.AddRange(entities);
                    }

                    return;
            }
        }

        public async Task Seed<TEntity, TModel>(IEnumerable<TModel> models, Expression<Func<TEntity, bool>> exists)
            where TEntity : class
        {
            var entities = models.Select(model => this.Mapper.Map<TEntity>(model));

            await this.Seed<TEntity>(entities, exists);
        }
    }
}
