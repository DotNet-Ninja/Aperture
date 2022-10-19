namespace Aperture.Exceptions;

public class StorageOperationException : ApertureApplicationException
{
    public StorageOperationException()
    {
    }

    public StorageOperationException(string message) : base(message)
    {
    }

    public StorageOperationException(string message, Exception innerException) : base(message, innerException)
    {
    }
}