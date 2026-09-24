using System;
using System.Text.RegularExpressions;
using TUnit.Assertions;
using VerifyTests;

namespace TestsCommon;

public static class Shared
{
    static readonly Regex NormalizeArgumentExceptionTextRegex = new(@" \(Parameter '(\w+)'\)");

    public static string NormalizedArgumentExceptionMessage(this Exception ex)
    {
        return NormalizeArgumentExceptionText(ex.Message);
    }

    public static string NormalizeArgumentExceptionText(string value)
    {
        return NormalizeArgumentExceptionTextRegex.Replace(value, "\r\nParameter name: $1");
    }

#pragma warning disable TUnitAssertions0002 // the assertion is deliberately blocked on synchronously
    // synchronous wrappers around TUnit's async Throws assertion, so they can be used with dynamic lambdas
    public static TException Throws<TException>(Action action)
        where TException : Exception
    {
        return Assert.That(action).Throws<TException>().GetAwaiter().GetResult()!;
    }

    public static TException Throws<TException>(Func<object> function)
        where TException : Exception
    {
        return Assert.That(() => function()).Throws<TException>().GetAwaiter().GetResult()!;
    }
#pragma warning restore TUnitAssertions0002

    public static VerifySettings With(this VerifySettings settings, Action<VerifySettings> action)
    {
        var clone = new VerifySettings(settings);
        action(clone);
        return clone;
    }
}