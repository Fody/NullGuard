public enum NullGuardMode
{
    /// <summary>
    /// Not null is implicit, allow null must be set explicit.
    /// </summary>
    Implicit,
    /// <summary>
    /// Not null must be set explicit, allow null is implicit.
    /// </summary>
    Explicit
}