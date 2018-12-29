namespace NativeCode.Core.Reliability
{
    using System;
    using System.Collections.Concurrent;

    public abstract class DisposableCollection : Disposable
    {
        private readonly ConcurrentBag<IDisposable> disposables = new ConcurrentBag<IDisposable>();

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.Disposed == false)
            {
                while (this.disposables.IsEmpty == false)
                {
                    if (this.disposables.TryTake(out var disposable))
                    {
                        disposable.Dispose();
                    }
                }
            }

            base.Dispose(disposing);
        }
    }
}
