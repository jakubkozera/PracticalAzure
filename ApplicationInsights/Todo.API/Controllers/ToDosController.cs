using Azure.Storage.Queues;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Todo.API.Controllers
{
    public record ToDo(Guid Id, string Description, bool IsFinished);
    public record ToDoDto(string Description);

    [ApiController]
    [Route("api/todos")]
    public class ToDosController : ControllerBase
    {
        private readonly ILogger<ToDosController> _logger;

        public ToDosController(ILogger<ToDosController> logger)
        {
            _logger = logger;
        }

        private static List<ToDo> _todos = new()
        {
            new(Guid.NewGuid(), "Learn application insights", false)
        };

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ToDo>>> Get()
        {
            _logger.LogInformation("Getting todos");

            var httpClient = new HttpClient();
            await httpClient.GetAsync("https://httpbin.org/status/200");

            return Ok(_todos);
        }

        [HttpPost]
        public ActionResult<IEnumerable<ToDo>> Create(ToDoDto dto)
        {
            _logger.LogInformation("Creating new todo: {0}", dto.Description);

            var newTodo = new ToDo(Guid.NewGuid(), dto.Description, false);

            var queueName = "todos";
            var connectionString = "DefaultEndpointsProtocol=https;AccountName=appinsigtssample1;AccountKey=uPTaEHX/qC7gwtqH2pqQiLDbBJysJsH423HWbNGkKdoHvmK4nKmYKayGoJPUKKtsDpAOKqK1iiY++AStG4PLRw==;EndpointSuffix=core.windows.net";
            QueueClient queueClient = new QueueClient(connectionString, queueName);

            queueClient.CreateIfNotExists();

            queueClient.SendMessage(JsonSerializer.Serialize(newTodo));

            _todos.Add(newTodo);
            return Ok(_todos);
        }
    }
}