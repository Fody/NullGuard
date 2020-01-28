using System;
using System.Diagnostics.CodeAnalysis;

namespace AssemblyToProcessNullableRefTypes
{
    // Ensures that build does not fail in NRT mode when [AllowNull] is applied to an abstract class method, which is not allowed in implicit mode.
    public abstract class AbstractTestClass
    {
        public abstract void AllowNullOnAbstractMethod([AllowNull] string value);
    }
}
