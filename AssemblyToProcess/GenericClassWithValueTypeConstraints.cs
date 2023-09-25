// No guards should be added for generic types with value type constraints

using System.Collections.Generic;

public class GenericClassWithValueTypeConstraints<T> where T: struct
{
    public T NonNullProperty { get; set; }

    public T NonNullMethod()
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
    public GenericClassWithValueTypeConstraints<int> Integer => new();

    public GenericClassWithValueTypeConstraints<KeyValuePair<string, string>> Struct => new();
}
