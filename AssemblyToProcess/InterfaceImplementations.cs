using System;

public class InterfaceImplementations
{
    public interface ISomeInterface
    {
        string NonNullProperty { get; set; }

        void SomeInterfaceMethod(string notNull);
    }

    private class ExplicitImplementation : ISomeInterface
    {
        string ISomeInterface.NonNullProperty { get; set; }

        // This method is expected to get an argument check because it is exposed through ISomeInterface
        void ISomeInterface.SomeInterfaceMethod(string notNull)
        {
        }
    }

    private class ImplicitImplementation : ISomeInterface
    {
        public string NonNullProperty { get; set; }

        // This method (of a private class) is expected to get an argument check because it is exposed through ISomeInterface
        public void SomeInterfaceMethod(string notNull)
        {
        }
    }
}