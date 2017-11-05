using JetBrains.Annotations;

internal class NonPublicWithNested
{
    public class PublicNestedClass
    {
        [NotNull]
        public string MethodReturnsNull()
        {
            return null;
        }
    }
}