using NullGuard;

[NullGuard(ValidationFlags.NonPublic | ValidationFlags.Arguments)]
public class ClassWithPrivateMethod
{
    // ReSharper disable UnusedParameter.Local
    private void SomePrivateMethod(string x)
    // ReSharper restore UnusedParameter.Local
    {
    }

    public string SomeProperty { get; set; }
}