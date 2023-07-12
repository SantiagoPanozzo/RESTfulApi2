using Microsoft.EntityFrameworkCore;
using Npgsql;
using TodoApi.Models;

namespace TodoApi.Controllers;

public class DataBaseContext : DbContext {
    public DbSet<TodoItemDTO> TodoItems { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder configBase)  {
        var connectionStringBuilder = new NpgsqlConnectionStringBuilder
        {
            Host = "localhost",      // Host de la base (docker)
            Port = 5432,       // Puerto de la base (docker)
            Username = "postgres",  // User de la base
            Password = "postgres",  // Contraseña de la base
            Database = "SampleDbDriver"   // Nombre de la base
        };
        configBase.UseNpgsql(connectionStringBuilder.ToString());
    }
    
    public DataBaseContext(DbContextOptions<DataBaseContext> options)
        : base(options)
    {
    }
}
