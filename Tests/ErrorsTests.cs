using NUnit.Framework;

[TestFixture]
public class ErrorsTests
{
    [Test]
    public void ErrorsForAbstract()
    {
        Assert.Contains("Method 'System.Void ClassWithBadAttributes::MethodWithNoNullCheckOnParam(System.String)' is abstract but has a [AllowNullAttribute]. Remove this attribute.", AssemblyWeaver.Errors);
        Assert.Contains("Method 'System.Void ClassWithBadAttributes::set_PropertyWithNoNullCheckOnSet(System.String)' is abstract but has a [AllowNullAttribute]. Remove this attribute.", AssemblyWeaver.Errors);
        Assert.Contains("Method 'System.Void InterfaceBadAttributes::MethodWithNoNullCheckOnParam(System.String)' is abstract but has a [AllowNullAttribute]. Remove this attribute.", AssemblyWeaver.Errors);
        Assert.Contains("Method 'System.Void InterfaceBadAttributes::set_PropertyWithNoNullCheckOnSet(System.String)' is abstract but has a [AllowNullAttribute]. Remove this attribute.", AssemblyWeaver.Errors);
    }
}