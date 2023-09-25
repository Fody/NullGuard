namespace AssemblyWithExternalAnnotations;

public abstract class BaseClassWithAttributes
{
    public abstract void MethodWithNotNullParameter(string canBeNull, string arg);

    public abstract string MethodWithNotNullReturnValue(string arg);

    public abstract string NotNullProperty { get; set; }
}

public interface BaseInterfaceWithAttributes
{
    void MethodWithNotNullParameter(string canBeNull, string arg);

    string MethodWithNotNullReturnValue(string arg);

    string NotNullProperty { get; set; }
}

public interface InheritedInterface : BaseInterfaceWithAttributes;

public interface InterfaceWithAttributes
{
    void MethodWithNotNullParameter(string canBeNull, string arg);

    string MethodWithNotNullReturnValue(string arg);

    string NotNullProperty { get; set; }
}