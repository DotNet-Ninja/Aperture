using Aperture.Configuration;

namespace Aperture.Tests.TagHelpers;

public class SiteSettingsBuilder
{
    public static readonly string DefaultBlogName = "Aperture";
    public static readonly int DefaultStartYear = 2022;
    public static readonly string DefaultCopyrightNotice = "All rights reserved.";

    private string _blogName = DefaultBlogName;
    private readonly CopyrightSettings _copyright = new ()
    {
        Notice = DefaultCopyrightNotice,
        StartYear = DefaultStartYear
    };

    public SiteSettingsBuilder WithBlogName(string name)
    {
        _blogName = name;
        return this;
    }

    public SiteSettingsBuilder WithCopyrightNotice(string notice)
    {
        _copyright.Notice = notice;
        return this;
    }

    public SiteSettingsBuilder WithCopyrightStartYear(int year)
    {
        _copyright.StartYear = year;
        return this;
    }

    public SiteSettings Build()
    {
        return new SiteSettings
        {
            Copyright = _copyright,
            Name = _blogName
        };
    }

    public static implicit operator SiteSettings(SiteSettingsBuilder builder)
    {
        return builder.Build();
    }
}