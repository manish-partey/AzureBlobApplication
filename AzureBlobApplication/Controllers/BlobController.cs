using Azure.Core;
using AzureBlobApplication.Service;
using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.AspNetCore.Mvc;
using System;

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
        [HttpGet]
        public IActionResult AddFile(string containerName)
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddFile(string containerName,IFormFile file)
        {
            if (file == null || file.Length < -1) return View();
            var fileName = Path.GetFileNameWithoutExtension(file.FileName) + "_" + Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var result = await _blobService.UploadBlob(fileName, file, containerName);
            if(result)
            {
                return RedirectToAction("Index", "Container");
            }
            return View();
        }

    }
}
