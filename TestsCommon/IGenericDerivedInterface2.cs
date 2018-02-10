public interface IGenericDerivedInterface2<U1, U2, U3> : IGenericBaseInterface<U1, U3>
{
    U1 DerivedMethod(U3 derivedParam);

    U3 Method(U2 param);
}