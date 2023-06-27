using Microsoft.EntityFrameworkCore;
using server.Models.Entities;

namespace server.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<MembersEntity> Members { get; set; }
    public DbSet<TodosEntity> Todos { get; set; }
}