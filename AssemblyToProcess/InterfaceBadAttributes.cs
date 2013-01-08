using NullGuard;

internal interface InterfaceBadAttributes
{
    void MethodWithNoNullCheckOnParam([AllowNull] string arg);

    string PropertyWithNoNullCheckOnSet { get; [param: AllowNull] set; }
}