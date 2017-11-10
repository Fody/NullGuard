using JetBrains.Annotations;

public class UnsafeClass
{
    [NotNull]
    unsafe public int* MethodWithAmp([NotNull] int* instance)
    {
        return default;
    }

    [NotNull]
    unsafe public int* NullProperty { get; set; }
}