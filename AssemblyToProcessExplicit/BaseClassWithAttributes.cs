using JetBrains.Annotations;

using NullGuard;

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

public interface InheritedInterface : BaseInterfaceWithAttributes
{
    
}

public interface InterfaceWithAttributes
{
    void MethodWithNotNullParameter(string canBeNull, [NotNull] string arg);

    [NotNull]
    string MethodWithNotNullReturnValue(string arg);

    [NotNull]
    string NotNullProperty { get; set; }
}



public class DerivedClass : BaseClassWithAttributes
{
    public override void MethodWithNotNullParameter(string canBeNull, string arg)
    {
    }

    public override string MethodWithNotNullReturnValue(string arg)
    {
        return arg;
    }

    public override string NotNullProperty { get; set; }
}

public class ImplementsInterface : InterfaceWithAttributes
{
    public void MethodWithNotNullParameter(string canBeNull, string arg)
    {
    }

    public string MethodWithNotNullReturnValue(string arg)
    {
        return arg;
    }

    public string NotNullProperty { get; set; }
}

public class ImplementsInheritedInterface : InheritedInterface
{
    public void MethodWithNotNullParameter(string canBeNull, string arg)
    {
    }

    public string MethodWithNotNullReturnValue(string arg)
    {
        return arg;
    }

    public string NotNullProperty { get; set; }
}

[NullGuard(ValidationFlags.All)] // TODO: need this due to https://github.com/Fody/NullGuard/issues/37 https://github.com/Fody/NullGuard/issues/60, remove after fix of #60
public class ImplementsInterfaceExplicit : InterfaceWithAttributes
{
    public void MethodWithNotNullParameter(string canBeNull, string arg)
    {
        ((InterfaceWithAttributes)this).MethodWithNotNullParameter(canBeNull, arg);
    }

    public string MethodWithNotNullReturnValue(string arg)
    {
        return ((InterfaceWithAttributes)this).MethodWithNotNullReturnValue(arg);
    }

    public string NotNullProperty
    {
        get => ((InterfaceWithAttributes)this).NotNullProperty;
        set => ((InterfaceWithAttributes)this).NotNullProperty = value;
    }

    void InterfaceWithAttributes.MethodWithNotNullParameter(string canBeNull, string arg)
    {
    }

    string InterfaceWithAttributes.MethodWithNotNullReturnValue(string arg)
    {
        return arg;
    }

    string InterfaceWithAttributes.NotNullProperty
    {
        get;
        set;
    }
}


