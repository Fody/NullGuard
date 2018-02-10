public class ClassWithMixedGenericInterfaces : IGenericDerivedInterface<string, int, bool>
{
    public int Method(bool param)
    {
        return 0;
    }

    bool IGenericBaseInterface<bool, int>.Property
    {
        get;
        set;
    }

    string IGenericDerivedInterface<string, int, bool>.DerivedMethod(bool derivedParam)
    {
        return default(string);
    }

    public bool Method(int param)
    {
        return false;
    }
}