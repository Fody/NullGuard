using NullGuard;

[NullGuard(ValidationFlags.NonPublic | ValidationFlags.Methods | ValidationFlags.Arguments)]
public class ClassWithPrivateMethod
{
    public void PublicWrapperOfPrivateMethod()
    {
        SomePrivateMethod(null);
    }

// ReSharper disable UnusedParameter.Local
    void SomePrivateMethod(string x)
// ReSharper restore UnusedParameter.Local
    {
    }

    public string SomeProperty { get; set; }
}