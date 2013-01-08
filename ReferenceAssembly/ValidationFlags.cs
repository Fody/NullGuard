using System;

namespace NullGuard
{
    [Flags]
    public enum ValidationFlags
    {
        None = 0,
        Properties = 1,
        Methods = 2,
        Arguments = 4,
        OutValues = 8,
        ReturnValues = 16,
        NonPublic = 32,
        AllPublicArguments = Properties | Methods | Arguments,
        AllPublic = AllPublicArguments | OutValues | ReturnValues,
        All = AllPublic | NonPublic
    }
}