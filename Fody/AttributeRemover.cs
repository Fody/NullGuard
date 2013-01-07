public partial class ModuleWeaver
{
    void RemoveAttributes()
    {
		ModuleDefinition.RemoveAllowNullAttribute();
		ModuleDefinition.Assembly.RemoveAllowNullAttribute();
        foreach (var typeDefinition in types)
        {
            foreach (var method in typeDefinition.Methods)
            {
                foreach (var parameter in method.Parameters)
                {
                    parameter.RemoveAllowNullAttribute();
                }
            }
        }
    }
}