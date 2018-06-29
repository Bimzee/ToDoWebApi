using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Todo.Models;

namespace Todo.Controllers
{
    [ApiVersion("1")]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]")]
    //[Route("api/[controller]")]
    //[ApiController]
    public class TodoController : ControllerBase
    {
        private readonly TodoContext _context;

        public TodoController(TodoContext context)
        {
            _context = context;

            if (_context.TodoItems.Count() == 0)
            {
                _context.TodoItems.Add(new TodoItem { Name = "Item1" });
                _context.TodoItems.Add(new TodoItem { Name = "Item2" });
                _context.TodoItems.Add(new TodoItem { Name = "Item3" });
                _context.SaveChanges();
            }
        }

        /*
        //[HttpGet("api/{version:apiVersion}/GetAll")]
        //[ProducesResponseType(typeof(BadRequestResult),400)]
        //[ProducesResponseType(typeof(BadRequestResult), 401)]
        //[ProducesResponseType(typeof(NotFoundResult), 404)]
        //[ProducesResponseType(typeof(StatusCodeResult), 500)]
        [HttpGet]
        public ActionResult<List<TodoItem>> GetAll()
        {
            return _context.TodoItems.ToList();
        }*/

        //[HttpGet("api/{version:apiVersion}/[controller]/filter/{id}")]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(BadRequestResult), 401)]
        [ProducesResponseType(typeof(NotFoundResult), 409)]
        [ProducesResponseType(typeof(StatusCodeResult), 500)]
        [HttpGet]
        public async Task<ActionResult<TodoItem>> GetById(long id)
        {
            var item = _context.TodoItems.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            return item;
        }



        [Route("Name")]
        [ProducesResponseType(typeof(BadRequestResult), 400)]
        [ProducesResponseType(typeof(BadRequestResult), 401)]
        [ProducesResponseType(typeof(NotFoundResult), 405)]
        [ProducesResponseType(typeof(StatusCodeResult), 500)]
        [HttpGet]
        public async Task<ActionResult<TodoItem>> GetByName([FromHeader]string name)
        {
            try
            {
                var item = _context.TodoItems.Where(n=>n.Name==name).FirstOrDefault();
                if (item == null)
                {
                    return NotFound();
                }
                return item;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public IActionResult Student()
        {
            return Ok();
        }

    }
}