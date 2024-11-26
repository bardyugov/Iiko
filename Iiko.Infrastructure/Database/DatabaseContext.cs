using Iiko.Domain;
using Microsoft.EntityFrameworkCore;

namespace Iiko.Infrastructure.Database;

public class DatabaseContext(DbContextOptions<DatabaseContext> options) : DbContext(options)
{
    public DbSet<Client> Clients { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>()
            .HasKey(e => e.ClientId);

        modelBuilder.Entity<Client>()
            .Property(e => e.ClientId)
            .HasColumnName("client_Id")
            .ValueGeneratedNever();

        modelBuilder.Entity<Client>()
            .Property(e => e.Username)
            .IsRequired()
            .HasColumnName("username");

        modelBuilder.Entity<Client>()
            .Property(e => e.SystemId)
            .IsRequired()
            .HasColumnName("system_id")
            .ValueGeneratedOnAdd();
    }
}