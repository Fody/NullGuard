using System;
using System.Text;
using System.Text.RegularExpressions;
using VerifyTests;

namespace TestsCommon
{
    public static class Shared
    {
        private static readonly Regex NormalizeArgumentExceptionTextRegex = new(@" \(Parameter '(\w+)'\)");

        public static string NormalizedArgumentExceptionMessage(this Exception ex)
        {
            return NormalizeArgumentExceptionText(ex.Message);
        }

        public static string NormalizeArgumentExceptionText(string value)
        {
            return NormalizeArgumentExceptionTextRegex.Replace(value, "\r\nParameter name: $1");
        }
    }
}
