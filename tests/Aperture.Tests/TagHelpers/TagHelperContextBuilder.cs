using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Aperture.Tests.TagHelpers;

public class TagHelperContextBuilder
{
    public static readonly string DefaultTagId = "TagId";

    private TagHelperAttributeList _attributes = new ();
    private Dictionary<object, object> _items = new ();
    private string _id = DefaultTagId;

    public TagHelperContextBuilder WithId(string id)
    {
        _id = id;
        return this;
    }

    public TagHelperContextBuilder WithItems(Dictionary<object, object> items)
    {
        _items = items;
        return this;
    }

    public TagHelperContextBuilder WithItem(object key, object item)
    {
        _items.Add(key, item);
        return this;
    }

    public TagHelperContextBuilder RemoveItem(object key)
    {
        if (_items.ContainsKey(key))
        {
            _items.Remove(key);
        }

        return this;
    }

    public TagHelperContextBuilder ClearItems()
    {
        _items = new Dictionary<object, object> ();
        return this;
    }

    public TagHelperContextBuilder UpdateItem(object key, object item)
    {
        if (_items.ContainsKey(key))
        {
            _items[key]=item;
            return this;
        }
        return WithItem(key, item);
    }

    public TagHelperContextBuilder WithAttributes(TagHelperAttributeList attributes)
    {
        _attributes = attributes;
        return this;
    }

    public TagHelperContextBuilder WithAttribute(TagHelperAttribute attribute)
    {
        _attributes.Add(attribute);
        return this;
    }

    public TagHelperContextBuilder WithAttribute(string key, object value)
    {
        _attributes.Add(key, value);
        return this;
    }

    public TagHelperContextBuilder RemoveAttribute(TagHelperAttribute attribute)
    {
        _attributes.Remove(attribute);
        return this;
    }

    public TagHelperContext Build()
    {
        return new TagHelperContext(_attributes, _items, _id);
    }

    public static implicit operator TagHelperContext(TagHelperContextBuilder builder)
    {
        return builder.Build();
    }


}