// Simple guards should be added for generic types with reference type constraints

using System.Collections;

using NullGuard;

public class GenericClassWithReferenceTypeConstraints<T> where T: class
{
    public T NonNullProperty { get; set; }

    [CanBeNull]
    public T CanBeNullProperty { get; set; }

    public T NonNullMethod()
    {
        return CanBeNullProperty;
    }

    public U GenericMethod<U>(T t, U u) where U: IList
    {
        return default(U);
    }

    public U GenericMethodReturnsParameter<U>(T t, [AllowNull] U u) where U: IList
    {
        return u;
    }
}

public class GenericClassWithReferenceTypeConstraintsFactory
{
    public GenericClassWithReferenceTypeConstraints<object> Object => new GenericClassWithReferenceTypeConstraints<object>();
}
