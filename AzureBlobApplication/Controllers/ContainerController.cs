using AzureBlobApplication.Service;
using Microsoft.AspNetCore.Mvc;

namespace AzureBlobApplication.Controllers
{
    public class ContainerController : Controller
    {
        private readonly IContainerService _containerService;
        public ContainerController(IContainerService containerService)
        {
            _containerService = containerService;
        }

        public async Task<IActionResult> Index()
        {
            var containerName = await _containerService.GetAllContainer();
            return View(containerName);
        }
    }
}
