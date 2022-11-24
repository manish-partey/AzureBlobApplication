using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace AzureBlobApplication.Service
{
    public class ContainerService : IContainerService
    {
        private readonly BlobServiceClient _blobServiceClient;

        public ContainerService(BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;
        }
        public async Task CreateContainer(string containername)
        {
            BlobContainerClient blobContainerClient = _blobServiceClient.GetBlobContainerClient(containername);
            await blobContainerClient.CreateIfNotExistsAsync(PublicAccessType.BlobContainer);
        }

        public async Task DeleteContainer(string containername)
        {
            BlobContainerClient blobContainerClient = _blobServiceClient.GetBlobContainerClient(containername);
            await blobContainerClient.DeleteIfExistsAsync();
        }

        public async Task<List<string>> GetAllContainer()
        {
            List<string> containerNames = new();

            await foreach (BlobContainerItem blobContainerItem in _blobServiceClient.GetBlobContainersAsync())
            {
                containerNames.Add(blobContainerItem.Name);
            }
            return containerNames;
        }

        public async Task<List<string>> GetAllContaineraAndBlob()
        {
            List<string> containerAndBlobNames = new();
            containerAndBlobNames.Add("Account Name : " + _blobServiceClient.AccountName);
            containerAndBlobNames.Add("===================================================");
            await foreach (BlobContainerItem blobContainerItem in _blobServiceClient.GetBlobContainersAsync())
            {
                containerAndBlobNames.Add("===" + blobContainerItem.Name);
                BlobContainerClient blobContainerClient = _blobServiceClient.GetBlobContainerClient(blobContainerItem.Name);
                await foreach (BlobItem blobItem in blobContainerClient.GetBlobsAsync())
                {
                    containerAndBlobNames.Add("======" + blobItem.Name);
                }
                containerAndBlobNames.Add("===================================================");
            }
            return containerAndBlobNames;
        }
    }
}