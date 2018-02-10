class DerivedGenericClass2<T> : GenericBaseClass<string, T>
{
    public override string Method(T param)
    {
        return base.Method(default(T));
    }
    public override string Property
    {
        get => base.Property + "1";
        set => base.Property = value;
    }
}