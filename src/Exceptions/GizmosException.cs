namespace Godizmos;

public class GizmosException : Exception
{
    public GizmosException() { }

    public GizmosException(string message)
        : base(message) { }

    public GizmosException(string message, Exception inner)
        : base(message, inner) { }
}