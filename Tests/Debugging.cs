using System;
using NetFrameworkSmokeTest;
using Xunit;

public class Debugging
{
    // [Fact] // just to manually step through the code and verify that breakpoints are hit properly.
    void TestDebugging()
    {
        var c1 = new Class1("1");

        Assert.Throws<InvalidOperationException>(() => c1.Test("2"));

        c1.Test2("3");
    }
}