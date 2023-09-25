namespace AssemblyWithAnnotations;

using JetBrains.Annotations;

public abstract class BaseClassWithAttributes
{
    public abstract void MethodWithNotNullParameter(string canBeNull, [NotNull] string arg);

    [NotNull]
    public abstract string MethodWithNotNullReturnValue(string arg);

    [NotNull]
    public abstract string NotNullProperty { get; set; }
}

public interface BaseInterfaceWithAttributes
{
    void MethodWithNotNullParameter(string canBeNull, [NotNull] string arg);

    [NotNull]
    string MethodWithNotNullReturnValue(string arg);

    [NotNull]
    string NotNullProperty { get; set; }
}

public interface InheritedInterface : BaseInterfaceWithAttributes;

public interface InterfaceWithAttributes
{
    void MethodWithNotNullParameter(string canBeNull, [NotNull] string arg);

    [NotNull]
    string MethodWithNotNullReturnValue(string arg);

    [NotNull]
    string NotNullProperty { get; set; }
}