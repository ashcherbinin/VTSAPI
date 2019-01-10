using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using static VTSAPI.Models.ToDoAPIModel;

namespace VTSAPI.Repository
{
    public interface ITodoRepository
    {
        Task addTodoList(int userid, string name);
        Task deleteTodoList(int userid, int todoListID);
        Task  <IList> getTodoList(int? userdId);
        Task  <IList> getToDoItems (int? listId);

    }
}
