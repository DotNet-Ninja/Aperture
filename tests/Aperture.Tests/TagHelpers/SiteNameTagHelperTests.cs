using Aperture.Configuration;
using Aperture.TagHelpers;
using FluentAssertions;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Aperture.Tests.TagHelpers;

public class SiteNameTagHelperTests: TagHelperTest
{
    private const string DefaultBlogName = "Test Blog";
    private readonly SiteSettingsBuilder _settingsBuilder = new ();

    [Fact]
    public void Output_ShouldBeSettingsBlogName()
    {
        SiteSettings settings = _settingsBuilder.WithBlogName(DefaultBlogName);
        TagHelperContext context = ContextBuilder;
        TagHelperOutput output = OutputBuilder;
        var tagHelper = new SiteNameTagHelper(settings);
        
        tagHelper.Process(context, output);

        var result = RenderOutput(output);

        result.Should().Be(DefaultBlogName);
    }


    [Fact]
    public void Output_WhenNameNotSetInConfig_ShouldBePhotoBlogger()
    {
        SiteSettings settings = _settingsBuilder.WithBlogName(string.Empty);
        TagHelperContext context = ContextBuilder;
        TagHelperOutput output = OutputBuilder;
        var tagHelper = new SiteNameTagHelper(settings);

        tagHelper.Process(context, output);

        var result = RenderOutput(output);

        result.Should().Be("Aperture");
    }
}