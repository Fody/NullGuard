using JetBrains.Annotations;
using NullGuard;

public class Indexers
{
    public class NonNullable
    {
        [NotNull]
        public string this[[NotNull] string nonNullParam1, [NotNull] string nonNullParam2]
        {
            get { return "return value of NonNullable"; }
            set { }
        }
    }

    public class PassThroughGetterReturnValue
    {
        [NotNull]
        public string this[[CanBeNull] string returnValue] => returnValue;
    }

    public class AllowedNulls
    {
        [CanBeNull]
        public string this[[CanBeNull] string allowNull, int? nullableInt, string optional = null]
        {
            get { return null; }
            set { }
        }
    }
}