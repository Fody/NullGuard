
// ReSharper disable UnusedParameter.Local
using NullGuard;

public class TargetClass
{
    public TargetClass()
    {
    }

    public TargetClass(string arg)
    {
    }

    public void MethodWithNullCheckOnParam(string arg)
    {
    }

    public void MethodWithNoNullCheckOnParam([AllowNull] string arg)
    {
    }

    public string PropertyWithNullCheckOnSet { get; set; }

    public string PropertyWithNoNullCheckOnSet { get; [param: AllowNull] set; }
}

// ReSharper restore UnusedParameter.Local