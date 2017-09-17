using System.Diagnostics;
using System.IO;
using System.Text;
using NUnit.Framework;

public static class Decompiler
{
    public static string Decompile(string assemblyPath, string identifier = "")
    {
        var exePath = GetPathToIldasm();

        if (!string.IsNullOrEmpty(identifier))
        {
            identifier = $"/item:{identifier}";
        }

        var startInfo = new ProcessStartInfo(exePath, $"\"{assemblyPath}\" /text /linenum /source {identifier}")
        {
            RedirectStandardOutput = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };
        using (var process = Process.Start(startInfo))
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

    static string GetPathToIldasm()
    {
        var path = SdkToolsHelper.GetSdkToolPath("ildasm.exe");
        if (!File.Exists(path))
        {
            Assert.Ignore("ildasm could not be found");
        }
        return path;
    }
}