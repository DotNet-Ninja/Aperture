namespace Aperture.Services;

public interface ITimeProvider
{
    public DateTimeOffset Now { get; }
    public DateTimeOffset RequestTime { get; }

    public string FileTimeStamp { get; }
}