using System;
using NullGuard;

public class SimpleClass
{
    public SimpleClass()
    {
    }

    // Why would anyone place an out parameter on a ctor?! I don't know, but I'll support your idiocy.
    public SimpleClass([NotNull] out string nonNullOutArg)
    {
        nonNullOutArg = null;
    }

    public SimpleClass([NotNull] string nonNullArg, [AllowNull] string nullArg)
    {
        Console.WriteLine(nonNullArg + " " + nullArg);
    }

    public void SomeMethod([NotNull] string nonNullArg, string nullArg)
    {
        Console.WriteLine(nonNullArg);
    }

    [NotNull]
    public string NonNullProperty { get; set; }

    public string NullProperty { get; set; }

    [NotNull]
    public int? NonNullNullableProperty { get; set; }

    [NotNull]
    public string MethodWithReturnValue(bool returnNull)
    {
        return returnNull ? null : "";
    }

    public void MethodWithRef([NotNull] ref object returnNull)
    {
    }

    public void MethodWithGeneric<T>([NotNull] T returnNull)
    {
    }

    public void MethodWithGenericRef<T>([NotNull] ref T returnNull)
    {
    }

    public string MethodAllowsNullReturnValue()
    {
        return null;
    }

    public string MethodWithCanBeNullResult()
    {
        return null;
    }

    public void MethodWithOutValue([NotNull] out string nonNullOutArg)
    {
        nonNullOutArg = null;
    }

    public void MethodWithAllowedNullOutValue(out string nonNullOutArg)
    {
        nonNullOutArg = null;
    }

    public void PublicWrapperOfPrivateMethod()
    {
        SomePrivateMethod(null);
    }

    void SomePrivateMethod([NotNull] string x)
    {
        Console.WriteLine(x);
    }

    public void MethodWithTwoRefs([NotNull] ref string first, [NotNull] ref string second)
    {
    }

    public void MethodWithTwoOuts([NotNull] out string first, [NotNull] out string second)
    {
        first = null;
        second = null;
    }

    public void MethodWithOptionalParameter([NotNull] string optional = null)
    {
    }

    public void MethodWithOptionalParameterWithNonNullDefaultValue([NotNull] string optional = "default")
    {
    }

    public void MethodWithOptionalParameterWithNonNullDefaultValueButAllowNullAttribute(string optional = "default")
    {
    }

    public void MethodWithGenericOut<T>([NotNull] out T item)
    {
        item = default(T);
    }

    [NotNull]
    public T MethodWithGenericReturn<T>(bool returnNull)
    {
        return returnNull ? default(T) : Activator.CreateInstance<T>();
    }

    [NotNull]
    public object MethodWithOutAndReturn([NotNull] out string prefix)
    {
        prefix = null;
        return null;
    }

    public void MethodWithExistingArgumentGuard([NotNull] string x)
    {
        if (string.IsNullOrEmpty(x))
            throw new ArgumentException("x is null or empty.", "x");

        Console.WriteLine(x);
    }

    public void MethodWithExistingArgumentNullGuard([NotNull] string x)
    {
        if (string.IsNullOrEmpty(x))
            throw new ArgumentNullException("x");

        Console.WriteLine(x);
    }

    public void MethodWithExistingArgumentNullGuardWithMessage([NotNull] string x)
    {
        if (string.IsNullOrEmpty(x))
            throw new ArgumentNullException("x", "x is null or empty.");

        Console.WriteLine(x);
    }

    [NotNull]
    public string ReturnValueChecksWithBranchToRetInstruction()
    {
        // This is a regression test scenario for the "Branch to RET" issue described in https://github.com/Fody/NullGuard/issues/57.

        // It is important that the return value is assinged *before* the branch, otherwise the C# compiler emits
        // instructions before the RET instructions, which wouldn't trigger the original issue.
        string returnValue = null;

        // The following, not-reachable, branch will jump directly to the RET statement (at least with Roslyn 1.0 with
        // enabled optimizations flag) which triggers the issue (the return value checks will be skipped).
        if ("".Length == 42)
            throw new Exception("Not reachable");

        return returnValue;
    }

    public void OutValueChecksWithBranchToRetInstruction([NotNull] out string outParam)
    {
        // This is the same scenario as above, but for out parameters.

        outParam = null;

        if ("".Length == 42)
            throw new Exception("Not reachable");
    }

    [NotNull]
    public string GetterReturnValueChecksWithBranchToRetInstruction
    {
        get
        {
            // This is the same scenario as above, but for property getters.

            string returnValue = null;

            if ("".Length == 42)
                throw new Exception("Not reachable");

            return returnValue;
        }
    }

    public void OutValueChecksWithRetInstructionAsSwitchCase(int i, [NotNull] out string outParam)
    {
        // This is the same scenario as above, but with a SWITCH instruction with branch targets to RET
        // instructions (they are handled specially).
        // Note that its important to have more than sections to prove that all sections with only a RET instruction are handled.

        outParam = null;
        switch (i)
        {
            case 0:
                return;
            case 1:
                Console.WriteLine("1");
                break;
            case 2:
                return;
            case 3:
                Console.WriteLine("3");
                break;
        }
    }
}