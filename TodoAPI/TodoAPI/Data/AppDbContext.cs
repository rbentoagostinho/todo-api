using Microsoft.EntityFrameworkCore;
using TodoAPI.Models;

namespace TodoAPI.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Tarefa> Tarefas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Tarefa>().HasKey(t => t.Id);

        modelBuilder.Entity<Tarefa>()
            .Property(t => t.Titulo)
            .HasMaxLength(100)
            .IsRequired();

        modelBuilder.Entity<Tarefa>()
            .Property(t => t.Descricao)
            .HasMaxLength(500);

        modelBuilder.Entity<Tarefa>()
            .Property(t => t.Status)
            .HasConversion<int>();

        // Seed de dados
        modelBuilder.Entity<Tarefa>().HasData(
            new Tarefa { Id = 1, Titulo = "Estudar .NET 8", Descricao = "Estudar os novos recursos do .NET 8", Status = StatusTarefa.EmAndamento, DataVencimento = new DateTime(2026, 6, 1), DataCriacao = new DateTime(2026, 5, 27) },
            new Tarefa { Id = 2, Titulo = "Desenvolver API", Descricao = "Desenvolver a API REST de tarefas", Status = StatusTarefa.Pendente, DataVencimento = new DateTime(2026, 6, 7), DataCriacao = new DateTime(2026, 5, 27) },
            new Tarefa { Id = 3, Titulo = "Escrever testes", Descricao = "Escrever testes unitários", Status = StatusTarefa.Pendente, DataVencimento = new DateTime(2026, 6, 15), DataCriacao = new DateTime(2026, 5, 27) }
        );
    }
}