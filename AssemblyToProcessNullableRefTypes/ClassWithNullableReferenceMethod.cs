/// <summary>
/// This class does not get annotated with NullableContext, but the method does.
/// </summary>
public class ClassWithNullableReferenceMethod
{
    // [NullableContext(1)]
    // [return: Nullable(2)]
    public string? MethodAllowsNullReturnValue(string nonNullArg) => null;

}