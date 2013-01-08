using System;

namespace NullGuard
{
    [AttributeUsage(AttributeTargets.Class)]
    public class NullGuardAttribute : Attribute
    {
        public NullGuardAttribute(ValidationFlags flags)
        {
        }
    }
}