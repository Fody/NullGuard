using System.Text.RegularExpressions;
using System.Xml;

using Fody;
using NullGuard;

public partial class ModuleWeaver: BaseModuleWeaver
{
    public ValidationFlags ValidationFlags { get; set; }
    public bool IncludeDebugAssert = true;
    bool isDebug;
    NullGuardMode nullGuardMode;
    INullabilityAnalyzer nullabilityAnalyzer = new ImplicitModeAnalyzer();
    public Regex ExcludeRegex { get; set; }

    public override bool ShouldCleanReference => true;

    public ModuleWeaver()
    {
        ValidationFlags = ValidationFlags.AllPublic;
        DefineConstants = new();
    }

    public override void Execute()
    {
        ReadConfig();

        if (nullGuardMode == NullGuardMode.AutoDetect)
        {
            nullGuardMode = ModuleDefinition.AutoDetectMode();
        }

        switch (nullGuardMode)
        {
            case NullGuardMode.Explicit:
                nullabilityAnalyzer = new ExplicitModeAnalyzer();
                break;

            case NullGuardMode.NullableReferenceTypes:
                nullabilityAnalyzer = new NullableReferenceTypesModeAnalyzer();
                break;
        }

        var nullGuardAttribute = ModuleDefinition.GetNullGuardAttribute();

        if (nullGuardAttribute == null)
        {
            nullGuardAttribute = ModuleDefinition.Assembly.GetNullGuardAttribute();
        }

        if (nullGuardAttribute != null)
        {
            ValidationFlags = (ValidationFlags)nullGuardAttribute.ConstructorArguments[0].Value;
        }

        WriteInfo($"Mode={nullGuardMode}, ValidationFlags={ValidationFlags}");

        FindReferences();
        var types = GetTypesToProcess();

        nullabilityAnalyzer.CheckForBadAttributes(types, WriteError);
        ProcessAssembly(types);
        RemoveAttributes(types);
    }

    public override IEnumerable<string> GetAssembliesForScanning()
    {
        yield return "mscorlib";
        yield return "System.Runtime";
        yield return "System";
        yield return "netstandard";
        yield return "System.Diagnostics.Debug";
    }

    List<TypeDefinition> GetTypesToProcess()
    {
        var allTypes = new List<TypeDefinition>(ModuleDefinition.GetTypes());
        if (ExcludeRegex == null)
        {
            return allTypes;
        }
        return allTypes.Where(x => !ExcludeRegex.IsMatch(x.FullName)).ToList();
    }

    void ReadConfig()
    {
        ReadIncludeDebugAssert();
        ReadExcludeRegex();
        ReadMode();
    }

    void ReadIncludeDebugAssert()
    {
        var value = Config?.Attribute("IncludeDebugAssert")?.Value;
        if (value == null)
        {
            return;
        }

        try
        {
            IncludeDebugAssert = XmlConvert.ToBoolean(value.ToLowerInvariant());
        }
        catch
        {
            throw new WeavingException($"Could not parse 'IncludeDebugAssert' from '{value}'.");
        }
    }

    void ReadMode()
    {
        var modeAttribute = Config.Attribute("Mode");
        if (modeAttribute != null)
        {
            if (!Enum.TryParse(modeAttribute.Value, out nullGuardMode))
            {
                throw new WeavingException($"Could not parse 'NullGuardMode' from '{modeAttribute.Value}'.");
            }
        }
    }

    void ReadExcludeRegex()
    {
        var attribute = Config.Attribute("ExcludeRegex");
        var regex = attribute?.Value;
        if(!string.IsNullOrWhiteSpace(regex))
        {
            ExcludeRegex = new(regex, RegexOptions.Compiled | RegexOptions.CultureInvariant);
        }
    }

    void ProcessAssembly(List<TypeDefinition> types)
    {
        isDebug = IncludeDebugAssert &&
                      DefineConstants.Any(_ => _ == "DEBUG") &&
                      DebugAssertMethod != null;

        WriteInfo("Debug=" + isDebug);

        foreach (var type in types)
        {
            if (type.IsInterface ||
                type.ContainsAllowNullAttribute() ||
                type.IsGeneratedCode() ||
                type.HasInterface("Windows.UI.Xaml.Markup.IXamlMetadataProvider"))
            {
                continue;
            }

            foreach (var method in type.MethodsWithBody())
            {
                Process(method);
            }

            foreach (var property in type.ConcreteProperties())
            {
                Process(property);
            }
        }
    }

    void RemoveAttributes(List<TypeDefinition> types)
    {
        ModuleDefinition.Assembly.RemoveAllNullGuardAttributes();
        ModuleDefinition.RemoveAllNullGuardAttributes();
        foreach (var typeDefinition in types)
        {
            typeDefinition.RemoveAllNullGuardAttributes();

            foreach (var method in typeDefinition.Methods)
            {
                method.MethodReturnType.RemoveAllNullGuardAttributes();

                foreach (var parameter in method.Parameters)
                {
                    parameter.RemoveAllNullGuardAttributes();
                }
            }

            foreach (var property in typeDefinition.Properties)
            {
                property.RemoveAllNullGuardAttributes();
            }
        }
    }
}
