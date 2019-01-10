using System;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VTSAPI.Models;
using static VTSAPI.Models.ToDoAPIModel;

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
           var userexists = _toDoContext.Users.Where(a => a.ID == userid).First();

           if (userexists == null)
           {
               throw new Exception();
           }
           else
           {

           TodoList newList = new TodoList();
             
               newList.isDefault = false;
               newList.Name = name;
               newList.UserID = userid;
               
               await _toDoContext.TodoLists.AddAsync(newList);
               await _toDoContext.SaveChangesAsync();
           }
        }

        public async Task deleteTodoList(int userid, int todoListID)
        {
            var userexists = _toDoContext.Users.Where(a => a.ID == userid).First();
            var listexists = _toDoContext.TodoLists.Where(a => a.TodoListID == todoListID).First();
           
             if (userexists == null || listexists == null)
            {
                throw new Exception();
            }
            else
            {

                 _toDoContext.Remove(listexists);
                await _toDoContext.SaveChangesAsync();
            }
        }


        public async Task<IList> getToDoItems(int? listId )
        {
            var result = await _toDoContext.TodoItems.ToListAsync();

            return result;
        }



        public async Task <IList> getTodoList(int? userId)
        {

            var result = await _toDoContext.TodoLists.ToListAsync();
                              

       //    var result = (from o in _toDoContext.Users
            //                join t in _toDoContext.TodoItems on s.TodoItemID equals t.TodoItemID

          //                select new { o.FirstMidName, o.LastName, o.todoLists, t.Name, t.isComplete}).AsNoTracking().ToList();

            //_toDoContext.Students
            //                             .Include(s => s.Enrollments)
            //                             .ThenInclude(e => e.Course)
            //                             .AsNoTracking().ToList();


            return result;
        }


    }
}
