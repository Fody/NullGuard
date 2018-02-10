public interface IGenericBaseInterface<T1, T2>
{
    T2 Method(T1 param);

    T1 Property { get; set; }
}