using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.Build.Utilities;
using NUnit.Framework;

public static class Decompiler
{
    public static string Decompile(string assemblyPath, string identifier = "")
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

            var ilText = string.Join(Environment.NewLine, Regex.Split(process.StandardOutput.ReadToEnd(), Environment.NewLine)
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