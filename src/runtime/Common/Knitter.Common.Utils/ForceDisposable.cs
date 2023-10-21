using Knitter.Common.Utils.Exception;

namespace Knitter.Common.Utils;

public abstract class ForceDisposable : Disposable
{
    ~ForceDisposable()
    {
        if (!_disposed)
        {
            throw new UndisposedException("Memory leak! You must call Dispose() manually?");
        }
    }
}