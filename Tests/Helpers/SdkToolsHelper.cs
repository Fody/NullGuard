using System.IO;

public static class SdkToolsHelper
{
    private static readonly string[] PeVerifyProbingPaths =
    {
        @"C:\Program Files\Microsoft SDKs\Windows\v10.0A\bin\NETFX 4.6.1 Tools",
        @"C:\Program Files (x86)\Microsoft SDKs\Windows\v10.0A\bin\NETFX 4.6.1 Tools",
        @"C:\Program Files\Microsoft SDKs\Windows\v8.1A\bin\NETFX 4.5.1 Tools",
        @"C:\Program Files (x86)\Microsoft SDKs\Windows\v8.1A\bin\NETFX 4.5.1 Tools",
        @"C:\Program Files\Microsoft SDKs\Windows\v8.0A\bin\NETFX 4.0 Tools",
        @"C:\Program Files (x86)\Microsoft SDKs\Windows\v8.0A\bin\NETFX 4.0 Tools",
        @"C:\Program Files\Microsoft SDKs\Windows\v7.1\Bin\NETFX 4.0 Tools",
        @"C:\Program Files (x86)\Microsoft SDKs\Windows\v7.1\Bin\NETFX 4.0 Tools",
        @"C:\Program Files\Microsoft SDKs\Windows\v7.0A\bin\NETFX 4.0 Tools",
        @"C:\Program Files (x86)\Microsoft SDKs\Windows\v7.0A\Bin\NETFX 4.0 Tools",
        @"C:\Program Files\Microsoft SDKs\Windows\v7.0A\Bin",
        @"C:\Program Files (x86)\Microsoft SDKs\Windows\v7.0A\Bin",
        @"C:\Program Files\Microsoft SDKs\Windows\v6.0A\Bin",
        @"C:\Program Files (x86)\Microsoft SDKs\Windows\v6.0A\Bin",
        @"C:\Program Files (x86)\Microsoft Visual Studio 8\SDK\v2.0\bin"
    };

    public static string GetSdkToolPath(string execName)
    {
        foreach (var path in PeVerifyProbingPaths)
        {
            var file = Path.Combine(path, execName);
            if (File.Exists(file))
            {
                return file;
            }
        }

        return string.Empty;
    }
}