using System;
using NullGuard;

public class Indexers
{
    [NullGuard(ValidationFlags.All)] // This can be removed as soon as issue #37 is resolved
    public class NonNullable
    {
        public string this[string nonNullParam1, string nonNullParam2]
        {
            get { return "return value of NonNullable"; }
            set { }
        }
    }

    [NullGuard(ValidationFlags.All)] // This can be removed as soon as issue #37 is resolved
    public class PassThroughGetterReturnValue
    {
        public string this[[AllowNull] string returnValue]
        {
            get { return returnValue; }
        }
    }

    [NullGuard(ValidationFlags.All)] // This can be removed as soon as issue #37 is resolved
    public class AllowedNulls
    {
        [AllowNull]
        public string this[[AllowNull] string allowNull, int? nullableInt, string optional = null]
        {
            get { return null; }
            set { }
        }
    }
}