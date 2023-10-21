namespace Knitter.Common.Utils.Exception;

public abstract class KnitterException : System.Exception
{
    public KnitterException(string message) : base(message) { }
}

public class NotInitializeException : KnitterException
{
    public NotInitializeException(string message) : base(message) { }
}
