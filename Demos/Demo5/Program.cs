using Demo5;

var builder = WebApplication.CreateBuilder(args);

// business logic
builder.Services.AddSingleton<ITodoService, InMemoryTodoService>();

// swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// swagger
app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/todos", (ITodoService service) => service.GetAll());



app.MapGet("/todos/{id}",
    (int id, ITodoService service) =>
{
    var item = service.GetById(id);
    return item is not null
        ? Results.Ok(item)
        : Results.NotFound();
})
    // produces
    .Produces<ToDoItem>()
    .ProducesProblem(404);

app.MapPut("/todos/{id}", (int id, PutToDoItem data, ITodoService service) => service.Upsert(id, data));

app.Run();

public record ToDoItem(int Id, string Title, DateTime DueDate);
public record PutToDoItem(string Title, DateTime DueDate);
