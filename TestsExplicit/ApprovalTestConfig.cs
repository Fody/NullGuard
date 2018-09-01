
#if (NET472)
using ApprovalTests.Reporters;

[assembly: UseReporter(typeof(DiffReporter), typeof(AllFailingTestsClipboardReporter))]

#endif