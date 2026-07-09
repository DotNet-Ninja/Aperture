using Aperture.Constants;
using Aperture.Models;

namespace Aperture.Services;

public class ErrorPageService : IErrorPageService
{
    public ErrorPageModel GetErrorPageModel(int statusCode)
    {
        var model = ErrorPages.FirstOrDefault(ep => ep.StatusCode == statusCode) ?? ErrorPages.First(ep => ep.StatusCode == 0);
        return model;
    }

    private static readonly List<ErrorPageModel> ErrorPages =
    [
        new ErrorPageModel
        {
            Actions =
            [
                new ErrorAction
                {
                    Text = "Home Page",
                    Url = "/",
                    Icon = BootstrapIcon.House,
                    CssClass = "primary"
                }
            ],
            ErrorName = "Bad Request",
            Image = "/images/400.BadRequest.jpg",
            Message = "We tried to take the shot, but the request came through darker than a midnight photo walk. Somewhere between you and the server, the camera confidently captured absolutely nothing.",
            StatusCode = 400,
            Title = "Looks Like the Lens Cap Had One Job"
        },
        new ErrorPageModel
        {
            Actions =
            [
                new ErrorAction
                {
                    Text = "Home Page",
                    Url = "/",
                    Icon = BootstrapIcon.House,
                    CssClass = "primary"
                }
            ],
            ErrorName = "Unauthorized",
            Image = "/images/401.Unauthorized.jpg",
            Message = "This area is for credentialed photographers, assistants, and people who definitely know where their badge is. Please sign in before attempting to sneak past the velvet rope with a camera bag and suspicious confidence.",
            StatusCode = 401,
            Title = "Sorry, You’re Not on the Shot List"
        },
        new ErrorPageModel
        {
            Actions =
            [
                new ErrorAction
                {
                    Text = "Home Page",
                    Url = "/",
                    Icon = BootstrapIcon.House,
                    CssClass = "primary"
                }
            ],
            ErrorName = "Forbidden",
            Image = "/images/403.Forbidden.jpg",
            Message = "You found the perfect angle, but unfortunately this page is standing behind a very serious “no photography” sign. Access is restricted, and even your best zoom lens cannot talk its way in.",
            StatusCode = 403,
            Title = "This Shot Is Off-Limits"
        },
        new ErrorPageModel
        {
            Actions =
            [
                new ErrorAction
                {
                    Text = "Home Page",
                    Url = "/",
                    Icon = BootstrapIcon.House,
                    CssClass = "primary"
                }
            ],
            ErrorName = "Not Found",
            Image = "/images/404.NotFound.jpg",
            Message = "Our photographer searched everywhere: wide angle, telephoto, macro, and that weird angle people only use when they’re trying to look artistic. The page you wanted has wandered out of frame.",
            StatusCode = 404,
            Title = "We Couldn’t Find the Frame"
        },
        new ErrorPageModel
        {
            Actions =
            [
                new ErrorAction
                {
                    Text = "Home Page",
                    Url = "/",
                    Icon = BootstrapIcon.House,
                    CssClass = "primary"
                }
            ],
            ErrorName = "Internal Server Error",
            Image = "/images/500.InternalServerError.jpg",
            Message = "Something broke backstage, and now the lights are tilted, the backdrop is collapsing, and someone is pretending the smoke machine was intentional. Our crew is checking the cables and trying not to blame the intern.",
            StatusCode = 500,
            Title = "The Studio Had a Tiny Meltdown"
        },
        new ErrorPageModel
        {
            Actions =
            [
                new ErrorAction
                {
                    Text = "Home Page",
                    Url = "/",
                    Icon = BootstrapIcon.House,
                    CssClass = "primary"
                }
            ],
            ErrorName = "Unknown Error",
            Image = "/images/GenericError.jpg",
            Message = "Something unexpected happened somewhere between the shutter click and the final print. The result is a little blurry, slightly overexposed, and definitely not going in the portfolio.",
            StatusCode = 0,
            Title = "That Didn’t Develop Properly"
        }
    ];
}