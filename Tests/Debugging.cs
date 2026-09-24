using System;
using TestsCommon;
using NetFrameworkSmokeTest;

public class Debugging
{
    // [Test] // just to manually step through the code and verify that breakpoints are hit properly.
    void TestDebugging()
    {
        var c1 = new Class1("1");

        Shared.Throws<InvalidOperationException>(() => c1.Test("2"));

        c1.Test2("3");
    }
}