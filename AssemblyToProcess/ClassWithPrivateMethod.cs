using NullGuard;

[NullGuard(ValidationFlags.NonPublic | ValidationFlags.Methods | ValidationFlags.Arguments)]
public class ClassWithPrivateMethod
{
    public void PublicWrapperOfPrivateMethod()
    {
        SomePrivateMethod(null);
    }

    private void SomePrivateMethod(string x)
    {
    }

    public string SomeProperty { get; set; }
}