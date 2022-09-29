using Aperture.Configuration;
using Aperture.Services;
using Aperture.TagHelpers;
using FluentAssertions;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Moq;

namespace Aperture.Tests.TagHelpers;

public class CopyrightTagHelperTests: TagHelperTest
{
    private readonly SiteSettingsBuilder _settingsBuilder = new SiteSettingsBuilder();

    private static readonly int Year = SiteSettingsBuilder.DefaultStartYear;
    
    [Fact]
    public void Process_ShouldReturnValueEndingWithConfiguredNotice()
    {
        var mockTime = ConfigureMockTimeProvider(Year, 1, 1);
        SiteSettings settings = _settingsBuilder;
        TagHelperContext context = ContextBuilder.WithId("id");
        TagHelperOutput output = OutputBuilder.WithTagName("copyright");
        var copyrightTagHelper = new CopyrightTagHelper(settings, mockTime.Object);

        copyrightTagHelper.Process(context, output);

        var html = RenderOutput(output);
        html.Should().EndWith(settings.Copyright.Notice);
    }

    [Fact]
    public void Process_WhenStartYearIsCurrentYear_ShouldReturnValueStartingWithCopyrightAndYear()
    {
        var expected = $"&copy; {Year}";
        var mockTime = ConfigureMockTimeProvider(Year, 1, 1);
        SiteSettings settings = _settingsBuilder;
        TagHelperContext context = ContextBuilder.WithId("id");
        TagHelperOutput output = OutputBuilder.WithTagName("copyright");
        var copyrightTagHelper = new CopyrightTagHelper(settings, mockTime.Object);

        copyrightTagHelper.Process(context, output);

        var html = RenderOutput(output);
        html.Should().StartWith(expected);
    }

    [Fact]
    public void Process_WhenStartYearIsLessThanCurrentYear_ShouldReturnValueStartingWithCopyrightAndYearRange()
    {
        var mockTime = ConfigureMockTimeProvider(Year + 1, 1, 1);
        SiteSettings settings = _settingsBuilder;
        var expected = $"&copy; {settings.Copyright.StartYear} - {Year+1}";
        TagHelperContext context = ContextBuilder.WithId("id");
        TagHelperOutput output = OutputBuilder.WithTagName("copyright");
        var copyrightTagHelper = new CopyrightTagHelper(settings, mockTime.Object);

        copyrightTagHelper.Process(context, output);

        var html = RenderOutput(output);
        html.Should().StartWith(expected);
    }

    [Fact]
    public void Process_WhenStartYearIsGreaterThanCurrentYear_ShouldReturnValueStartingWithCopyrightAndCurrentYear()
    {
        var mockTime = ConfigureMockTimeProvider(Year - 1, 1, 1);
        SiteSettings settings = _settingsBuilder;
        var expected = $"&copy; {Year-1}";
        TagHelperContext context = ContextBuilder.WithId("id");
        TagHelperOutput output = OutputBuilder.WithTagName("copyright");
        var copyrightTagHelper = new CopyrightTagHelper(settings, mockTime.Object);

        copyrightTagHelper.Process(context, output);

        var html = RenderOutput(output);
        html.Should().StartWith(expected);
    }

    private Mock<ITimeProvider> ConfigureMockTimeProvider(DateTimeOffset date)
    {
        var result = new Mock<ITimeProvider>();
        result.Setup(mock => mock.Now).Returns(date);
        result.Setup(mock => mock.RequestTime).Returns(date);
        return result;
    }

    private Mock<ITimeProvider> ConfigureMockTimeProvider(int year, int month, int day, TimeSpan? offset = null)
    {
        var timeOffset = offset ?? TimeSpan.Zero;
        var date = new DateTimeOffset(new DateTime(year, month, day), timeOffset);
        return ConfigureMockTimeProvider(date);
    }
}