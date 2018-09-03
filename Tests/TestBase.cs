#if (NET472)
using ApprovalTests.Core;
using ApprovalTests.Reporters;
#endif

public class TestBase
{
    static TestBase()
    {
#if (NET472)
        //TODO: this works around https://github.com/approvals/ApprovalTests.Net/issues/159
        var reporters = (IEnvironmentAwareReporter[]) FrameworkAssertReporter.INSTANCE.Reporters;
        reporters[3] = XUnit2Reporter.INSTANCE;
#endif
    }
}