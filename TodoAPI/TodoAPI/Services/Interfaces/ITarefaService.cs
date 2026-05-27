using TodoAPI.DTOs;

namespace TodoAPI.Services.Interfaces;

public interface ITarefaService
{
    Task<IEnumerable<TarefaDTO>> GetAllAsync();
    Task<IEnumerable<TarefaDTO>> GetByFiltroAsync(TarefaFiltroDTO filtro);
    Task<TarefaDTO?> GetByIdAsync(int id);
    Task<TarefaDTO> CreateAsync(TarefaDTO dto);
    Task<bool> UpdateAsync(int id, TarefaDTO dto);
    Task<bool> DeleteAsync(int id);
}