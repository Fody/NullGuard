using System.Linq;
using Anotar.Custom;
using Mono.Cecil;
using Mono.Cecil.Rocks;

public class JetBrainsAnnotationsApplier
{
    private const string InjectJetBrainsAnnotationsAttributeType = "NullGuard.InjectJetBrainsAnnotationsAttribute";

    private readonly CustomAttribute _notNullAttribute;
    private readonly CustomAttribute _itemNotNullAttribute;

    private JetBrainsAnnotationsApplier(CustomAttribute notNullAttribute, CustomAttribute itemNotNullAttribute)
    {
        _notNullAttribute = notNullAttribute;
        _itemNotNullAttribute = itemNotNullAttribute;
    }

    public static JetBrainsAnnotationsApplier CreateForType(TypeDefinition type)
    {
        // Note that the InjectJetBrainsAnnotationsAttribute is defined as an (AllowMultiple=false) attribute
        // with exactly two Type constructor parameters.

        var configurationAttribute = GetInjectJetBrainsAnnotationsAttribute(type);

        if (configurationAttribute == null)
            return new JetBrainsAnnotationsApplier(null, null);

        return new JetBrainsAnnotationsApplier(
                GetReferencedAttribute(type.Module, configurationAttribute.ConstructorArguments[0]),
                GetReferencedAttribute(type.Module, configurationAttribute.ConstructorArguments[1]));
    }

    private static CustomAttribute GetInjectJetBrainsAnnotationsAttribute(TypeDefinition type)
    {
        // Two-step attribute lookup for testing purposes and to make it symmetric with the way the NullGuard attribute works
        return new[] { type.CustomAttributes, type.Module.Assembly.CustomAttributes }
                .Select(x => x.SingleOrDefault(a => a.AttributeType.FullName == InjectJetBrainsAnnotationsAttributeType))
                .FirstOrDefault(x => x != null);
    }

    private static CustomAttribute GetReferencedAttribute(ModuleDefinition module, CustomAttributeArgument referencedAttributeArgument)
    {
        var referencedAttributeType = (TypeReference) referencedAttributeArgument.Value;

        if (referencedAttributeType == null)
            return null;

        var defaultConstructor = referencedAttributeType.Resolve().GetConstructors().SingleOrDefault(x => !x.HasParameters);

        if (defaultConstructor == null)
        {
            LogTo.Error(
                    $"{InjectJetBrainsAnnotationsAttributeType} referenced invalid annotation type '{referencedAttributeType}' " +
                    "(doesn't contain an empty constructor).");
            return null;
        }

        return new CustomAttribute(module.ImportReference(defaultConstructor));
    }

    public void AddToMethod(MethodDefinition method)
    {
        AddIfNotPresent(method, _notNullAttribute);
    }

    public void AddToAsyncMethod(MethodDefinition method)
    {
        AddIfNotPresent(method, _itemNotNullAttribute);
    }

    public void AddToParameter(ParameterDefinition parameter)
    {
        AddIfNotPresent(parameter, _notNullAttribute);
    }

    public void AddToProperty(PropertyDefinition property)
    {
        AddIfNotPresent(property, _notNullAttribute);
    }

    private static void AddIfNotPresent(ICustomAttributeProvider attributeProvider, CustomAttribute attribute)
    {
        if (attribute != null)
        {
            if (!attributeProvider.CustomAttributes.Any(x => x.AttributeType.FullName == attribute.AttributeType.FullName))
                attributeProvider.CustomAttributes.Add(attribute);
        }
    }
}