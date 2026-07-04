namespace Aperture.Services;

public interface ITimeProvider
{
    public DateTimeOffset Now { get; }
    public DateTimeOffset Request { get; }
}