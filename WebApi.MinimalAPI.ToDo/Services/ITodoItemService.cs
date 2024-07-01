using WebApi.MinimalAPI.ToDo.Models.Entities;

namespace WebApi.MinimalAPI.ToDo.Services
{
    public interface ITodoItemService
    {
        IEnumerable<TodoItem> GetAllTodoItems();
        IEnumerable<TodoItem> GetCompletedTodoItems();
        TodoItem? GetTodoItemById(int id);
        TodoItem CreateTodoItem(TodoItem item);
        bool UpdateTodoItem(int id, TodoItem item);
        bool DeleteTodoItem(int id);
    }
}
