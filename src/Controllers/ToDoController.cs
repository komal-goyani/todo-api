using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver.Core.Operations;
using ToDoAPI.Models;
using ToDoAPI.Services;

namespace ToDoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        private readonly IToDoService _toDoService;

        public ToDoController(IToDoService toDoService)
        {
            this._toDoService = toDoService;
        }
        // Get: api/ToDo
        [HttpGet]
        public async Task<List<ToDoItem>> Get()
        {
            return await _toDoService.GetAsync();
        }

        // Get: api/ToDo/id
        [HttpGet("{id}")]
        public async Task<ActionResult<ToDoItem>> GetById(string id)
        {
            var toDo = await _toDoService.GetAsync(id);

            if (toDo is null)
            {
                return NotFound($"Todo with Id = {id} not found");
            }

            return Ok(toDo);
        }

        // Post: api/ToDo
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ToDoItem newTodo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await _toDoService.CreateAsync(newTodo);

            return CreatedAtAction(nameof(Get), new { id = newTodo.Id }, newTodo);
        }

        // Put: api/ToDo/id
        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, [FromBody] ToDoItem updatedToDo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var toDo = await _toDoService.GetAsync(id);

            if (toDo is null)
            {
                return NotFound($"Todo with Id = {id} not found");
            }

            updatedToDo.Id = toDo.Id;

            await _toDoService.UpdateAsync(id, updatedToDo);

            return NoContent();
        }

        // DELETE: api/ToDo/id
        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var toDo = await _toDoService.GetAsync(id);

            if (toDo is null)
            {
                return NotFound($"Todo with Id = {id} not found");
            }

            await _toDoService.DeleteAsync(id);

            return NoContent();
        }
    }
}
