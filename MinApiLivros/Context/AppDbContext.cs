using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MinApiLivros.Entities;

namespace MinApiLivros.Context;

public class AppDbContext : IdentityDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
                                                : base(options) { }

    public DbSet<Livro> Livros { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Livro>().HasData(
            new Livro
            {
                Id = 1,
                Titulo = "Clean Code",
                Autor = "Robert C. Martin"
            },
            new Livro
            {
                Id = 2,
                Titulo = "Clean Architecture",
                Autor = "Robert C. Martin"
            },
            new Livro
            {
                Id = 3,
                Titulo = "Programming Entity Framework Core",
                Autor = "Julia Lerman"
            }
        );
        base.OnModelCreating(modelBuilder);
    }
}
