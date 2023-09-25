
// ReSharper disable NotAccessedField.Local
// ReSharper disable InconsistentNaming
public record SimpleRecord
{
    private int _backingField;

    public int InitPropertyWithBackingField
    {
        init => _backingField = value;
    }
};
