namespace Aperture.Exceptions;

public abstract class ApertureApplicationException: ApplicationException
{
    protected ApertureApplicationException():base()
    {
    }

    protected ApertureApplicationException(string message) : base(message)
    {
    }

    protected ApertureApplicationException(string message, Exception innerException):base(message, innerException)
    {
    }
}