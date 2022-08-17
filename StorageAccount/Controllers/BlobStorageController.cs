using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageAccount.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BlobStorageController : ControllerBase
    {
        [HttpPost("upload")]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            var connectionString = "DefaultEndpointsProtocol=https;AccountName=projectname1sane;AccountKey=Z7BDYxJqQgFKvJPpw7NcKPpURQkOraIPpO74njujqaWgVKBrs/qP4eHgp9r4taV1l/xQk00I4pFa+AStyjfaMw==;EndpointSuffix=core.windows.net";
            BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);

            var containerName = "documents";

            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);

            await containerClient.CreateIfNotExistsAsync();

            BlobClient blobClient = containerClient.GetBlobClient(file.FileName);

            var blobHttpHeaders = new BlobHttpHeaders();
            blobHttpHeaders.ContentType = file.ContentType;

            await blobClient.UploadAsync(file.OpenReadStream(), blobHttpHeaders);

            return Ok();
        }

        [HttpGet("download")]
        public async Task<IActionResult> Download([FromQuery] string blobName)
        {
            var connectionString = "DefaultEndpointsProtocol=https;AccountName=projectname1sane;AccountKey=Z7BDYxJqQgFKvJPpw7NcKPpURQkOraIPpO74njujqaWgVKBrs/qP4eHgp9r4taV1l/xQk00I4pFa+AStyjfaMw==;EndpointSuffix=core.windows.net";
            BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);

            var containerName = "documents";

            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);

            await containerClient.CreateIfNotExistsAsync();

            BlobClient blobClient = containerClient.GetBlobClient(blobName);

            var downloadResponse = await blobClient.DownloadContentAsync();
            var content = downloadResponse.Value.Content.ToStream();
            var contentType = blobClient.GetProperties().Value.ContentType;

            return File(content, contentType, fileDownloadName: blobName);
        }
    }
}