using Aperture.Services;
using FluentAssertions;

namespace Aperture.Tests.Services;

public class DefaultTimeProviderTests
{
    [Fact]
    public void Now_ShouldReturnCurrentTime()
    {
        var provider = new DefaultTimeProvider();
        var now = provider.Now;

        now.Should().BeCloseTo(DateTimeOffset.Now, TimeSpan.FromMilliseconds(20));
    }

    [Fact]
    public void RequestTime_ShouldReturnTimeOfProviderInstantiation()
    {
        var provider = new DefaultTimeProvider();
        var time = provider.RequestTime;

        time.Should().BeCloseTo(DateTimeOffset.Now, TimeSpan.FromMilliseconds(20));
    }

    [Fact]
    public void RequestTime_ShouldNotChangeAsTimeElapses()
    {
        var provider = new DefaultTimeProvider();
        var time1 = provider.RequestTime;
        Thread.Sleep(20);
        var time2 = provider.RequestTime;

        time2.Should().Be(time1);
    }

    [Fact]
    public void FileTimeStamp_ShouldReturnDotSeparatedDateInExpectedFormat()
    {
        var provider = new DefaultTimeProvider();
        var timestamp = provider.FileTimeStamp;
        
        timestamp.Should().MatchRegex(@"\d{4}\.\d{2}\.\d{2}");
        var segments = timestamp.Split(".").Select(s => Convert.ToInt32(s)).ToArray();
        segments[0].Should().Be(provider.RequestTime.Year);
        segments[1].Should().Be(provider.RequestTime.Month);
        segments[2].Should().Be(provider.RequestTime.Day);
    }
}