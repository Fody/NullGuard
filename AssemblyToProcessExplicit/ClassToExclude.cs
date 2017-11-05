using JetBrains.Annotations;

public class ClassToExclude
{
    // ReSharper disable once UnusedParameter.Local
    public ClassToExclude([NotNull] string test)
    {
    }

    [NotNull]
    public string Test([NotNull] string text)
    {
        return text;
    }

    [NotNull]
    public string Property { get; set; }
}