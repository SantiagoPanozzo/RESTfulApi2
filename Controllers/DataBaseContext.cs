using Microsoft.EntityFrameworkCore;
using Npgsql;
using TodoApi.Models;

namespace TodoApi.Controllers;

public class DataBaseContext : DbContext {
    public DbSet<TodoItemDTO> TodoItems { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder configBase)  {
        var connectionStringBuilder = new NpgsqlConnectionStringBuilder
        {
            Host = "",      // Host de la base (docker)
            Port = 1,       // Puerto de la base (docker)
            Username = "",  // User de la base
            Password = "",  // Contraseña de la base
            Database = ""   // Nombre de la base
        };
        configBase.UseNpgsql(connectionStringBuilder.ToString());
    }
}
