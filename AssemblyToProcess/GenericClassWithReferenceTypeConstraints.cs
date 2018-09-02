// Simple guards should be added for generic types with reference type constraints

using System.Collections;
using System.Collections.Generic;

public class GenericClassWithReferenceTypeConstraints<T> where T: class 
{
    public T NonNullProperty { get; set; }

    public T NotNullMethod()
    {
        return default(T);
    }

    public U GenericMethod<U>(T t, U u) where U: IList
    {
        return default(U);
    }
}

public class GenericClassWithReferenceTypeConstraintFactory
{
    public GenericClassWithReferenceTypeConstraints<object> Object => new GenericClassWithReferenceTypeConstraints<object>();

    public GenericClassWithReferenceTypeConstraints<SimpleClass> Class => new GenericClassWithReferenceTypeConstraints<SimpleClass>();
}
