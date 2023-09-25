#pragma warning disable 1584,1711,1572,1581,1580

namespace NullGuard;

/// <summary>
/// Used by <see cref="NullGuardAttribute"/> to target specific categories of members.
/// </summary>
[Flags]
public enum ValidationFlags
{
    /// <summary>
    /// Don't process anything.
    /// </summary>
    None = 0,

    /// <summary>
    /// Process properties.
    /// </summary>
    Properties = 1,

    /// <summary>
    /// Process arguments of methods.
    /// </summary>
    Arguments = 2,

    /// <summary>
    /// Process out arguments of methods.
    /// </summary>
    OutValues = 4,

    /// <summary>
    /// Process return values of members.
    /// </summary>
    ReturnValues = 8,

    /// <summary>
    /// Process non-public members.
    /// </summary>
    NonPublic = 16,

    /// <summary>
    /// Process arguments, out arguments and return values of a method.
    /// </summary>
    Methods = Arguments | OutValues | ReturnValues,

    /// <summary>
    /// Process properties and arguments of public methods.
    /// </summary>
    AllPublicArguments = Properties | Arguments,

    /// <summary>
    /// Process public properties, and arguments, out arguments and return values of public methods.
    /// </summary>
    AllPublic = Properties | Methods,

    /// <summary>
    /// Process all properties, and arguments, out arguments and return values of all methods.
    /// </summary>
    All = AllPublic | NonPublic
}