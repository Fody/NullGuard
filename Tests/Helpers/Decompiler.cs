using System.Diagnostics;
using System.IO;
using System.Text;
using Microsoft.Build.Utilities;
using NUnit.Framework;

public static class Decompiler
{
    public static string Decompile(string assemblyPath, string identifier = "")
    {
        var exePath = GetPathToILDasm();

        if (!string.IsNullOrEmpty(identifier))
        {
            identifier = "/item:" + identifier;
        }

        using (var process = Process.Start(
            new ProcessStartInfo(exePath, $"\"{assemblyPath}\" /text /linenum {identifier}")
            {
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            }))
        {
            process.WaitForExit(10000);

            var stringBuilder = new StringBuilder();
            string line;
            while ((line = process.StandardOutput.ReadLine()) != null)
            {
                if (line.Contains(".line "))
                {
                    continue;
                }
                if (line.Contains(".custom instance void ["))
                {
                    continue;
                }
                if (line.StartsWith("// "))
                {
                    continue;
                }
                if (line.Trim().Length == 0)
                {
                    continue;
                }
                if (line.StartsWith("  } // end of "))
                {
                    stringBuilder.AppendLine("  } ");
                    continue;
                }
                if (line.StartsWith("} // end of "))
                {
                    stringBuilder.AppendLine("}");
                    continue;
                }
                if (line.StartsWith("    .get instance "))
                {
                    continue;
                }
                if (line.StartsWith("    .set instance "))
                {
                    continue;
                }

                stringBuilder.AppendLine(line);
            }
            return stringBuilder.ToString();
        }
    }

    static string GetPathToILDasm()
    {
        var path = ToolLocationHelper.GetPathToDotNetFrameworkSdkFile("ildasm.exe", TargetDotNetFrameworkVersion.VersionLatest);
        if (!File.Exists(path))
            Assert.Ignore("ILDasm could not be found");
        return path;
    }
}