using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

		using (var process = Process.Start(new ProcessStartInfo(exePath, string.Format("\"{0}\" /text /linenum {1}", assemblyPath, identifier))
		{
			RedirectStandardOutput = true,
			UseShellExecute = false,
			CreateNoWindow = true
		}))
		{
			process.WaitForExit(10000);

            var ilText = process.StandardOutput.ReadToEnd();

            // Sort the custom attributes in the generated IL code because the order of custom attributes is not specified (see
            // http://stackoverflow.com/questions/480007) and not stable.
            ilText = Regex.Replace(
                    ilText,
                    @"(?<CustomInstance>[\t ]*\.custom instance void.*=[^)]+\).*((\r\n)|\n){1}){2,}",
                    match => string.Join("", match.Groups["CustomInstance"].Captures.Cast<Capture>().Select(x => x.Value).OrderBy(x => x.Trim())));

            var stringBuilder = new StringBuilder();
            using (var reader = new StringReader(ilText))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (line.Contains(".line "))
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
    }

	static string GetPathToILDasm()
	{
		var path = Path.Combine(ToolLocationHelper.GetPathToDotNetFrameworkSdk(TargetDotNetFrameworkVersion.Version40), @"bin\NETFX 4.0 Tools\ildasm.exe");
		if (!File.Exists(path))
			path = path.Replace("v7.0", "v8.0");
		if (!File.Exists(path))
			Assert.Ignore("ILDasm could not be found");
		return path;
	}
}