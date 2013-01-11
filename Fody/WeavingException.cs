using System;

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

    protected WeavingException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context)
        : base(info, context) { }
}