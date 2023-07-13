using Microsoft.EntityFrameworkCore;
using Npgsql;
using TodoApi.Models;

namespace TodoApi.Controllers;

public class DataBaseContext : DbContext {
    public DbSet<TodoItemDTO> TodoItems { get; set; }
    
    public DataBaseContext(DbContextOptions<DataBaseContext> options)
        : base(options)
    {
    }
}
