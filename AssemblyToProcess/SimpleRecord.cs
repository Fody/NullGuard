
public record SimpleRecord
{
    private int _backingField;

    public int InitPropertyWithBackingField
    {
        init => _backingField = value;
    }
};
