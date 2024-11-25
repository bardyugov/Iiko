using Aiko.Domain;
using Microsoft.EntityFrameworkCore;

namespace Aiko.Infrastructure.Database;

public class DatabaseContext(DbContextOptions<DatabaseContext> options) : DbContext(options)
{
    public DbSet<Entity> Entities { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Entity>()
            .HasKey(e => e.ClientId);

        modelBuilder.Entity<Entity>()
            .Property(e => e.ClientId)
            .HasColumnName("client_Id")
            .ValueGeneratedNever();

        modelBuilder.Entity<Entity>()
            .Property(e => e.Username)
            .IsRequired()
            .HasColumnName("username");

        modelBuilder.Entity<Entity>()
            .Property(e => e.SystemId)
            .IsRequired()
            .HasColumnName("system_id")
            .ValueGeneratedOnAdd();
    }
}