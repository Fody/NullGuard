using System;
using System.Text.RegularExpressions;
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

    public static VerifySettings With(this VerifySettings settings, Action<VerifySettings> action)
    {
        var clone = new VerifySettings(settings);
        action(clone);
        return clone;
    }
}