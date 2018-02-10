public interface IGenericDerivedInterface<U1, U2, U3> : IGenericBaseInterface<U3, U2>
{
    U1 DerivedMethod(U3 derivedParam);

    U3 Method(U2 param);
}