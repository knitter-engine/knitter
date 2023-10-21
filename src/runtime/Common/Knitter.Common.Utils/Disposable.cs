using Knitter.Common.Utils.Exception;

namespace Knitter.Common.Utils;

public abstract class Disposable : IDisposable
{
    protected bool _disposed = false;
    protected abstract void DoDispose();// customize dispose code
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            DoDispose();
            _disposed = true;
        }
    }

    ~Disposable()
    {
        if (!_disposed)
        {
            Dispose();
        }
    }
}