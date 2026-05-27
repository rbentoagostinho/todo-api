using TodoAPI.Models;

namespace TodoAPI.DTOs;

public class TarefaFiltroDTO
{
    public StatusTarefa? Status { get; set; }
    public DateTime? DataVencimentoInicio { get; set; }
    public DateTime? DataVencimentoFim { get; set; }
}