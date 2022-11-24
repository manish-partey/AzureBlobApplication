using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace AzureBlobApplication.Service
{
    public class BlobService : IBlobService
    {
        private readonly BlobServiceClient _blobServiceClient;

        public BlobService(BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;
        }

        public async Task<bool> DeleteBlob(string name, string containername)
        {
            BlobContainerClient blobContainerClient = _blobServiceClient.GetBlobContainerClient(containername);
            var blob = blobContainerClient.GetBlobClient(name);
            return await blob.DeleteIfExistsAsync();
        }

        public async Task<List<string>> GetAllBlob(string containerName)
        {
            BlobContainerClient blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            var blobs = blobContainerClient.GetBlobsAsync();
            var bloblist = new List<string>();
            await foreach(var blob in blobs)
            {
                bloblist.Add(blob.Name);
            }
            return bloblist;
        }

        public async Task<string> GetBlob(string name, string containerName)
        {
            BlobContainerClient blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            var blob = blobContainerClient.GetBlobClient(name);
            return blob.Uri.AbsoluteUri;
        }

        public async Task<bool> UploadBlob(string name, IFormFile formFile, string containername)
        {
            BlobContainerClient blobContainerClient = _blobServiceClient.GetBlobContainerClient(containername);
            var blob = blobContainerClient.GetBlobClient(name);
            var httpHeader = new BlobHttpHeaders()
            { ContentType = formFile.ContentType };
            var result =await blob.UploadAsync(formFile.OpenReadStream(), httpHeader);
            if (result != null)
                return true;
            return false;

        }
    }
}
