using JetBrains.Annotations;

class NonPublicWithNested
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