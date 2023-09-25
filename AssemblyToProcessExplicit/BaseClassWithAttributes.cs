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

public interface InheritedInterface : BaseInterfaceWithAttributes;

public interface InterfaceWithAttributes
{
    void MethodWithNotNullParameter(string canBeNull, [NotNull] string arg);

    [NotNull]
    string MethodWithNotNullReturnValue(string arg);

    [NotNull]
    string NotNullProperty { get; set; }
}

namespace InternalBase
{
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
}

namespace AssemblyBase
{
    public class DerivedClass : AssemblyWithAnnotations.BaseClassWithAttributes
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

    public class ImplementsInterface : AssemblyWithAnnotations.InterfaceWithAttributes
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

    public class ImplementsInheritedInterface : AssemblyWithAnnotations.InheritedInterface
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
    public class ImplementsInterfaceExplicit : AssemblyWithAnnotations.InterfaceWithAttributes
    {
        public void MethodWithNotNullParameter(string canBeNull, string arg)
        {
            ((AssemblyWithAnnotations.InterfaceWithAttributes)this).MethodWithNotNullParameter(canBeNull, arg);
        }

        public string MethodWithNotNullReturnValue(string arg)
        {
            return ((AssemblyWithAnnotations.InterfaceWithAttributes)this).MethodWithNotNullReturnValue(arg);
        }

        public string NotNullProperty
        {
            get => ((AssemblyWithAnnotations.InterfaceWithAttributes)this).NotNullProperty;
            set => ((AssemblyWithAnnotations.InterfaceWithAttributes)this).NotNullProperty = value;
        }

        void AssemblyWithAnnotations.InterfaceWithAttributes.MethodWithNotNullParameter(string canBeNull, string arg)
        {
        }

        string AssemblyWithAnnotations.InterfaceWithAttributes.MethodWithNotNullReturnValue(string arg)
        {
            return arg;
        }

        string AssemblyWithAnnotations.InterfaceWithAttributes.NotNullProperty
        {
            get;
            set;
        }
    }
}

namespace ExternalBase
{
    public class DerivedClass : AssemblyWithExternalAnnotations.BaseClassWithAttributes
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

    public class ImplementsInterface : AssemblyWithExternalAnnotations.InterfaceWithAttributes
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

    public class ImplementsInheritedInterface : AssemblyWithExternalAnnotations.InheritedInterface
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
    public class ImplementsInterfaceExplicit : AssemblyWithExternalAnnotations.InterfaceWithAttributes
    {
        public void MethodWithNotNullParameter(string canBeNull, string arg)
        {
            ((AssemblyWithExternalAnnotations.InterfaceWithAttributes)this).MethodWithNotNullParameter(canBeNull, arg);
        }

        public string MethodWithNotNullReturnValue(string arg)
        {
            return ((AssemblyWithExternalAnnotations.InterfaceWithAttributes)this).MethodWithNotNullReturnValue(arg);
        }

        public string NotNullProperty
        {
            get => ((AssemblyWithExternalAnnotations.InterfaceWithAttributes)this).NotNullProperty;
            set => ((AssemblyWithExternalAnnotations.InterfaceWithAttributes)this).NotNullProperty = value;
        }

        void AssemblyWithExternalAnnotations.InterfaceWithAttributes.MethodWithNotNullParameter(string canBeNull, string arg)
        {
        }

        string AssemblyWithExternalAnnotations.InterfaceWithAttributes.MethodWithNotNullReturnValue(string arg)
        {
            return arg;
        }

        string AssemblyWithExternalAnnotations.InterfaceWithAttributes.NotNullProperty
        {
            get;
            set;
        }
    }
}
