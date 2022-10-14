using Azure.Messaging.ServiceBus;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ServiceBusProducent.Controllers
{
    public record User(string FirstName, string LastName, string Email);

    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register(User user)
        {
            var connectionString = "Endpoint=sb://project1sbne.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=9MPCij7AP/uy2BPNCv613tnnJgRT3AbYGKwC7bVYHzc=";

            var client = new ServiceBusClient(connectionString);

            var sender = client.CreateSender("user-registered");

            var message = new ServiceBusMessage(JsonSerializer.Serialize(user));

            await sender.SendMessageAsync(message);

            return Ok();
        }
    }
}