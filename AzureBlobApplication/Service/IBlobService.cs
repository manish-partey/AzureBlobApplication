namespace AzureBlobApplication.Service
{
    public interface IBlobService
    {
        Task<string> GetBlob(string name, string containerName);
        Task<List<string>> GetAllBlob(string containerName);
        Task<bool> UploadBlob(string name, IFormFile formFile, string containername);
        Task<bool> DeleteBlob(string name, string containername);
    }
}
