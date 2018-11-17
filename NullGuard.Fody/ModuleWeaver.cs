using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml;

using Fody;
using Mono.Cecil;
using NullGuard;

public partial class ModuleWeaver: BaseModuleWeaver
{
    public ValidationFlags ValidationFlags { get; set; }
    public bool IncludeDebugAssert = true;
    bool isDebug;
    NullGuardMode nullGuardMode;
    ExplicitMode explicitMode;
    public Regex ExcludeRegex { get; set; }

    public ModuleWeaver()
    {
        ValidationFlags = ValidationFlags.AllPublic;
        DefineConstants = new List<string>();
    }

    public override void Execute()
    {
        ReadConfig();

        if (nullGuardMode == NullGuardMode.AutoDetect)
        {
            nullGuardMode = ModuleDefinition.AutoDetectMode();
        }

        if (nullGuardMode == NullGuardMode.Explicit)
        {
            explicitMode = new ExplicitMode();
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

        LogInfo($"Mode={nullGuardMode}, ValidationFlags={ValidationFlags}");

        FindReferences();
        var types = GetTypesToProcess();

        CheckForBadAttributes(types);
        ProcessAssembly(types);
        RemoveAttributes(types);
        RemoveReference();
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
        if (Config == null)
        {
            return;
        }

        ReadIncludeDebugAssert();
        ReadExcludeRegex();
        ReadMode();
    }

    void ReadIncludeDebugAssert()
    {
        var value = Config?.Attribute("IncludeDebugAssert")?.Value;
        if (value == null)
            return;

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
            ExcludeRegex = new Regex(regex, RegexOptions.Compiled | RegexOptions.CultureInvariant);
        }
    }

    void CheckForBadAttributes(List<TypeDefinition> types)
    {
        foreach (var typeDefinition in types)
        {
            foreach (var method in typeDefinition.AbstractMethods())
            {
                if (method.ContainsAllowNullAttribute())
                {
                    LogError($"Method '{method.FullName}' is abstract but has a [AllowNullAttribute]. Remove this attribute.");
                }
                foreach (var parameter in method.Parameters)
                {
                    if (parameter.ContainsAllowNullAttribute())
                    {
                        LogError($"Method '{method.FullName}' is abstract but has a [AllowNullAttribute]. Remove this attribute.");
                    }
                }
            }
        }
    }

    void ProcessAssembly(List<TypeDefinition> types)
    {
        isDebug = IncludeDebugAssert &&
                      DefineConstants.Any(c => c == "DEBUG") &&
                      DebugAssertMethod != null;

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

    void RemoveReference()
    {
        var referenceToRemove = ModuleDefinition.AssemblyReferences.FirstOrDefault(x => x.Name == "NullGuard");
        if (referenceToRemove == null)
        {
            LogInfo("\tNo reference to 'NullGuard.dll' found. References not modified.");
            return;
        }

        ModuleDefinition.AssemblyReferences.Remove(referenceToRemove);
        LogInfo("\tRemoving reference to 'NullGuard.dll'.");
    }
}