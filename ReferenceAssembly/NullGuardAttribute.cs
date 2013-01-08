using System;

namespace NullGuard
{
    [AttributeUsage(AttributeTargets.Class)]
    public class NullGuardAttribute : Attribute
    {
        public NullGuardAttribute(ValidationFlags flags)
        {
            Flags = flags;
        }

        public ValidationFlags Flags { get; private set; }
    }
}