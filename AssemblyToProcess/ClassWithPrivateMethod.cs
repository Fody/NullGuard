// ReSharper disable UnusedParameter.Local
// ReSharper disable MemberCanBeMadeStatic.Local

using NullGuard;

[NullGuard(ValidationFlags.NonPublic | ValidationFlags.Arguments)]
public class ClassWithPrivateMethod
{
    public void PublicWrapperOfPrivateMethod()
    {
        SomePrivateMethod(null);
    }
    void SomePrivateMethod(string x)
    {
    }

    public string SomeProperty { get; set; }
}