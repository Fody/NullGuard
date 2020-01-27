#pragma warning disable CS8618

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

public class ClassWithGenericNestedClass
{
    public string Value { get; set; } // forces parent class to have nullable context

    public class NestedUnconstrained<T>
    {
        [DisallowNull]
        public T PossiblyNullPropertyWithDisallowNull { get; set; }

        [NotNull]
        public T PossiblyNullPropertyWithNotNull { get; set; }

        public void DisallowedNullAndNotNullRefValue([DisallowNull][NotNull]ref T nonNullArg)
        {
            nonNullArg = default!;
        }
    }

    public class NestedNotNull<T> where T : notnull
    {
        [AllowNull]
        public T NotNullPropertyWithAllowNull { get; set; }

        [MaybeNull]
        public T NotNullPropertyWithMaybeNull { get; set; }

        public bool MaybeNullOutValueWhenFalse([MaybeNullWhen(false)] out T maybeNullWhenFalseArg)
        {
            maybeNullWhenFalseArg = default!;
            return false;
        }
    }
}
