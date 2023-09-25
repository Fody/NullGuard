// Type checked guards should be added for generic types with no type constraints

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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

    public Task<T> GetThing(Func<T> getThing)
    {
        return Task.Run(getThing);
    }

    public async Task<T> GetThing2(Func<T> getThing)
    {
        return await Task.Run(getThing);
    }
}

public class GenericClassFactory
{
    public GenericClass<int> Integer => new();

    public GenericClass<KeyValuePair<string, string>> Struct => new();

    public GenericClass<object> Object => new();

    public int GetThingAsync()
    {
        return new GenericClass<int>().GetThing(() => 0).Result;
    }

    public int GetThingAsync2()
    {
        return new GenericClass<int>().GetThing2(() => 0).Result;
    }

}
