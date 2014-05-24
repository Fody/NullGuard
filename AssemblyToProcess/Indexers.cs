using NullGuard;

public class Indexers
{
    public class NonNullable
    {
        public string this[string nonNullParam1, string nonNullParam2]
        {
            get { return "return value of NonNullable"; }
            set { }
        }
    }

    public class PassThroughGetterReturnValue
    {
        public string this[[AllowNull] string returnValue]
        {
            get { return returnValue; }
        }
    }

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