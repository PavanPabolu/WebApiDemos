using WebApi.MinimalAPI.ToDo.Models.Entities;
using WebApi.MinimalAPI.ToDo.Services;

namespace WebApi.MinimalAPI.ToDo.EndPoints
{
    /// <summary>
    /// In this example, we've defined a static class called TodoItemsEndpoints that contains all the endpoint 
    /// definitions for the Todo Items API. The MapTodoItemsEndpoints method is an extension method that can 
    /// be called on the WebApplication instance to register all the endpoints.
    /// 
    /// Each endpoint is defined as a private static method that takes the necessary parameters and returns 
    /// an IResult object, which is the new way of returning responses in minimal APIs.
    /// 
    /// This approach allows us to keep the Program.cs file clean and focused on the overall application 
    /// setup, while moving the endpoint definitions to a separate class. This can be especially useful 
    /// for larger API projects with many endpoints.
    /// </summary>
    public static class TodoItemsEndpoints
    {
        public static void MapTodoItemsEndpoints(this WebApplication app)
        {
            app.MapGet("/todoitems", GetAllTodoItems);
            app.MapGet("/todoitems/complete", GetCompletedTodoItems);
            app.MapGet("/todoitems/{id}", GetTodoItemById);
            app.MapPost("/todoitems", CreateTodoItem);
            app.MapPut("/todoitems/{id}", UpdateTodoItem);
            app.MapDelete("/todoitems/{id}", DeleteTodoItem);
        }

        //Minimal API action methods

        private static IResult GetAllTodoItems(ITodoItemService todoItemService)
        {
            var items = todoItemService.GetAllTodoItems();
            return Results.Ok(items);
        }

        private static IResult GetCompletedTodoItems(ITodoItemService todoItemService)
        {
            var items = todoItemService.GetCompletedTodoItems();
            return Results.Ok(items);
        }

        private static IResult GetTodoItemById(int id, ITodoItemService todoItemService)
        {
            var item = todoItemService.GetTodoItemById(id);
            if (item == null)
                return Results.NotFound();
            return Results.Ok(item);
        }

        private static IResult CreateTodoItem(TodoItem item, ITodoItemService todoItemService)
        {
            var createdItem = todoItemService.CreateTodoItem(item);
            return Results.Created($"/todoitems/{createdItem.Id}", createdItem);
        }

        private static IResult UpdateTodoItem(int id, TodoItem item, ITodoItemService todoItemService)
        {
            if (!todoItemService.UpdateTodoItem(id, item))
                return Results.NotFound();
            return Results.NoContent();
        }

        private static IResult DeleteTodoItem(int id, ITodoItemService todoItemService)
        {
            if (!todoItemService.DeleteTodoItem(id))
                return Results.NotFound();
            return Results.NoContent();
        }
    }
}
