using AzureBlobApplication.Service;
using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.AspNetCore.Mvc;

namespace AzureBlobApplication.Controllers
{
    public class BlobController : Controller
    {
        private readonly IBlobService _blobService;
        public BlobController(IBlobService blobService)
        {
            _blobService = blobService;
        }

        public async Task<IActionResult> Manage(string containerName)
        {
            var blobList = await _blobService.GetAllBlob(containerName);
            return  View(blobList);
        }

        public IActionResult AddFile()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddFile(IFormFile formFile)
        {
            if (formFile == null || formFile.Length < -1) return View();
            var fileName = Path.GetFileNameWithoutExtension(formFile.FileName) + "_" + Guid.NewGuid().ToString() + "_" + Path.GetExtension(formFile.FileName);
            var result = await _blobService.UploadBlob(fileName, formFile, "images");
            if(result)
            {
                return View("Index", "Container");
            }
            return View();
        }

    }
}
