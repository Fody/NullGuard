using NullGuard;

[NullGuard(ValidationFlags.NonPublic | ValidationFlags.Arguments)]
public class ClassWithPrivateMethod
{
    public void PublicWrapperOfPrivateMethod()
    {
        SomePrivateMethod(null);
    }

    // ReSharper disable UnusedParameter.Local
    void SomePrivateMethod([NotNull] string x)
    // ReSharper restore UnusedParameter.Local
    {
    }

    [NotNull]
    public string SomeProperty { get; set; }
}