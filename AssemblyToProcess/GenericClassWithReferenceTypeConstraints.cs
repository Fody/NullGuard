// Simple guards should be added for generic types with reference type constraints

using System.Collections;

public class GenericClassWithReferenceTypeConstraints<T> where T: class 
{
    public T NonNullProperty { get; set; }

    public U GenericMethod<U>(T t, U u) where U: IList
    {
        return u;
    }
}
