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
        private readonly ToDoService toDoService;

        public ToDoController(ToDoService toDoService)
        {
            this.toDoService = toDoService;
        }
        // Get: api/ToDo
        [HttpGet]
        public async Task<List<ToDoItem>> Get()
        {
            return await toDoService.GetAsync();
        }

        // Get: api/ToDo/id
        [HttpGet("{id}")]
        public async Task<ActionResult<ToDoItem>> Get(string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var toDo = await toDoService.GetAsync(id);

            if (toDo is null)
            {
                return NotFound();
            }

            return Ok(toDo);
        }

        // Post: api/ToDo
        [HttpPost]
        public async Task<IActionResult> Post(ToDoItem newTodo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await toDoService.CreateAsync(newTodo);

            return CreatedAtAction(nameof(Get), new { id = newTodo.Id }, newTodo);
        }


        // Put: api/ToDo/id
        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, ToDoItem updatedToDo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != updatedToDo.Id)
            {
                return BadRequest();
            }

            var toDo = await toDoService.GetAsync(id);

            if (toDo is null)
            {
                return NotFound();
            }

            //updatedToDo.Id = toDo.Id; //doesn't make sense

            await toDoService.UpdateAsync(id, updatedToDo);

            return NoContent();
        }

        // DELETE: api/ToDo/id
        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            var toDo = await toDoService.GetAsync(id);

            if (toDo is null)
            {
                return NotFound();
            }

            await toDoService.RemoveAsync(id);

            return NoContent();
        }
    }
}
