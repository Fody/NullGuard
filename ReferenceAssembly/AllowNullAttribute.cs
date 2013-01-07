using System;

namespace NullGuard
{

    [AttributeUsage(AttributeTargets.Parameter)]
    public class AllowNullAttribute : Attribute
    {
    }
}