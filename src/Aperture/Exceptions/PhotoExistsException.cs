using Aperture.Entities;
using Microsoft.EntityFrameworkCore;

namespace Aperture.Exceptions;

public class PhotoExistsException: ApertureApplicationException
{
    public PhotoExistsException() : base()
    {
    }

    public PhotoExistsException(string message) : base(message)
    {
    }

    public PhotoExistsException(string message, Exception innerException) : base(message, innerException)
    {
    }

    public static PhotoExistsException FromDbUpdateException(Photo photo, DbUpdateException innerException)
    {
        return new PhotoExistsException($"An exception occurred while adding photo (Id: {photo.Id}, slug {photo.Slug}) to the database.", innerException);
    }
}