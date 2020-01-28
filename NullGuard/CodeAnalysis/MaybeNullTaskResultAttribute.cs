using System;

namespace NullGuard.CodeAnalysis
{
    /// <summary>
    /// Prevents injection of null checking on task result values when return value checks are enabled (NRT mode only).
    /// </summary>
    [AttributeUsage(AttributeTargets.ReturnValue)]
    public class MaybeNullTaskResultAttribute : Attribute
    {
    }
}
