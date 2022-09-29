using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Aperture.Tests.TagHelpers;

public class TagHelperOutputBuilder
{
    public TagHelperOutputBuilder()
    {
        _getChildContentAsync = (b, encoder) =>
        {
            var tagHelperContent = new DefaultTagHelperContent();
            tagHelperContent.SetHtmlContent(string.Empty);
            return Task.FromResult<TagHelperContent>(tagHelperContent);
        };

    }

    public const string DefaultTagName = "test";
    private string _tagName = DefaultTagName;
    private TagHelperAttributeList _attributes= new ();

    private Func<bool, HtmlEncoder, Task<TagHelperContent>> _getChildContentAsync;

    public TagHelperOutputBuilder WithTagName(string name)
    {
        _tagName = name;
        return this;
    }

    public TagHelperOutputBuilder WithAttributes(TagHelperAttributeList attributes)
    {
        _attributes = attributes;
        return this;
    }

    public TagHelperOutputBuilder WithAttribute(TagHelperAttribute attribute)
    {
        _attributes.Add(attribute);
        return this;
    }

    public TagHelperOutputBuilder WithAttribute(string key, object value)
    {
        _attributes.Add(key, value);
        return this;
    }

    public TagHelperOutputBuilder RemoveAttribute(TagHelperAttribute attribute)
    {
        _attributes.Remove(attribute);
        return this;
    }

    public TagHelperOutputBuilder WithGetChildContentAsync(Func<bool, HtmlEncoder, Task<TagHelperContent>> method)
    {
        _getChildContentAsync = method;
        return this;
    }

    public TagHelperOutput Build()
    {
        return new TagHelperOutput(_tagName, _attributes, _getChildContentAsync);
    }

    public static implicit operator TagHelperOutput(TagHelperOutputBuilder builder)
    {
        return builder.Build();
    }
}