namespace NativeCode.Core.Syncing
{
    using System.Collections.Generic;
    using System.Linq;

    public class SyncBuilder<TSource, TTarget>
    {
        public SyncBuilder(IReadOnlyCollection<TSource> sources, IReadOnlyCollection<TTarget> targets)
        {
            this.Sources = sources;
            this.Targets = targets;
        }

        protected IReadOnlyCollection<TSource> Sources { get; }

        protected IReadOnlyCollection<TTarget> Targets { get; }

        public IReadOnlyCollection<SyncBuilderChange<TSource, TTarget>> Diff()
        {
            var changes = new List<SyncBuilderChange<TSource, TTarget>>();
            this.Add(changes)
                .Remove(changes);

            return changes;
        }

        protected virtual SyncBuilder<TSource, TTarget> Add(ICollection<SyncBuilderChange<TSource, TTarget>> changes)
        {
            foreach (var source in this.Sources)
            {
                var target = this.Targets.SingleOrDefault(t => this.IsEqual(source, t));

                if (target != null)
                {
                    changes.Add(new SyncBuilderChange<TSource, TTarget>(source, target, SyncBuilderChangeType.Add));
                }
            }

            return this;
        }

        protected virtual bool IsEqual(TSource source, TTarget target)
        {
            return Equals(source, target);
        }

        protected virtual SyncBuilder<TSource, TTarget> Remove(ICollection<SyncBuilderChange<TSource, TTarget>> changes)
        {
            foreach (var target in this.Targets)
            {
                var source = this.Sources.SingleOrDefault(s => this.IsEqual(s, target));

                if (source != null)
                {
                    changes.Add(new SyncBuilderChange<TSource, TTarget>(source, target, SyncBuilderChangeType.Remove));
                }
            }

            return this;
        }
    }
}
