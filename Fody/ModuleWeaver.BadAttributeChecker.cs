public partial class ModuleWeaver
{

    void CheckForBadAttributes()
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
}