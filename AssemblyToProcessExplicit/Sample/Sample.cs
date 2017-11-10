    using JetBrains.Annotations;

    using NullGuard;


    public class Sample
    {
        public void SomeMethod(string arg)
        {
            // throws ArgumentNullException if arg is null.
        }

        public void AnotherMethod([CanBeNull] string arg)
        {
            // arg may be null here
        }

        public string MethodWithReturn()
        {
            return SomeOtherClass.SomeMethod();
        }

        // Null checking works for automatic properties too.
        public string SomeProperty { get; set; }

        // can be applied to a whole property
        [CanBeNull] 
        public string NullProperty { get; set; }

        // Or just the setter.
        public string NullPropertyOnSet { get; [param: AllowNull] set; }
    }