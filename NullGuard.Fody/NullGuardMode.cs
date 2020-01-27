public enum NullGuardMode
{
    AutoDetect,
    /// <summary>
    /// Not null is implicit, allow null must be set explicit.
    /// </summary>
    Implicit,
    /// <summary>
    /// Not null must be set explicit, allow null is implicit.
    /// </summary>
    Explicit,
    /// <summary>
    /// C#8 nullable reference types
    /// </summary>
    NullableReferenceTypes
}