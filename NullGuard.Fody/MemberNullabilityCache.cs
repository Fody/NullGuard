using System.IO;
using Mono.Cecil.Rocks;

class MemberNullabilityCache
{
    readonly Dictionary<string, AssemblyCache> cache = new();

    public MethodNullability GetOrCreate(MethodDefinition method)
    {
        return (MethodNullability)GetOrCreate(method, externalAnnotation => new MethodNullability(this, method, externalAnnotation));
    }

    public PropertyNullability GetOrCreate(PropertyDefinition property)
    {
        return (PropertyNullability)GetOrCreate(property, externalAnnotation => new PropertyNullability(this, property, externalAnnotation));
    }

    MemberNullability GetOrCreate(MemberReference member, Func<XElement, MemberNullability> createNew)
    {
        var module = member.Module;
        var assemblyName = module.Assembly.Name.Name;

        var key = DocCommentId.GetDocCommentId((IMemberDefinition)member);

        if (!cache.TryGetValue(assemblyName, out var assemblyCache))
        {
            assemblyCache = new(module.FileName);
            cache.Add(assemblyName, assemblyCache);
        }

        return assemblyCache.GetOrCreate(key, createNew);
    }

    class AssemblyCache
    {
        readonly Dictionary<string, MemberNullability> cache = new();
        readonly Dictionary<string, XElement> externalAnnotations;

        public AssemblyCache(string moduleFileName)
        {
            var annotations = Path.ChangeExtension(moduleFileName, ".ExternalAnnotations.xml");

            if (!File.Exists(annotations))
            {
                return;
            }

            try
            {
                externalAnnotations = XDocument.Load(annotations)
                    .Element("assembly")?
                    .Elements("member")
                    .ToDictionary(member => member.Attribute("name")?.Value);
            }
            catch
            {
                // invalid file, ignore (TODO: log something?)
            }
        }

        public MemberNullability GetOrCreate(string key, Func<XElement, MemberNullability> createNew)
        {
            if (cache.TryGetValue(key, out var value))
            {
                return value;
            }

            XElement externalAnnotation = null;
            externalAnnotations?.TryGetValue(key, out externalAnnotation);

            value = createNew(externalAnnotation);

            cache.Add(key, value);

            return value;
        }
    }
}