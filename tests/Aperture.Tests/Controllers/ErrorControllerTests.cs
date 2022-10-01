using Aperture.Constants;
using Aperture.Controllers;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;

namespace Aperture.Tests.Controllers;

public class ErrorControllerTests
{
    private readonly Mock<ILogger<ErrorController>> _mockLogger = new();

    [Fact]
    public void ServerError_ShouldReturnExpectedView()
    {
        var controller = CreateController();

        var result = controller.ServerError();

        result.ViewName.Should().Be("~/Views/Error/ServerError.cshtml");
    }

    [Fact]
    public void ServerError_ShouldReturnStatus500()
    {
        var controller = CreateController();

        var result = controller.ServerError();

        result.StatusCode.Should().Be(HttpStatus.InternalServerError);
    }
    
    [Fact]
    public void AccessDeniedError_ShouldReturnExpectedView()
    {
        var controller = CreateController();

        var result = controller.AccessDenied();

        result.ViewName.Should().Be("~/Views/Error/AccessDenied.cshtml");
    }

    [Fact]
    public void AccessDenied_ShouldReturnStatus403()
    {
        var controller = CreateController();

        var result = controller.AccessDenied();

        result.StatusCode.Should().Be(HttpStatus.Forbidden);
    }

    private ErrorController CreateController()
    {
        return CreateController(_mockLogger.Object);
    }

    private ErrorController CreateController(ILogger<ErrorController> logger)
    {
        return new ErrorController(logger);
    }
}