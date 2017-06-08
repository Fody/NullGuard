using ApprovalTests.Namers.StackTraceParsers;
using Xunit;

namespace Tests.Helpers
{
    public class SkippableNamer : AttributeStackTraceParser
    {
        protected override string GetAttributeType()
        {
            return typeof(SkippableFactAttribute).FullName;
        }

        public override string ForTestingFramework
        {
            get { return "xUnit.Skippable"; }
        }
    }
}
