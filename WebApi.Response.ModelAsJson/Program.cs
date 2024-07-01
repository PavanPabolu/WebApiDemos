using Microsoft.AspNetCore.Http.HttpResults;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

//--------------------------------------------------
builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.WriteIndented = true;
    options.SerializerOptions.IncludeFields = true;
});
//--------------------------------------------------

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();


app.UseRouting();
app.MapControllers();
app.MapControllerRoute(
    name: "default",
    pattern: "api/{controller}/{action}/{id?}",
    defaults: new { controller = "Person", action = "GetPersons" });





app.MapGet("/test1", () =>
{
    return "test-1 method called.";
});

app.MapGet("/test2", () => "test-2 method called.");

app.MapGet("/test3", () => new { message = "test-3 method called." }); //JSON


app.MapGet("/test4", context =>
{

    return Task.Run(() => context.Response.Redirect("/test-3")); //"/Account/Login"
});

app.MapGet("/test5", (HttpContext context) =>
{
    return context.Response.WriteAsJsonAsync(new { message = "Hello World!" }); //JSON
});

app.MapGet("/test6", async (HttpResponse response) =>
{

    await response.WriteAsync("test-6 method called.");
});

app.MapGet("/test7", async (HttpResponse response) =>
{

    await response.WriteAsJsonAsync("test-7 method called.");
});


app.MapGet("/test8", () => Results.Json(new { Id = 1, Name = "john Smith" },
                                        new JsonSerializerOptions(JsonSerializerDefaults.Web)
                                        {
                                            WriteIndented = true,
                                            PropertyNameCaseInsensitive = true
                                        }));


app.MapGet("/test9", () => Results.Ok(new { message = "Hello World!" })).Produces<string>();

app.MapGet("/test10", () => TypedResults.Ok(new { message = "Hello World!" })); //JSON


app.MapGet("/sample-badrequest-1", () =>
{

    return Results.BadRequest("/sample-badrequest-1 called.");
});

app.MapGet("/sample-badrequest-2", () =>
{

    return Results.BadRequest(new { message = "/sample-badrequest-1 called." });
});

app.MapGet("/GetPersona{id:int}", (int id) =>
{

    return id <= 0 ? Results.BadRequest() : Results.Ok(new { id = id, name = "john" });
});

//stream
app.MapGet("/externalapi", async () =>
{

    var url = "https://jsonplaceholder.typicode.com/todos";
    var client = new HttpClient();
    var stream = await client.GetStreamAsync(url);

    return Results.Stream(stream, "application/json");
});


app.MapGet("/exception", () =>
{
    throw new InvalidOperationException("Sample Exception");
});

//app.MapFallbackToFile("index.html");

app.UseExceptionHandler(exceptionHAndlerapp =>
    exceptionHAndlerapp.Run(async context =>
    {
        await Results.Problem().ExecuteAsync(context);
    })
);




app.Run();

/*
 Reference:
 Responses in Minimal API apps
 https://learn.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis/responses?view=aspnetcore-8.0

 How to handle errors in Minimal API apps 
 https://learn.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis/handle-errors?view=aspnetcore-8.0#exception-handler

*/