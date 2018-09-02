// Type checked guards should be added for generic types with no type constraints

using System.Collections.Generic;

using NullGuard;

public class GenericClass<T>
{
    public T NonNullProperty { get; set; }

    [CanBeNull]
    public T CanBeNullProperty { get; set; }

    public T NonNullMethod()
    {
        return CanBeNullProperty;
    }

    public U GenericMethod<U>(T t, U u)
    {
        return default(U);
    }

    public U GenericMethodReturnsParameter<U>(T t, [AllowNull] U u)
    {
        return u;
    }
}

public class GenericClassFactory
{
    public GenericClass<int> Integer => new GenericClass<int>();

    public GenericClass<KeyValuePair<string, string>> Struct => new GenericClass<KeyValuePair<string, string>>();

    public GenericClass<object> Object => new GenericClass<object>();
}
