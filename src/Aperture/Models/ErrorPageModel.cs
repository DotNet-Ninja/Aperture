namespace Aperture.Models;

public class ErrorPageModel
{
    public string Title { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;
    public string ErrorName { get; set; } = string.Empty;
    public int StatusCode { get; set; } = 0;

    public IList<ErrorAction> Actions { get; set; } = new List<ErrorAction>();
}