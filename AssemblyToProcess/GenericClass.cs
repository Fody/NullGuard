// Type checked guards should be added for generic types with no type constraints

public class GenericClass<T>
{
    public T NonNullProperty { get; set; }

    public U GenericMethod<U>(T t, U u)
    {
        return u;
    }
}