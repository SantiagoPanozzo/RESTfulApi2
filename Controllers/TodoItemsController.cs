using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly TodoContext _context;
        private readonly DataBaseContext _dbContext;

        public TodoItemsController(TodoContext context, DataBaseContext dbContext)
        {
            _context = context;
            _dbContext = dbContext;
        }

        // GET: api/TodoItems
        [HttpGet]
        public ActionResult<IEnumerable<TodoItemDTO>> GetTodoItems()
        {
            return (!_dbContext.TodoItems.ToList().IsNullOrEmpty()) ? _dbContext.TodoItems.ToList() : NoContent();
        }

        // GET: api/TodoItems/5
        [HttpGet("{id}")]
        public ActionResult<TodoItemDTO> GetTodoItem(long id)
        {
            var todoItemDto = _dbContext.TodoItems.Find(id);
            return (todoItemDto == null) ? NotFound() : todoItemDto;
        }
        
        // PUT: api/TodoItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodoItem(long id, TodoItemDTO todoItemDTO)
        {
            if (id != todoItemDTO.Id)
            {
                return Conflict();
            }

            _dbContext.Entry(todoItemDTO).State = EntityState.Modified;
            _dbContext.SaveChanges();

            return NoContent();
        }
        
        // POST: api/TodoItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TodoItemDTO>> CreateTodoItem(TodoItemDTO todoItemDTO)
        {
            _dbContext.TodoItems.Add(todoItemDTO);
            _dbContext.SaveChanges();

            return CreatedAtAction(nameof(GetTodoItem), new { id = todoItemDTO.Id }, todoItemDTO);
        }

        // DELETE: api/TodoItems/5
        [HttpDelete("{id}")]
        public ActionResult DeleteTodoItem(long id)
        {
            var todoItem = _dbContext.TodoItems.Find(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            _dbContext.TodoItems.Remove(todoItem);
            _dbContext.SaveChangesAsync();

            return NoContent();
        }

        private bool TodoItemExists(long id)
        {
            return _context.TodoItems.Any(e => e.Id == id);
        }

        private static TodoItemDTO ItemToDTO(TodoItem todoItem) =>
            new TodoItemDTO
            {
                Id = todoItem.Id,
                Name = todoItem.Name,
                IsComplete = todoItem.IsComplete
            };
    }
}