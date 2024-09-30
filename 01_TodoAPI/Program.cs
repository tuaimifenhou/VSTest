using Microsoft.EntityFrameworkCore;

namespace _01_TodoAPI
{


    //1. Create TodoItem Class
    //2. add nuget package: Microsoft.EntityFrameworkCore.InMemory  ==> for testing
    // Not for Production Use
    //3. create TodoDb class, inherites DbContext
    //4. add DI in Main
    //5. add MapGet, MapPost
    //6. test with postman
//Post Json data
//    {
//   "name" : "medical card renew",
//   "IsCompleted" :false
//}

public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            //add DI, services
            builder.Services.AddDbContext<TodoDb>(opt=>opt.UseInMemoryDatabase("TodoList"));
            //builder.Services.AddCors(options => {
            //    options.AddPolicy("AllowAll", policy =>
            //    {
            //        policy.AllowAnyOrigin()
            //        .AllowAnyMethod()
            //        .AllowAnyHeader();
            //    });
            //});

            var app = builder.Build();

            app.MapGet("/todoitems", async (TodoDb db) => await db.TodoItems.ToListAsync());
            app.MapPost("/todoitems", async (TodoItem todo, TodoDb db) =>
            {
                db.TodoItems.Add(todo);
                await db.SaveChangesAsync();
                return Results.Created($"/todoitems/{todo.Id}", todo);
            });

            app.Run();
        }
    }
}
