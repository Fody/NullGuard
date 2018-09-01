// No guards should be added for generic types with value trype constraints

public class GenericClassWithValueTypeConstraint<T> where T: struct 
{
    public T NonNullProperty { get; set; }

    public U GenericMethod<U>(T t, U u) where U: struct
    {
        return u;
    }
}
