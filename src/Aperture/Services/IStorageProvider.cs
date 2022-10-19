namespace Aperture.Services;

public interface IStorageProvider
{
    Task<Uri> StoreAsync(string container, string name, string contentType, byte[] data);
}