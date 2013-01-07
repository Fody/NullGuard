## This is an add-in for [Fody](https://github.com/SimonCropp/Fody/) 

Injects some very basic method timing code.

[Introduction to Fody](http://github.com/SimonCropp/Fody/wiki/SampleUsage)

## Nuget package http://nuget.org/packages/NullGuard.Fody 

### Your Code

	public class MyClass
	{
		public void MyMethod(string param)
		{
			//Some code that uses 'param'
		}
	}

### What gets compiled 

    public class MyClass
    {
        public void MyMethod()
        {
            if (param == null)
            {
                throw new NullReferenceException("param");
            }
			//Some code that uses 'param'
            }
        }
    }
	