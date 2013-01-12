using System;

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