﻿using System;
using System.Collections.Generic;
using System.Linq;
using Mono.Cecil;
using NullGuard;

public class ModuleWeaver
{
    public ValidationFlags ValidationFlags { get; set; }
    public bool UseDebugAssertion { get; set; }
    public List<string> DefineConstants { get; set; }
    public Action<string> LogInfo { get; set; }
    public Action<string> LogError { get; set; }
    public ModuleDefinition ModuleDefinition { get; set; }
    public IAssemblyResolver AssemblyResolver { get; set; }

    public ModuleWeaver()
    {
        LogInfo = s => { };
        LogError = s => { };
        ValidationFlags = ValidationFlags.AllPublic;
        DefineConstants = new List<string>();
    }

    public void Execute()
    {
        var nullGuardAttribute = ModuleDefinition.GetNullGuardAttribute();

        if (nullGuardAttribute == null)
            nullGuardAttribute = ModuleDefinition.Assembly.GetNullGuardAttribute();

        if (nullGuardAttribute != null)
        {
            ValidationFlags = (ValidationFlags)nullGuardAttribute.ConstructorArguments[0].Value;
            var outputOptionProperties = nullGuardAttribute.Properties.Where(n=>n.Name == "UseDebugAssertion").ToArray();
            if (outputOptionProperties.Any())
            {
                var outputOptionProperty = outputOptionProperties.First();
                UseDebugAssertion = (bool) outputOptionProperty.Argument.Value;
            }
        }

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

        var methodProcessor = new MethodProcessor(ValidationFlags, UseDebugAssertion, isDebug);
        var propertyProcessor = new PropertyProcessor(ValidationFlags, UseDebugAssertion, isDebug);

        foreach (var type in types)
        {
            if (type.IsInterface || type.ContainsAllowNullAttribute() || type.IsGeneratedCode() || type.HasInterface("Windows.UI.Xaml.Markup.IXamlMetadataProvider"))
                continue;

            foreach (var method in type.MethodsWithBody())
                methodProcessor.Process(method);

            foreach (var property in type.ConcreteProperties())
                propertyProcessor.Process(property);
        }
    }

    private void RemoveAttributes(List<TypeDefinition> types)
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