// Type checked guards should be added for generic types with no type constraints

using System.Collections.Generic;

public class GenericClass<T>
{
    public T NonNullProperty { get; set; }

    public T NotNullMethod()
    {
        return default(T);
    }

    public U GenericMethod<U>(T t, U u)
    {
        return default(U);
    }
}

public class GenericClassFactory
{
    public GenericClass<int> Integer => new GenericClass<int>();

    public GenericClass<KeyValuePair<string, string>> Struct => new GenericClass<KeyValuePair<string, string>>();

    public GenericClass<object> Object => new GenericClass<object>();

    public GenericClass<SimpleClass> Class => new GenericClass<SimpleClass>();
}
