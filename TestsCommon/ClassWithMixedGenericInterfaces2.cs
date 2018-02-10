public class ClassWithMixedGenericInterfaces2 : IGenericDerivedInterface2<string, int, bool>
{
    public bool Method(string param)
    {
        return false;
    }

    string IGenericBaseInterface<string, bool>.Property { get; set; }

    string IGenericDerivedInterface2<string, int, bool>.DerivedMethod(bool derivedParam)
    {
        return default(string);
    }

    public bool Method(int param)
    {
        return false;
    }
}