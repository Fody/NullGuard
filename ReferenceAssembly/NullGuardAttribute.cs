using System;

namespace NullGuard
{
    /// <summary>
    /// Allow specific categories of members to be targeted for injection. <seealso cref="ValidationFlags"/>
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class)]
    public class NullGuardAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NullGuardAttribute"/> with a <see cref="ValidationFlags"/>.
        /// </summary>
        /// <param name="flags">The <see cref="ValidationFlags"/> to use for the target this attribute is being applied to.</param>
        // ReSharper disable once UnusedParameter.Local
        public NullGuardAttribute(ValidationFlags flags)
        {
        }
    }
}