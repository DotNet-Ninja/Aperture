using Aperture.TagHelpers;
using FluentAssertions;

namespace Aperture.Tests.TagHelpers;

public class IsInRoleTagHelperTests : AuthenticationTagHelperTestBase
{
    [Fact]
    public async Task ProcessAsync_WhenUserIsInDesignatedRole_ShouldReturnTag()
    {
        var user = CreateAuthenticatedUser(); // Default authenticated user has role claim "Member"
        var accessor = SetUpContextAccessor(user);
        var context = ContextBuilder.Build();
        var output = OutputBuilder.WithTagName("img").WithAttribute("src", "https://www.example.com/image.jpg").Build();
        var helper = new IsInRoleTagHelper(accessor.Object)
        {
            IsInRole = "Member"
        };

        await helper.ProcessAsync(context, output);

        output.TagName.Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    public async Task ProcessAsync_WhenUserIsNotInDesignatedRole_ShouldNotReturnTag()
    {
        var user = CreateAuthenticatedUser(); // Default authenticated user has role claim "Member"
        var accessor = SetUpContextAccessor(user);
        var context = ContextBuilder.Build();
        var output = OutputBuilder.WithTagName("img").WithAttribute("src", "https://www.example.com/image.jpg").Build();
        var helper = new IsInRoleTagHelper(accessor.Object)
        {
            IsInRole = "Administrator"
        };

        await helper.ProcessAsync(context, output);

        output.TagName.Should().BeNullOrWhiteSpace();
    }

}