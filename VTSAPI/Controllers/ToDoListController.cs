using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Threading.Tasks;
using VTSAPI.Repository;

namespace VTSAPI.Controllers
{

    [Route("api/[controller]")]
    [Produces("application/json")]
   
    public class ToDoListController:Controller
    {
       
            private readonly ITodoRepository _repository;

            public ToDoListController(ITodoRepository repository)
            {
                _repository = repository;
            }

            [HttpGet()]
            public async Task <IActionResult> Get()   
            {
                IEnumerable response = null;

                try
                {

                response = await _repository.getTodoList(null);
              
                return Ok(response);
                }
                catch (Exception e)
                {

                    return BadRequest(e.Message);

                }
            }

            [HttpDelete()]
            public async Task<IActionResult> Delete([FromQuery] int userid, int todoListID)
            {
              try
                {

                   await _repository.deleteTodoList(userid, todoListID);
                   return Ok("ok");
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

    }
}
