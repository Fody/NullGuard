#if(DEBUG)

using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using ApprovalTests;
using ApprovalTests.Reporters;
using Microsoft.Build.Utilities;
using NUnit.Framework;

[TestFixture]
[UseReporter(typeof(DiffReporter), typeof(ClipboardReporter))]
public class ApprovedTests
{
    [Test]
    public void ClassWithBadAttributes()
    {
        Approvals.Verify(Decompile(AssemblyWeaver.AfterAssemblyPaths[0], "ClassWithBadAttributes"));
    }

    [Test]
    public void ClassWithPrivateMethod()
    {
        Approvals.Verify(Decompile(AssemblyWeaver.AfterAssemblyPaths[0], "ClassWithPrivateMethod"));
    }

    [Test]
    public void ClassWithPrivateMethodNoAssert()
    {
        Approvals.Verify(Decompile(AssemblyWeaver.AfterAssemblyPaths[1], "ClassWithPrivateMethod"));
    }

    [Test]
    public void GenericClass()
    {
        Approvals.Verify(Decompile(AssemblyWeaver.AfterAssemblyPaths[0], "GenericClass`1"));
    }

    [Test]
    public void GenericClassNoAssert()
    {
        Approvals.Verify(Decompile(AssemblyWeaver.AfterAssemblyPaths[1], "GenericClass`1"));
    }

    [Test]
    public void Indexers()
    {
        Approvals.Verify(Decompile(AssemblyWeaver.AfterAssemblyPaths[0], "Indexers"));
    }

    [Test]
    public void InterfaceBadAttributes()
    {
        Approvals.Verify(Decompile(AssemblyWeaver.AfterAssemblyPaths[0], "InterfaceBadAttributes"));
    }

    [Test]
    public void InterfaceImplementations()
    {
        Approvals.Verify(Decompile(AssemblyWeaver.AfterAssemblyPaths[0], "InterfaceImplementations"));
    }

    [Test]
    public void NestedClasses()
    {
        Approvals.Verify(Decompile(AssemblyWeaver.AfterAssemblyPaths[0], "NestedClasses"));
    }

    [Test]
    public void SimpleClass()
    {
        Approvals.Verify(Decompile(AssemblyWeaver.AfterAssemblyPaths[0], "SimpleClass"));
    }

    [Test]
    public void SimpleClassNoAssert()
    {
        Approvals.Verify(Decompile(AssemblyWeaver.AfterAssemblyPaths[1], "SimpleClass"));
    }

    [Test]
    public void SkipIXamlMetadataProvider()
    {
        Approvals.Verify(Decompile(AssemblyWeaver.AfterAssemblyPaths[0], "XamlMetadataProvider"));
    }

    [Test]
    public void SpecialClass()
    {
        Approvals.Verify(Decompile(AssemblyWeaver.AfterAssemblyPaths[0], "SpecialClass"));
    }

    [Test]
    public void SpecialClassNoAssert()
    {
        Approvals.Verify(Decompile(AssemblyWeaver.AfterAssemblyPaths[1], "SpecialClass"));
    }

    [Test]
    public void UnsafeClass()
    {
        Approvals.Verify(Decompile(AssemblyWeaver.AfterAssemblyPaths[0], "UnsafeClass"));
    }

    [Test]
    public void InfosList()
    {
        Approvals.VerifyAll(AssemblyWeaver.Infos.OrderBy(e => e), "Infos: ");
    }

    [Test]
    public void WarnsList()
    {
        Approvals.VerifyAll(AssemblyWeaver.Warns.OrderBy(e => e), "Warns: ");
    }

    [Test]
    public void ErrorsList()
    {
        Approvals.VerifyAll(AssemblyWeaver.Errors.OrderBy(e => e), "Errors: ");
    }

    private static string Decompile(string assemblyPath, string identifier = "")
    {
        var exePath = GetPathToILDasm();

        if (!string.IsNullOrEmpty(identifier))
            identifier = "/item:" + identifier;

        using (var process = Process.Start(new ProcessStartInfo(exePath, String.Format("\"{0}\" /text /linenum {1}", assemblyPath, identifier))
        {
            RedirectStandardOutput = true,
            UseShellExecute = false,
            CreateNoWindow = true
        }))
        {
            var projectFolder = Path.GetFullPath(Path.GetDirectoryName(assemblyPath) + "\\..\\..\\..").Replace("\\", "\\\\");
            projectFolder = String.Format("{0}{1}\\\\", Char.ToLower(projectFolder[0]), projectFolder.Substring(1));

            process.WaitForExit(10000);

            string ilText = string.Join(Environment.NewLine, Regex.Split(process.StandardOutput.ReadToEnd(), Environment.NewLine)
                    .Where(l => !l.StartsWith("// ") && !string.IsNullOrEmpty(l))
                    .Select(l => l.Replace(projectFolder, ""))
                    .ToList());

            // Sort the custom attributes in the generated IL code because the order of custom attributes is not specified (see
            // http://stackoverflow.com/questions/480007) and not stable.
            ilText = Regex.Replace(
                    ilText,
                    @"(?<CustomInstance>[\t ]*\.custom instance void.*=[^)]+\).*((\r\n)|\n){1}){2,}",
                    match => string.Join("", match.Groups["CustomInstance"].Captures.Cast<Capture>().Select(x => x.Value).OrderBy(x => x.Trim())));

            // Getters and setters seem to be swapping order sometimes
            ilText = Regex.Replace(
                ilText,
                @"(?<getset>[\t ]*\.[gs]et instance [^)]+\).*((\r\n)|\n){1}){2}",
                match => string.Join("", match.Groups["getset"].Captures.Cast<Capture>().Select(x => x.Value).OrderBy(x => x.Trim())));

            return ilText;
        }
    }

    private static string GetPathToILDasm()
    {
        var path = Path.Combine(ToolLocationHelper.GetPathToDotNetFrameworkSdk(TargetDotNetFrameworkVersion.Version40), @"bin\NETFX 4.0 Tools\ildasm.exe");
        if (!File.Exists(path))
            path = path.Replace("v7.0", "v8.0");
        if (!File.Exists(path))
            Assert.Ignore("ILDasm could not be found");
        return path;
    }
}

#endif