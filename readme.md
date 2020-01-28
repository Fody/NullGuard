# <img src="/package_icon.png" height="30px"> NullGuard.Fody

[![Chat on Gitter](https://img.shields.io/gitter/room/fody/fody.svg)](https://gitter.im/Fody)
[![NuGet Status](https://img.shields.io/nuget/v/NullGuard.Fody.svg)](https://www.nuget.org/packages/NullGuard.Fody/)


### This is an add-in for [Fody](https://github.com/Fody/Home/)

**It is expected that all developers using Fody either [become a Patron on OpenCollective](https://opencollective.com/fody/contribute/patron-3059), or have a [Tidelift Subscription](https://tidelift.com/subscription/pkg/nuget-fody?utm_source=nuget-fody&utm_medium=referral&utm_campaign=enterprise). [See Licensing/Patron FAQ](https://github.com/Fody/Home/blob/master/pages/licensing-patron-faq.md) for more information.**


## Usage

See also [Fody usage](https://github.com/Fody/Home/blob/master/pages/usage.md).


### NuGet installation

Install the [NullGuard.Fody NuGet package](https://nuget.org/packages/NullGuard.Fody/) and update the [Fody NuGet package](https://nuget.org/packages/Fody/):

```powershell
PM> Install-Package Fody
PM> Install-Package NullGuard.Fody
```

The `Install-Package Fody` is required since NuGet always defaults to the oldest, and most buggy, version of any dependency.


### Modes

NullGuard supports three modes of operations, [*implicit*](#implicit-mode), [*explicit*](#explicit-mode) and [*nullable reference types*](#nrt-mode).

 * In [*implicit*](#implicit-mode) mode everything is assumed to be not-null, unless attributed with `[AllowNull]`. This is how NullGuard has been working always.
 * In [*explicit*](#explicit-mode) mode everything is assumed to be nullable, unless attributed with `[NotNull]`. This mode is designed to support the R# nullability analysis, using pessimistic mode.
 * In the new [*nullable reference types*](#nrt-mode) mode the C# 8 nullable reference type annotations are used to determine if a type may be null.

If not configured explicitly, NullGuard will auto-detect the mode as follows:

 * If C# 8 nullable attributes are detected then nullable reference types mode is used.
 * Referencing `JetBrains.Annotations` and using `[NotNull]` anywhere will switch to explicit mode.
 * Default to implicit mode if the above criteria is not met.


#### Implicit Mode


##### Your Code

```csharp
public class Sample
{
    public void SomeMethod(string arg)
    {
        // throws ArgumentNullException if arg is null.
    }

    public void AnotherMethod([AllowNull] string arg)
    {
        // arg may be null here
    }

    public void AndAnotherMethod(string? arg)
    {
        // arg may be null here
    }

    public string MethodWithReturn()
    {
        return SomeOtherClass.SomeMethod();
    }

    [return: AllowNull]
    public string MethodAllowsNullReturnValue()
    {
        return null;
    }

    public string? MethodAlsoAllowsNullReturnValue()
    {
        return null;
    }

    // Null checking works for automatic properties too.
    public string SomeProperty { get; set; }

    // can be applied to a whole property
    [AllowNull] 
    public string NullProperty { get; set; }

    // Or just the setter.
    public string NullPropertyOnSet { get; [param: AllowNull] set; }
}
```


##### What gets compiled

```csharp
public class SampleOutput
{
    public string NullProperty{get;set}

    string nullPropertyOnSet;
    public string NullPropertyOnSet
    {
        get
        {
            var returnValue = nullPropertyOnSet;
            if (returnValue == null)
            {
                throw new InvalidOperationException("Return value of property 'NullPropertyOnSet' is null.");
            }
            return returnValue;
        }
        set
        {
            nullPropertyOnSet = value;
        }
    }

    public string MethodAllowsNullReturnValue()
    {
        return null;
    }

    public string MethodAlsoAllowsNullReturnValue()
    {
        return null;
    }

    string someProperty;
    public string SomeProperty
    {
        get
        {
            if (someProperty == null)
            {
                throw new InvalidOperationException("Return value of property 'SomeProperty' is null.");
            }
            return someProperty;
        }
        set
        {
            if (value == null)
            {
                throw new ArgumentNullException("value", "Cannot set the value of property 'SomeProperty' to null.");
            }
            someProperty = value;
        }
    }

    public void AnotherMethod(string arg)
    {
    }

    public void AndAnotherMethod(string arg)
    {
    }

    public string MethodWithReturn()
    {
        var returnValue = SomeOtherClass.SomeMethod();
        if (returnValue == null)
        {
            throw new InvalidOperationException("Return value of method 'MethodWithReturn' is null.");
        }
        return returnValue;
    }

    public void SomeMethod(string arg)
    {
        if (arg == null)
        {
            throw new ArgumentNullException("arg");
        }
    }
}
```


#### Explicit Mode

If you are (already) using R#'s `[NotNull]` attribute in your code to explicitly annotate not null items, 
null guards will be added only for items that have an explicit `[NotNull]` annotation.

```csharp
public class Sample
{
    public void SomeMethod([NotNull] string arg)
    {
        // throws ArgumentNullException if arg is null.
    }

    public void AnotherMethod(string arg)
    {
        // arg may be null here
    }

    [NotNull]
    public string MethodWithReturn()
    {
        return SomeOtherClass.SomeMethod();
    }

    public string MethodAllowsNullReturnValue()
    {
        return null;
    }

    // Null checking works for automatic properties too.
    // Default in explicit mode is nullable
    public string NullProperty { get; set; }

    // NotNull can be applied to a whole property
    [NotNull]
    public string SomeProperty { get; set; }

    // or just the getter by overwriting the set method,
    [NotNull]
    public string NullPropertyOnSet { get; [param: AllowNull] set; }

    // or just the setter by overwriting the get method.
    [NotNull]
    public string NullPropertyOnGet { [return: AllowNull] get; set; }
}
```


Inheritance of nullability is supported in explicit mode, i.e. if you implement an interface or derive from a base method with `[NotNull]` annotations, 
null guards will be added to your implementation.

You may use the `[NotNull]` attribute defined in `JetBrains.Anntotations`, or simply define your own. However not referencing `JetBrains.Anntotations` will not auto-detect explicit mode, so you have to set this in the configuration.

Also note that using `JetBrains.Anntotations` will require to define [`JETBRAINS_ANNOTATIONS`](https://www.jetbrains.com/help/resharper/Code_Analysis__Annotations_in_Source_Code.html) to include the attributes in the assembly, so NullGuard can find them.
NullGuard will neither remove those attributes nor the reference to  `JetBrains.Anntotations`. To get rid of the attributes and the reference, you can use [JetBrainsAnnotations.Fody](https://github.com/tom-englert/JetBrainsAnnotations.Fody).
Just make sure NullGuard will run prior to [JetBrainsAnnotations.Fody](https://github.com/tom-englert/JetBrainsAnnotations.Fody).

### Nullable Reference Types Mode

Standard NRT annotations and attributes are used to determine the nullability of a type. Conditional postcondition attributes (ie. `[MaybeNullWhenAttribute]`) that indicate the value may sometimes be null causes the postcondition null check to be omitted.

```csharp
public class Sample
{
    // Allows null return values
    public string? MaybeGetValue()
    {
        return null;
    }

    // Throws InvalidOperationException since return value is not nullable
    public string MustReturnValue()
    {
        return null;
    }

    public void WriteValue(string arg)
    {
        // throws ArgumentNullException if arg is null.
    }

    public void WriteValue(string? arg) 
    {
        // arg may be null here
    }

    public void GenericMethod<T>(T arg) where T : notnull
    {
        // throws ArgumentNullException if arg is null.
    }

    public bool TryGetValue<T>(string key, [MaybeNullWhen(false)] out T value)
    {
        // throws ArgumentNullException if key is null.
        // out value is not checked.
    }
}

```

See https://docs.microsoft.com/en-us/dotnet/csharp/nullable-attributes for more information on the available nullable reference type attributes.

### Attributes

Where and how injection occurs can be controlled via attributes. The NullGuard.Fody nuget ships with an assembly containing these attributes.

```csharp
/// <summary>
/// Prevents the injection of null checking.
/// </summary>
[AttributeUsage(AttributeTargets.Parameter | AttributeTargets.ReturnValue | AttributeTargets.Property)]
public class AllowNullAttribute : Attribute
{
}

/// <summary>
/// Allow specific categories of members to be targeted for injection. <seealso cref="ValidationFlags"/>
/// </summary>
[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class)]
public class NullGuardAttribute : Attribute
{
    /// <summary>
    /// Initializes a new instance of the <see cref="NullGuardAttribute"/> with a <see cref="ValidationFlags"/>.
    /// </summary>
    /// <param name="flags">The <see cref="ValidationFlags"/> to use for the target this attribute is being applied to.</param>
    public NullGuardAttribute(ValidationFlags flags)
    {
    }
}

/// <summary>
/// Used by <see cref="NullGuardAttribute"/> to target specific categories of members.
/// </summary>
[Flags]
public enum ValidationFlags
{
    None = 0,
    Properties = 1,
    Arguments = 2,
    OutValues = 4,
    ReturnValues = 8,
    NonPublic = 16,
    Methods = Arguments | OutValues | ReturnValues,
    AllPublicArguments = Properties | Arguments,
    AllPublic = Properties | Methods,
    All = AllPublic | NonPublic
}
```

All NullGuard attributes are removed from the assembly as part of the build.

Attributes are checked locally at the member, and if there are no attributes then the class is checked. If the class has no attributes then the assembly is checked. Finally if there are no attributes at the assembly level then the default value is used.


#### NullGuardAttribute

`NullGuardAttribute` can be used at the class or assembly level. It takes a `ValidationFlags` parameter.

```csharp
    [assembly: NullGuard(ValidationFlags.None)] // Sets no guards at the assembly level
    
    [NullGuard(ValidationFlags.AllPublicArguments)] // Sets the default guard for class Foo
    public class Foo { ... }
```

#### ValidationFlags

The `ValidationFlags` determine how much checking NullGuard adds to your assembly.

 * `None` Does nothing.
 * `Properties` Adds null guard checks to properties getter (cannot return null) and setter (cannot be set to null).
 * `Arguments` Method arguments are checked to make sure they are not null. This only applies to normal arguments, and the incoming value of a ref argument.
 * `OutValues` Out and ref arguments of a method are checked for null just before the method returns.
 * `ReturnValues` Checks the return value of a method for null.
 * `NonPublic` Applies the other flags to all non-public members as well.
 * `Methods` Processes all arguments (normal, out and ref) and return values of methods.
 * `AllPublicArguments` Processes all methods (arguments and return values) and properties.
 * `AllPublic` Checks everything (properties, all method args and return values).


#### AllowNullAttribute and CanBeNullAttribute

These attributes allow you to specify which arguments, return values and properties can be set to null. `AllowNullAttribute` comes from the referenced project NullGuard adds. `CanBeNullAttribute` can come from anywhere, but is commonly used by Resharper.

```csharp
[AllowNull]
public string NullProperty { get; set; }

public void SomeMethod(string nonNullArg, [AllowNull] string nullArg) { ... }

[return: AllowNull]
public string MethodAllowsNullReturnValue() { ... }

public string PropertyAllowsNullGetButDoesNotAllowNullSet { [return: AllowNull] get; set; }

public string PropertyAllowsNullSetButDoesNotAllowNullGet { get; [param: AllowNull] set; }
```


### Configuration

For Release builds NullGuard will weave code that throws `ArgumentNullException`. For Debug builds NullGuard weaves `Debug.Assert`. 
If you want ArgumentNullException to be thrown for Debug builds then update FodyWeavers.xml to include:

```xml
<NullGuard IncludeDebugAssert="false" />
```

A complete example of `FodyWeavers.xml` looks like this:

```xml
<Weavers>
    <NullGuard IncludeDebugAssert="false" />
</Weavers>
```

You can also use RegEx to specify the name of a class to exclude from NullGuard.

```xml
<NullGuard ExcludeRegex="^ClassToExclude$" />
```

You can force the operation mode by setting it to `Explicit`, `Implicit` or `NullableReferenceTypes`, if the default `AutoDetect` does not detect the usage correctly.

```xml
<NullGuard Mode="Explicit" />
```


## Icon

Icon courtesy of [The Noun Project](https://thenounproject.com)