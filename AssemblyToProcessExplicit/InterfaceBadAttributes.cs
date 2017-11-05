using JetBrains.Annotations;

using NullGuard;

internal interface InterfaceBadAttributes
{
    void MethodWithNoNullCheckOnParam([CanBeNull] string arg);
    [NotNull]
    string PropertyWithNoNullCheckOnSet { get; [param: AllowNull] set; }
    [NotNull]
    string PropertyAllowsNullGetButDoesNotAllowNullSet { [return: AllowNull] get; set; }
    [return: AllowNull]
    string MethodAllowsNullReturnValue();
}