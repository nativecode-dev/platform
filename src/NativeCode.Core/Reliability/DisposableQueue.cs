namespace NativeCode.Core.Reliability
{
    using System;
    using System.Collections.Concurrent;

    public abstract class DisposableQueue : Disposable
    {
        private readonly ConcurrentQueue<IDisposable> queue = new ConcurrentQueue<IDisposable>();

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.Disposed == false)
            {
                while (this.queue.IsEmpty == false)
                {
                    this.PopDisposable();
                }
            }

            base.Dispose(disposing);
        }

        protected void PopDisposable()
        {
            if (this.queue.TryDequeue(out var disposable))
            {
                disposable.Dispose();
            }
        }

        protected void PushDisposable(IDisposable disposable)
        {
            this.queue.Enqueue(disposable);
        }
    }
}