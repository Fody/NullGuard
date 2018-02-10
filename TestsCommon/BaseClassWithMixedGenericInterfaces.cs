public class BaseClassWithMixedGenericInterfaces : IGenericDerivedInterface<string, int, bool>
{
    public virtual int Method(bool param) => 0;

    bool IGenericBaseInterface<bool, int>.Property
    {
        get;
        set;
    }

    string IGenericDerivedInterface<string, int, bool>.DerivedMethod(bool derivedParam)
    {
        return default;
    }

    public virtual bool Property { get; set; }

    public bool Method(int param) => false;
}