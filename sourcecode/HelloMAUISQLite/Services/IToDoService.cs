using HelloMAUI.Model;

namespace HelloMAUI.Services
{
    public interface IToDoService
    {
        Task Init();
        Task AddToDo(string todoItem);        
        Task<IEnumerable<ToDoItem>> GetToDoItems();
    }
}