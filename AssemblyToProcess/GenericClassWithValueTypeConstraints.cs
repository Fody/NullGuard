// No guards should be added for generic types with value type constraints

using System.Collections.Generic;

public class GenericClassWithValueTypeConstraints<T> where T: struct 
{
    public T NonNullProperty { get; set; }

    public T NotNullMethod()
    {
        return default(T);
    }

    public U GenericMethod<U>(T t, U u) where U: struct
    {
        return default(U);
    }
}

public class GenericClassWithValueTypeConstraintsFactory
{
    public GenericClassWithValueTypeConstraints<int> Integer => new GenericClassWithValueTypeConstraints<int>();

    public GenericClassWithValueTypeConstraints<KeyValuePair<string, string>> Struct => new GenericClassWithValueTypeConstraints<KeyValuePair<string, string>>();
}
