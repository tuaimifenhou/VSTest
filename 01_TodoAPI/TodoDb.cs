using Microsoft.EntityFrameworkCore;

namespace _01_TodoAPI
{
    public class TodoDb : DbContext
    {
        public TodoDb(DbContextOptions<TodoDb> options):base(options) { }
        public DbSet<TodoItem> TodoItems { get; set; }
    }
}
