using System;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VTSAPI.Models;
using static VTSAPI.Models.ToDoAPIModel;
using static VTSAPI.Models.UserModel;

namespace VTSAPI.Repository
{
    public class ToDoRepository: ITodoRepository
    {


        private readonly ToDoContext _toDoContext;

        public ToDoRepository(ToDoContext context)
        {
           _toDoContext = context;
        }

        public async Task addTodoList(int userid, string name )
        {
            var userexists = _toDoContext.Users.Where(a => a.Id == userid).First();

            if (userexists == null)
            {
                throw new Exception();
            }
            else
            {
                TodoList newList = new TodoList();
                newList.Name = name;
                newList.User = userexists;
                newList.isDefault = false;

                await _toDoContext.TodoLists.AddAsync(newList);
                await _toDoContext.SaveChangesAsync();
            }
        }


        public async Task <IEnumerable> getTodoList (int? userId)
        {

            var result = await _toDoContext.TodoLists.Select(a=> new { a.User, a.Id, a.Name }).ToListAsync();

            return result; 
        }

      
    }
}
