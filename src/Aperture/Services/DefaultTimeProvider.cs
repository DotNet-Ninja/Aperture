namespace Aperture.Services;

public class DefaultTimeProvider : ITimeProvider
{
    public DateTimeOffset Now => DateTimeOffset.Now;
    public DateTimeOffset RequestTime { get; } = DateTimeOffset.Now;

    public string FileTimeStamp =>
        $"{RequestTime.Year}.{RequestTime.Month.ToString().PadLeft(2, '0')}.{RequestTime.Day.ToString().PadLeft(2, '0')}";
}