## This is an add-in for [Fody](https://github.com/SimonCropp/Fody/) 

[Introduction to Fody](http://github.com/SimonCropp/Fody/wiki/SampleUsage)

## Nuget package http://nuget.org/packages/NullGuard.Fody 

### Your Code


    public class Sample
    {
        public void SomeMethod(string arg)
        {
            // throws ArgumentNullException if arg is null.
        }

        public void AnotherMethod([AllowNull] string arg)
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
        [AllowNull] 
        public string NullProperty { get; set; }

        // Or just the setter.
        public string NullPropertyOnSet { get; [param: AllowNull] set; }
    }

### What gets compiled 

    public class SampleOutput
    {

        public string NullProperty{get;set}
    
        string nullPropertyOnSet;
        public string NullPropertyOnSet
        {
            get
            {
                var returnValue = nullPropertyOnSet;
                if (returnValue == null)
                {
                    throw new InvalidOperationException("Return value of property 'NullPropertyOnSet' is null.");
                }
                return returnValue;
            }
            set
            {
                nullPropertyOnSet = value;
            }
        }
    
        string someProperty;
        public string SomeProperty
        {
            get
            {
                if (someProperty == null)
                {
                    throw new InvalidOperationException("Return value of property 'SomeProperty' is null.");
                }
                return someProperty;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value", "Cannot set the value of property 'SomeProperty' to null.");
                }
                someProperty = value;
            }
        }

        public void AnotherMethod(string arg)
        {
        }

        public string MethodWithReturn()
        {
            var returnValue = SomeOtherClass.SomeMethod();
            if (returnValue == null)
            {
                throw new InvalidOperationException("Return value of method 'MethodWithReturn' is null.");
            }
            return returnValue;
        }

        public void SomeMethod(string arg)
        {
            if (arg == null)
            {
                throw new ArgumentNullException("arg");
            }
        }
    }
	
