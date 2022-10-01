using Microsoft.AspNetCore.Mvc;

namespace Todo.API.Controllers
{
    public record ToDo(Guid Id, string Description, bool IsFinished);
    public record ToDoDto(string Description);

    [ApiController]
    [Route("api/[controller]")]
    public class ToDosController : ControllerBase
    {
        private static List<ToDo> _todos = new()
        {
            new(Guid.NewGuid(), "Learn application insights", false)
        };

        [HttpGet]
        public ActionResult<IEnumerable<ToDo>> Get()
        {
            return Ok(_todos);
        }

        [HttpPost]
        public ActionResult<IEnumerable<ToDo>> Create(ToDoDto dto)
        {
            var newTodo = new ToDo(Guid.NewGuid(), dto.Description, false);
            _todos.Add(newTodo);
            return Ok(_todos);
        }
    }
}