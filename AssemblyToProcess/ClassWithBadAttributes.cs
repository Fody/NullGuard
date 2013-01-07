using NullGuard;

public abstract class ClassWithBadAttributes
{

    public abstract void MethodWithNoNullCheckOnParam([AllowNull] string arg);

    public abstract string PropertyWithNoNullCheckOnSet { get; [param: AllowNull] set; }

}