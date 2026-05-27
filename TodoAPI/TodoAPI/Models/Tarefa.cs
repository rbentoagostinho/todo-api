using System.ComponentModel.DataAnnotations;

namespace TodoAPI.Models;

public class Tarefa
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Titulo { get; set; } = string.Empty;

    [MaxLength(500)]
    public string Descricao { get; set; } = string.Empty;

    public StatusTarefa Status { get; set; } = StatusTarefa.Pendente;

    public DateTime DataVencimento { get; set; }

    public DateTime DataCriacao { get; set; } = DateTime.Now;
}

public enum StatusTarefa
{
    Pendente = 0,
    EmAndamento = 1,
    Concluido = 2
}