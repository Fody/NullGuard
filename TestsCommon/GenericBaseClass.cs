public class GenericBaseClass<T1, T2>
{
    public virtual T2 Method(T1 param)
    {
        return default;
    }

    public virtual T1 Method(T2 param)
    {
        return default;
    }

    public virtual T1 Property { get; set; }
}