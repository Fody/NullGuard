public class DerivedClassClassWithMixedGenericInterfaces : BaseClassWithMixedGenericInterfaces
{
    public override int Method(bool param)
    {
        return 1;
    }

    public string Method(string param)
    {
        return default;
    }

    public override bool Property { get; set; }
}