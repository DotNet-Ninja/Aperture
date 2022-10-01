using Aperture.TagHelpers;
using FluentAssertions;

namespace Aperture.Tests.TagHelpers;

public class IsAuthenticatedTagHelperTests: AuthenticationTagHelperTestBase
{
    [Fact]
    public async Task ProcessAsync_WhenIsAuthenticatedAndUserAuthenticated_ReturnsElementContent()
    {
        var user = CreateAuthenticatedUser();
        var accessor = SetUpContextAccessor(user);
        var context = ContextBuilder.Build();
        var output = OutputBuilder.WithTagName("img").WithAttribute("src", "https://www.example.com/image.jpg").Build();
        var helper = new IsAuthenticatedTagHelper(accessor.Object);

        await helper.ProcessAsync(context, output);

        output.TagName.Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    public async Task ProcessAsync_WhenIsAuthenticatedAndUserNotAuthenticated_ReturnsEmptyContent()
    {
        var user = CreateUnauthenticatedUser();
        var accessor = SetUpContextAccessor(user);
        var context = ContextBuilder.Build();
        var output = OutputBuilder.WithTagName("img").WithAttribute("src", "https://www.example.com/image.jpg").Build();
        var helper = new IsAuthenticatedTagHelper(accessor.Object);

        await helper.ProcessAsync(context, output);

        output.TagName.Should().BeNullOrWhiteSpace();
    }

    [Fact]
    public async Task ProcessAsync_WhenNotIsAuthenticatedAndUserAuthenticated_ReturnsElementContent()
    {
        var user = CreateAuthenticatedUser();
        var accessor = SetUpContextAccessor(user);
        var context = ContextBuilder.Build();
        var output = OutputBuilder.WithTagName("img").WithAttribute("src", "https://www.example.com/image.jpg").Build();
        var helper = new IsAuthenticatedTagHelper(accessor.Object);
        helper.IsAuthenticated = false;

        await helper.ProcessAsync(context, output);

        output.TagName.Should().BeNullOrWhiteSpace();
    }

    [Fact]
    public async Task ProcessAsync_WhenNotIsAuthenticatedAndUserNotAuthenticated_ReturnsEmptyContent()
    {
        var user = CreateUnauthenticatedUser();
        var accessor = SetUpContextAccessor(user);
        var context = ContextBuilder.Build();
        var output = OutputBuilder.WithTagName("img").WithAttribute("src", "https://www.example.com/image.jpg").Build();
        var helper = new IsAuthenticatedTagHelper(accessor.Object);
        helper.IsAuthenticated = false;

        await helper.ProcessAsync(context, output);

        output.TagName.Should().NotBeNullOrWhiteSpace();
    }
}