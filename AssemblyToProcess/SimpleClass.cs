using System;
using NullGuard;
// ReSharper disable MemberCanBeMadeStatic.Local

public class SimpleClass
{
    public SimpleClass()
    {
    }

    // Why would anyone place an out parameter on a ctor?! I don't know, but I'll support your idiocy.
    public SimpleClass(out string nonNullOutArg)
    {
        nonNullOutArg = null;
    }

    public SimpleClass(string nonNullArg, [AllowNull] string nullArg)
    {
        Console.WriteLine(nonNullArg + " " + nullArg);
    }

    public void SomeMethod(string nonNullArg, [AllowNull] string nullArg)
    {
        Console.WriteLine(nonNullArg);
    }

    public string NonNullProperty { get; set; }

    [AllowNull]
    public string NullProperty { get; set; }

    public string PropertyAllowsNullGetButDoesNotAllowNullSet { [return: AllowNull] get; set; }

    public string PropertyAllowsNullSetButDoesNotAllowNullGet { get; [param: AllowNull] set; }

    public int? NonNullNullableProperty { get; set; }

    public string MethodWithReturnValue(bool returnNull)
    {
        return returnNull ? null : "";
    }

    public void MethodWithRef(ref object returnNull)
    {
    }

    public void MethodWithGeneric<T>(T returnNull)
    {
    }

    public void MethodWithGenericRef<T>(ref T returnNull)
    {
    }

    [return: AllowNull]
    public string MethodAllowsNullReturnValue()
    {
        return null;
    }

    [CanBeNull]
    public string MethodWithCanBeNullResult()
    {
        return null;
    }

    public void MethodWithOutValue(out string nonNullOutArg)
    {
        nonNullOutArg = null;
    }

    public void MethodWithAllowedNullOutValue([AllowNull]out string nonNullOutArg)
    {
        nonNullOutArg = null;
    }

    public void PublicWrapperOfPrivateMethod()
    {
        SomePrivateMethod(null);
    }

    void SomePrivateMethod(string x)
    {
        Console.WriteLine(x);
    }

    public void MethodWithTwoRefs(ref string first, ref string second)
    {
    }

    public void MethodWithTwoOuts(out string first, out string second)
    {
        first = null;
        second = null;
    }

    public void MethodWithOptionalParameter(string optional = null)
    {
    }

    public void MethodWithOptionalParameterWithNonNullDefaultValue(string optional = "default")
    {
    }

    public void MethodWithOptionalParameterWithNonNullDefaultValueButAllowNullAttribute([AllowNull] string optional = "default")
    {
    }

    public void MethodWithGenericOut<T>(out T item)
    {
        item = default;
    }

    public T MethodWithGenericReturn<T>(bool returnNull)
    {
        return returnNull ? default : Activator.CreateInstance<T>();
    }

    public object MethodWithOutAndReturn(out string prefix)
    {
        prefix = null;
        return null;
    }

    public void MethodWithExistingArgumentGuard(string x)
    {
        if (string.IsNullOrEmpty(x))
            throw new ArgumentException("x is null or empty.", "x");

        Console.WriteLine(x);
    }

    public void MethodWithExistingArgumentNullGuard(string x)
    {
        if (string.IsNullOrEmpty(x))
            throw new ArgumentNullException("x");

        Console.WriteLine(x);
    }

    public void MethodWithExistingArgumentNullGuardWithMessage(string x)
    {
        if (string.IsNullOrEmpty(x))
            throw new ArgumentNullException("x", "x is null or empty.");

        Console.WriteLine(x);
    }

    public string ReturnValueChecksWithBranchToRetInstruction()
    {
        // This is a regression test scenario for the "Branch to RET" issue described in https://github.com/Fody/NullGuard/issues/57.

        // It is important that the return value is assigned *before* the branch, otherwise the C# compiler emits
        // instructions before the RET instructions, which wouldn't trigger the original issue.
        string returnValue = null;

        // The following, not-reachable, branch will jump directly to the RET statement (at least with Roslyn 1.0 with
        // enabled optimizations flag) which triggers the issue (the return value checks will be skipped).
        if ("".Length == 42)
            throw new("Not reachable");

        return returnValue;
    }

    public void OutValueChecksWithBranchToRetInstruction(out string outParam)
    {
        // This is the same scenario as above, but for out parameters.

        outParam = null;

        if ("".Length == 42)
            throw new("Not reachable");
    }

    public string GetterReturnValueChecksWithBranchToRetInstruction
    {
        get
        {
            // This is the same scenario as above, but for property getters.

            string returnValue = null;

            if ("".Length == 42)
                throw new("Not reachable");

            return returnValue;
        }
    }

    public void OutValueChecksWithRetInstructionAsSwitchCase(int i, out string outParam)
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