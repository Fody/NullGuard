using System;

namespace NullGuard
{
    /// <summary>
    /// Attribute to configure injection of [NotNull]/[ItemNotNull] attributes for ReSharper's static nullability analysis.
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class)]
    public class InjectJetBrainsAnnotationsAttribute : Attribute
    {
        /// <summary>
        /// Configure injection of [NotNull]/[ItemNotNull] attributes for ReSharper's static nullability analysis.
        /// </summary>
        /// <param name="notNullAttribute">
        /// Reference to <see cref="T:JetBrains.Annotations.NotNullAttribute" />, used to inject this attribute
        /// onto non-nullable methods, parameters, and properties. Use <see langword="null" /> to disable this injection.
        /// </param>
        /// <param name="itemNotNullAttribute">
        /// Reference to <see cref="T:JetBrains.Annotations.ItemNotNullAttribute" />, used to inject this attribute
        /// onto non-nullable <c>async</c> methods (supported by ReSharper 9.2+). Use <see langword="null" /> to disable this injection.
        /// </param>
        public InjectJetBrainsAnnotationsAttribute(Type notNullAttribute, Type itemNotNullAttribute)
        {
        }
    }
}