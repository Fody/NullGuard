using NullGuard;

public abstract class ClassWithBadAttributes
{
    public abstract void MethodWithNoNullCheckOnParam([AllowNull] string arg);
    public abstract string PropertyWithNoNullCheckOnSet { get; [param: AllowNull] set; }
    public abstract string PropertyAllowsNullGetButDoesNotAllowNullSet { [return: AllowNull] get; set; }

    [return: AllowNull]
    public abstract string MethodAllowsNullReturnValue();
}