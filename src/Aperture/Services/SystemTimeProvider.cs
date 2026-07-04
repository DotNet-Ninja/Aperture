namespace Aperture.Services;

public class SystemTimeProvider: ITimeProvider
{
    public DateTimeOffset Now => DateTimeOffset.Now;
    public DateTimeOffset Request { get; } = DateTimeOffset.Now;
}