// ReSharper disable PossibleNullReferenceException
// ReSharper disable AssignNullToNotNullAttribute
#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;

using Mono.Cecil;

public class NullableReferenceTypesModeAnalyzer : INullabilityAnalyzer
{
    // https://github.com/dotnet/roslyn/blob/master/docs/features/nullable-metadata.md
    const string NullableContextAttributeTypeName = "System.Runtime.CompilerServices.NullableContextAttribute";
    const string NullableAttributeTypeName = "System.Runtime.CompilerServices.NullableAttribute";

    // Input Preconditions:

    const string AllowNullAttributeTypeName = "System.Diagnostics.CodeAnalysis.AllowNullAttribute";
    const string DisallowNullAttributeTypeName = "System.Diagnostics.CodeAnalysis.DisallowNullAttribute";

    // Output Postconditions:

    // Ignore the following attributes since they are conditional and thus the value may still be null under some
    // circumstances but we eventually want to add checks for these as well:
    // - System.Diagnostics.CodeAnalysis.NotNullIfNotNull
    // - System.Diagnostics.CodeAnalysis.NotNullWhen

    const string NotNullAttributeTypeName = "System.Diagnostics.CodeAnalysis.NotNullAttribute";

    // Treat all MaybeNull attributes as marking the type as nullable:

    const string MaybeNullAttributeTypeName = "System.Diagnostics.CodeAnalysis.MaybeNullAttribute";
    const string MaybeNullWhenAttributeTypeName = "System.Diagnostics.CodeAnalysis.MaybeNullWhenAttribute";

    // Special NullGuard attribute allow proper handling of nullable task results in all cases.

    const string MaybeNullTaskResultAttributeTypeName = "NullGuard.CodeAnalysis.MaybeNullTaskResultAttribute";

    // Implicit mode specific NullGuard AllowNull attribute which shouldn't be used in NRT mode

    const string NullGuardAllowNullAttributeTypeName = "NullGuard.AllowNullAttribute";

    enum Nullable
    {
        Unknown = -1,
        Oblivious = 0,
        NotAnnotated = 1,
        Annotated = 2
    }

    public void CheckForBadAttributes(List<TypeDefinition> types, Action<string> logError)
    {
        // Ensure user did not accidentally use NullGuard AllowNullAttribute instead of NRT attribute

        foreach (var typeDefinition in types)
        {
            foreach (var method in typeDefinition.Methods)
            {
                if (ContainsAttribute(method, NullGuardAllowNullAttributeTypeName))
                {
                    logError($"Method '{method.FullName}' has implicit mode NullGuard [AllowNullAttribute]. Use NRT attributes instead.");
                }
                foreach (var parameter in method.Parameters)
                {
                    if (ContainsAttribute(parameter, NullGuardAllowNullAttributeTypeName))
                    {
                        logError($"Method '{method.FullName}' has implicit mode NullGuard [AllowNullAttribute]. Use NRT attributes instead.");
                    }
                }
            }
        }
    }

    public bool AllowsNull(PropertyDefinition property)
    {
        // => do all checks for get/set method
        return false;
    }

    public bool AllowsNullInput(ParameterDefinition parameter, MethodDefinition method)
    {
        var contextAllowsNull = GetContextAllowsNull(method);

        return GetItemPreconditionAllowsNull(parameter)
               ?? GetGenericTypeAllowsNull(parameter.ParameterType, contextAllowsNull)
               ?? GetItemAllowsNull(parameter, contextAllowsNull)
               ?? true;
    }

    public bool AllowsNullOutput(ParameterDefinition parameter, MethodDefinition method)
    {
        var contextAllowsNull = GetContextAllowsNull(method);

        return GetItemPostconditionAllowsNull(parameter)
               ?? GetGenericTypeAllowsNull(parameter.ParameterType, contextAllowsNull)
               ?? GetItemAllowsNull(parameter, contextAllowsNull)
               ?? true;
    }

    public bool AllowsNullReturnValue(MethodDefinition method)
    {
        var contextAllowsNull = GetContextAllowsNull(method);

        return GetItemPostconditionAllowsNull(method.MethodReturnType)
               ?? GetGenericTypeAllowsNull(method.ReturnType, contextAllowsNull)
               ?? GetItemAllowsNull(method.MethodReturnType, contextAllowsNull)
               ?? true;
    }

    public bool AllowsNullAsyncTaskResult(MethodDefinition method, TypeReference resultType)
    {
        var contextAllowsNull = GetContextAllowsNull(method);

        return GetGenericTypeAllowsNull(resultType, contextAllowsNull)
               ?? GetReturnTypeAllowsNullTaskResult(method.MethodReturnType)
               ?? GetItemAllowsNull(method.MethodReturnType, contextAllowsNull, 1)
               ?? true;
    }

