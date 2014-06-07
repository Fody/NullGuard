using System;
using NullGuard;

public class NestedClasses
{
    public class OuterNestedClass
    {
        public class InnerNestedClass
        {
            public string NonNullProperty { get; set; }

            [AllowNull]
            public string AllowNullProperty { get; set; }

            internal string InternalProperty { get; set; }

            public void SomeMethod(string notNull)
            {
            }

            public void AllowNullArgMethod([AllowNull] string allowNull)
            {
            }

            internal void SomeInternalMethod(string x)
            {
            }
        }
    }
}