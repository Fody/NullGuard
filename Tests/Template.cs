using System;
using System.Diagnostics;
using System.Linq;
using Mono.Cecil;
using NUnit.Framework;

[TestFixture]
public class Template
{
    [Test]
    [Ignore]
    public void LookAtIL()
    {
        var moduleDefinition = ModuleDefinition.ReadModule(GetType().Assembly.Location);
        var methods = moduleDefinition.GetType("Tempalate").Methods;
        var noWeaving = methods.First(x => x.Name == "NoWeaving").Body;
        var withWeaving = methods.First(x => x.Name == "WithWeaving").Body;
        Debug.WriteLine(noWeaving);
    }

    public void NoWeaving(string param)
    {
        //Some code u are curious how long it takes
        Console.WriteLine("Hello");
    }

    public void WithWeaving(string param)
    {
        if (param == null)
        {
            throw new NullReferenceException("param");
        }
        Trace.WriteLine("sdf");
    }
}