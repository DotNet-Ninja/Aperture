using Aperture.Configuration;
using Aperture.Exceptions;
using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace Aperture.Services;

public class AzureStorageProvider : IStorageProvider
{
    private readonly StorageSettings _settings;

    public AzureStorageProvider(StorageSettings settings)
    {
        _settings = settings;
    }

    public async Task<Uri> StoreAsync(string container, string name, string contentType, byte[] data)
    {
        try
        {
            var client = new BlobServiceClient(_settings.ConnectionString);
            BlobContainerClient blobContainer = client.GetBlobContainerClient(container.ToLower());
            if (!await (blobContainer.ExistsAsync()))
            {
                await blobContainer.CreateAsync();
                await blobContainer.SetAccessPolicyAsync(PublicAccessType.BlobContainer);
            }
            var blob = blobContainer.GetBlobClient(name);
            var headers = new BlobHttpHeaders
            {
                ContentType = contentType,
                CacheControl = $"max-age={_settings.CacheControlTimeout}"
            };
            await using var stream = new MemoryStream(data);
            await blob.UploadAsync(stream);
            await blob.SetHttpHeadersAsync(headers);
            return blob.Uri;
        }
        catch (RequestFailedException e)
        {
            throw new StorageOperationException($"Failed to store file {name} in {container}.", e);
        }

    }
}