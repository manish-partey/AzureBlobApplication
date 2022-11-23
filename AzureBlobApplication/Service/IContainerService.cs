namespace AzureBlobApplication.Service
{
    public interface IContainerService
    {
        Task<List<string>> GetAllContaineraAndBlob();
        Task<List<string>> GetAllContainer();        
        Task CreateContainer(string containername);
        Task DeleteContainer(string containername);
    }
}
