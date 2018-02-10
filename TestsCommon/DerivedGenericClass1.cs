class DerivedGenericClass1<T> : GenericBaseClass<string, T>
{
    public override T Method(string param)
    {
        return base.Method(param + "1");
    }

    public override string Property
    {
        get => base.Property + "1";
        set => base.Property = value;
    }
}