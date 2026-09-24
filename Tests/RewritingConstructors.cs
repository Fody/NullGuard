using System;
using System.Reflection;
using System.Threading.Tasks;
using TestsCommon;
using VerifyTUnit;

public class RewritingConstructors
{
    [Test]
    public async Task RequiresNonNullArgument()
    {
        var type = AssemblyWeaver.Assembly.GetType("SimpleClass");
        var exception = Shared.Throws<TargetInvocationException>(() => Activator.CreateInstance(type, null, ""));
        await Verifier.Verify(exception.InnerException.NormalizedArgumentExceptionMessage());
    }

    [Test]
    public async Task RequiresNonNullOutArgument()
    {
        var type = AssemblyWeaver.Assembly.GetType("SimpleClass");
        var args = new object[1];
        var exception = Shared.Throws<TargetInvocationException>(() => Activator.CreateInstance(type, args));
        await Verifier.Verify(exception.InnerException.Message);
    }

    [Test]
    public void AllowsNullWhenAttributeApplied()
    {
        var type = AssemblyWeaver.Assembly.GetType("SimpleClass");
        Activator.CreateInstance(type, "", null);
    }

    [Test]
    public void AllowsNullWhenClassMatchExcludeRegex()
    {
        var type = AssemblyWeaver.Assembly.GetType("ClassToExclude");
        Activator.CreateInstance(type, new object[] {null});
    }
}