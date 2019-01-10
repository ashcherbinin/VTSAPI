using System;
using System.Collections;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VTSAPI.Repository;

namespace VTSAPI.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]

    public class ToDoItemsController: Controller
    {

        private readonly ITodoRepository _repository;
       
        public ToDoItemsController(ITodoRepository repository)
        {
           _repository = repository;

        }

        [HttpGet()]
        public async Task<IActionResult> Get()
        {
            IEnumerable response = null;

            try
            {
                response = await _repository.getToDoItems(null);

                return Ok(response);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);

            }
        }



        [HttpPut()]
        public async Task<IActionResult> Put([FromQuery] int userid, string name)
        {
            try
            {
                await _repository.addTodoList(userid, name);
                return Ok("Ok");

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }




        [HttpDelete()]
        public async Task<IActionResult> Delete([FromQuery] int itemId)
        {
            try
            {
                await _repository.deleteTodoItems(itemId);
                return Ok("Ok");

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }

}
