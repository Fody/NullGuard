using System;
using System.Runtime.Serialization;

[Serializable]
public class WeavingException : Exception
{
    public WeavingException()
    {
    }

    public WeavingException(string message)
        : base(message)
    {
    }

    public WeavingException(string message, Exception inner)
        : base(message, inner)
    {
    }

    protected WeavingException(SerializationInfo info, StreamingContext context)
        : base(info, context) { }
}