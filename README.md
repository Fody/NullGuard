## This is an add-in for [Fody](https://github.com/Fody/Fody/) 

[Introduction to Fody](http://github.com/Fody/Fody/wiki/SampleUsage)

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
	
## How to use

### 1. Ensure nuget is installed 

[NuGet Visual Studio package](http://visualstudiogallery.msdn.microsoft.com/27077b70-9dad-4c64-adcf-c7cf6bc9970c) (required to consume addins via nuget)

### 2. Install the Fody Visual Studio package 

[Fody Visual Studio package](http://visualstudiogallery.msdn.microsoft.com/074a2a26-d034-46f1-8fe1-0da97265eb7a) 

The Visual Studio package is only to help you configure Fody for your projects. It is not required to build on your machine or build servers.

### 3. Enable Fody for a project 

  * Open your solution Visual Studio
  * Select the project in Solution Explorer
  * Enable Fody for the project by using the top level menu 'Project > Fody > Enable'. Click OK. 
  
  ![ProjectEnable.jpg](https://github.com/Fody/Fody/wiki/ProjectEnable.jpg)

Notice a file `FodyWeavers.xml` has been added to the project

  ![FodyWeaversInProject.jpg](https://github.com/Fody/Fody/wiki/FodyWeaversInProject.jpg)

Which will contain

    <?xml version="1.0" encoding="utf-8" ?>
    <Weavers>
    </Weavers>

### 4. Add the weaver nuget package

Install the NullGuard package using NuGet. See [Using the package manager console](http://docs.nuget.org/docs/start-here/using-the-package-manager-console) for more info.

    Install-Package NullGuard.Fody

Notice `FodyWeavers.xml` will now look like this:

    <?xml version="1.0" encoding="utf-8" ?>
    <Weavers>
        <NullGuard/> 
    </Weavers>

### 5. Build

Now have a look at your assembly in your favourite decompiler. 

## Attributes

Where and how injection occurs can be controlled via attributes. The NullGuard.Fody nuget ships with an assembly containing these attributes.

	namespace NullGuard
	{
	    /// <summary>
	    /// Prevents the injection of null checking.
	    /// </summary>
	    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.ReturnValue | AttributeTargets.Property)]
	    public class AllowNullAttribute : Attribute
	    {
	    }
	    
	    /// <summary>
	    /// Allow specific categories of members to be targeted for injection. <seealso cref="ValidationFlags"/>
	    /// </summary>
	    [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class)]
	    public class NullGuardAttribute : Attribute
	    {
	        /// <summary>
	        /// Initializes a new instance of the <see cref="NullGuardAttribute"/> with a <see cref="ValidationFlags"/>.
	        /// </summary>
	        /// <param name="flags">The <see cref="ValidationFlags"/> to use for the target this attribute is being applied to.</param>
	        public NullGuardAttribute(ValidationFlags flags)
	        {
	        }
	    }
	    
	    /// <summary>
	    /// Used by <see cref="NullGuardAttribute"/> to taget specific categories of members.
	    /// </summary>
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
