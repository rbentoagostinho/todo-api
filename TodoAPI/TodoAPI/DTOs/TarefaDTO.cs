using System.ComponentModel.DataAnnotations;
using TodoAPI.Models;

namespace TodoAPI.DTOs;

public class TarefaDTO
{
    public int Id { get; set; }

    [Required(ErrorMessage = "O título é obrigatório")]
    [MaxLength(100, ErrorMessage = "O título deve ter no máximo 100 caracteres")]
    public string Titulo { get; set; } = string.Empty;

    [MaxLength(500, ErrorMessage = "A descrição deve ter no máximo 500 caracteres")]
    public string Descricao { get; set; } = string.Empty;

    public StatusTarefa Status { get; set; } = StatusTarefa.Pendente;

    [Required(ErrorMessage = "A data de vencimento é obrigatória")]
    public DateTime DataVencimento { get; set; }
}