    public bool AllowsGetMethodToReturnNull(PropertyDefinition property, MethodDefinition getMethod)
    {
        var contextAllowsNull = GetContextAllowsNull(getMethod);

        return GetItemPostconditionAllowsNull(getMethod.MethodReturnType)
               ?? GetGenericTypeAllowsNull(getMethod.ReturnType, contextAllowsNull)
               ?? GetItemAllowsNull(getMethod.MethodReturnType, contextAllowsNull)
               ?? true;
    }

    public bool AllowsSetMethodToAcceptNull(PropertyDefinition property, MethodDefinition setMethod, ParameterDefinition valueParameter)
    {
        var contextAllowsNull = GetContextAllowsNull(setMethod);

        return GetItemPreconditionAllowsNull(valueParameter)
               ?? GetGenericTypeAllowsNull(valueParameter.ParameterType, contextAllowsNull)
               ?? GetItemAllowsNull(valueParameter, contextAllowsNull)
               ?? true;
    }

    static bool? GetItemPreconditionAllowsNull(ICustomAttributeProvider customAttributeProvider)
    {
        return ContainsAttribute(customAttributeProvider, AllowNullAttributeTypeName) ? true :
               ContainsAttribute(customAttributeProvider, DisallowNullAttributeTypeName) ? false :
               (bool?)null;
    }

    static bool? GetItemPostconditionAllowsNull(ICustomAttributeProvider customAttributeProvider)
    {
        return ContainsAnyAttribute(customAttributeProvider, MaybeNullAttributeTypeName, MaybeNullWhenAttributeTypeName) ? true :
               ContainsAttribute(customAttributeProvider, NotNullAttributeTypeName) ? false :
               (bool?)null;
    }

    static bool? GetGenericTypeAllowsNull(TypeReference typeReference, bool? contextAllowsNull)
    {
        // Only return true or null from this method, because if the type allows null we can stop here, otherwise
        // we want to continue checking if the actual parameter/return value allows null.

        if (typeReference.IsByReference)
        {
            typeReference = typeReference.GetElementType();
        }

        if (typeReference is GenericParameter genericParameter && GetItemAllowsNull(genericParameter, contextAllowsNull) == true)
        {
            return true;
        }

        return null;
    }

    bool? GetReturnTypeAllowsNullTaskResult(MethodReturnType methodReturnType)
    {
        return ContainsAttribute(methodReturnType, MaybeNullTaskResultAttributeTypeName) ? true : (bool?)null;
    }

    static bool? GetItemAllowsNull(ICustomAttributeProvider customAttributeProvider, bool? contextAllowsNull, int index = 0)
    {
        return GetAllowsNull(customAttributeProvider, NullableAttributeTypeName, index) ?? contextAllowsNull;
    }

    static bool? GetContextAllowsNull(MethodDefinition method)
    {
        return GetAllowsNull(method, NullableContextAttributeTypeName)
               ?? GetContextAllowsNull(method.DeclaringType);
    }

    static bool? GetContextAllowsNull(TypeDefinition? type)
    {
        if (type == null)
            return null;

        return GetAllowsNull(type, NullableContextAttributeTypeName)
               ?? GetContextAllowsNull(type.DeclaringType);
    }

    static bool ContainsAttribute(ICustomAttributeProvider customAttributeProvider, string fullName)
    {
        if (!customAttributeProvider.HasCustomAttributes)
            return false;

        return customAttributeProvider.CustomAttributes.Any(a => a.AttributeType.FullName == fullName);
    }

    static bool ContainsAnyAttribute(ICustomAttributeProvider customAttributeProvider, params string[] fullNames)
    {
        if (!customAttributeProvider.HasCustomAttributes)
            return false;

        return fullNames.Any(n => ContainsAttribute(customAttributeProvider, n));
    }

    static bool? GetAllowsNull(ICustomAttributeProvider customAttributeProvider, string attributeTypeName, int index = 0)
    {
        var nullable = GetNullableAnnotation(customAttributeProvider, attributeTypeName, index);

        return nullable switch
        {
            Nullable.NotAnnotated => false,
            Nullable.Annotated => true,
            Nullable.Oblivious => true,
            _ => null
        };
    }

    static Nullable GetNullableAnnotation(ICustomAttributeProvider customAttributeProvider, string attributeTypeName, int index)
    {
        if (!customAttributeProvider.HasCustomAttributes)
            return Nullable.Unknown;

        var attribute = customAttributeProvider.CustomAttributes
            .SingleOrDefault(a => a.AttributeType.FullName == attributeTypeName && a.ConstructorArguments.Count == 1);

        return attribute != null ? GetConstructorArgumentValue(attribute.ConstructorArguments[0], index) : Nullable.Unknown;
    }

    static Nullable GetConstructorArgumentValue(CustomAttributeArgument arg, int index)
    {
        var value = arg.Type.FullName switch
        {
            "System.Byte" => (byte)arg.Value,
            "System.Byte[]" => (byte)((CustomAttributeArgument[])arg.Value).Take(index + 1).Last().Value,
            _ => throw new InvalidOperationException("unexpected type: " + arg.Type.FullName)
        };

        return (Nullable)value;
    }
}