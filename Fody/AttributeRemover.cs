public partial class ModuleWeaver
{
    private void RemoveAttributes()
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
}