namespace Aperture.Exceptions;

public class UnsupportedFileTypeException: ApertureApplicationException
{
    public UnsupportedFileTypeException() : base()
    {
    }

    public UnsupportedFileTypeException(string message) : base(message)
    {
    }

    public UnsupportedFileTypeException(string message, Exception innerException) : base(message, innerException)
    {
    }

    public static UnsupportedFileTypeException FromExtensionAndContentType(string extension, string contentType)
    {
        return new UnsupportedFileTypeException(
            $"Photo file type '{contentType}' with extension '{extension}' is not supported.");
    }
}