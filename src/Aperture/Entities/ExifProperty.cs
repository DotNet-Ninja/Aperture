namespace Aperture.Entities;

public class ExifProperty
{
    private string _displayName = string.Empty;
    private string _displayValue = string.Empty;


    public int Id { get; set; } = 0;
    public string Name { get; set; } = string.Empty;

    public string DisplayName
    {
        get => string.IsNullOrWhiteSpace(_displayName) ? Name : _displayName;
        set=> _displayName = value;
    }
    public string Value { get; set; } = string.Empty;
    public string DisplayValue
    {
        get => string.IsNullOrWhiteSpace(_displayValue) ? Name : _displayValue;
        set => _displayValue = value;
    }
}