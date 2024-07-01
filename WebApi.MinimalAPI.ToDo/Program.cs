using WebApi.MinimalAPI.ToDo.EndPoints;
using WebApi.MinimalAPI.ToDo.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//-------------------------------------------------------------

builder.Services.AddScoped<ITodoItemService, TodoItemService>();

//-------------------------------------------------------------




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//-------------------------------------------------------------

app.MapTodoItemsEndpoints(); //using WebApi.MinimalAPI.ToDo.EndPoints;

//-------------------------------------------------------------


app.Run();
