using Azure.Storage.Queues;
using Microsoft.AspNetCore.Mvc;
using QueueMessagePublisher.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace QueueMessagePublisher.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QueueStorageController : ControllerBase
    {
        [HttpPost("publish")]
        public async Task<IActionResult> Publish(ReturnDto returnDto)
        {
            var connectionString = "DefaultEndpointsProtocol=https;AccountName=projectname1sane;AccountKey=Z7BDYxJqQgFKvJPpw7NcKPpURQkOraIxcjujqaWgVKBrs/qP4eHgp9r4taV1l/xQk00I4pFa+AStyjfaMw==;EndpointSuffix=core.windows.net";

            var queueName = "returns";

            QueueClient queueClient = new QueueClient(connectionString, queueName);

            await queueClient.CreateIfNotExistsAsync();

            var serializedMessage = JsonSerializer.Serialize(returnDto);

            await queueClient.SendMessageAsync(serializedMessage);

            return Ok();
        }
    }
}