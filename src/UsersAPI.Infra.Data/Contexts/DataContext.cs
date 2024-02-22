using Microsoft.EntityFrameworkCore;
using UsersAPI.Domain.Models;
using UsersAPI.Infra.Data.Configurations;

namespace UsersAPI.Infra.Data.Contexts;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options)
    : base(options)
    { }

    //adicionando as configurações de modelos de entidade do banco de dados (ORM)
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
    }

    public DbSet<User> Users { get; set; }
}