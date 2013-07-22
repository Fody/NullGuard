using System;
using System.Collections.Generic;
using System.Linq;
using Mono.Cecil;
using NullGuard;

public class ModuleWeaver
{
    public ValidationFlags ValidationFlags { get; set; }
    public string[] DefineConstants { get; set; }
    public Action<string> LogInfo { get; set; }
    public Action<string> LogError { get; set; }
    public ModuleDefinition ModuleDefinition { get; set; }
    public IAssemblyResolver AssemblyResolver { get; set; }

    public ModuleWeaver()
    {
        LogInfo = s => { };
        LogError = s => { };
        ValidationFlags = ValidationFlags.AllPublic;
        DefineConstants = new string[0];
    }

    public void Execute()
    {
        var nullGuardAttribute = ModuleDefinition.GetNullGuardAttribute();

        if (nullGuardAttribute == null)
            nullGuardAttribute = ModuleDefinition.Assembly.GetNullGuardAttribute();

        if (nullGuardAttribute != null)
            ValidationFlags = (ValidationFlags)nullGuardAttribute.ConstructorArguments[0].Value;

        ReferenceFinder.FindReferences(AssemblyResolver, ModuleDefinition);
        var types = new List<TypeDefinition>(ModuleDefinition.GetTypes());

        CheckForBadAttributes(types);
        ProcessAssembly(types);
        RemoveAttributes(types);
        RemoveReference();
    }

    private void CheckForBadAttributes(List<TypeDefinition> types)
    {
        foreach (var typeDefinition in types)
        {
            foreach (var method in typeDefinition.AbstractMethods())
            {
                if (method.ContainsAllowNullAttribute())
                {
                    LogError(string.Format("Method '{0}' is abstract but has a [AllowNullAttribute]. Remove this attribute.", method.FullName));
                }
                foreach (var parameter in method.Parameters)
                {
                    if (parameter.ContainsAllowNullAttribute())
                    {
                        LogError(string.Format("Method '{0}' is abstract but has a [AllowNullAttribute]. Remove this attribute.", method.FullName));
                    }
                }
            }
        }
    }

    private void ProcessAssembly(List<TypeDefinition> types)
    {
        var isDebug = DefineConstants.Any(c => c == "DEBUG") && ReferenceFinder.DebugAssertMethod != null;

        var methodProcessor = new MethodProcessor(ValidationFlags, isDebug);
        var propertyProcessor = new PropertyProcessor(ValidationFlags, isDebug);

        foreach (var type in types)
        {
            if (type.ContainsAllowNullAttribute() || type.IsGeneratedCode())
                continue;

            foreach (var method in type.MethodsWithBody())
                methodProcessor.Process(method);

            foreach (var property in type.ConcreteProperties())
                propertyProcessor.Process(property);
        }
    }

    private void RemoveAttributes(List<TypeDefinition> types)
    {
        ModuleDefinition.RemoveAllowNullAttribute();
        ModuleDefinition.Assembly.RemoveAllowNullAttribute();
        foreach (var typeDefinition in types)
        {
            typeDefinition.RemoveNullGuardAttribute();

            foreach (var method in typeDefinition.Methods)
            {
                method.MethodReturnType.RemoveAllowNullAttribute();

                foreach (var parameter in method.Parameters)
                {
                    parameter.RemoveAllowNullAttribute();
                }
            }

            foreach (var property in typeDefinition.Properties)
            {
                property.RemoveAllowNullAttribute();
            }
        }
    }

    private void RemoveReference()
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