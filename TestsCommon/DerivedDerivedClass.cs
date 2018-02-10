class DerivedDerivedClass : EmptyDerivedClass<string>
{
    public override int Method(string param)
    {
        return base.Method(param + "1");
    }

    public override int Property
    {
        get => base.Property + 1;
        set => base.Property = value;
    }
}