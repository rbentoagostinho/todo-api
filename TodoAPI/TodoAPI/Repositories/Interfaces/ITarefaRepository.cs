using TodoAPI.DTOs;
using TodoAPI.Models;

namespace TodoAPI.Repositories.Interfaces;

public interface ITarefaRepository
{
    Task<IEnumerable<Tarefa>> GetAllAsync();
    Task<IEnumerable<Tarefa>> GetByFiltroAsync(TarefaFiltroDTO filtro);
    Task<Tarefa?> GetByIdAsync(int id);
    Task<Tarefa> CreateAsync(Tarefa tarefa);
    Task<bool> UpdateAsync(Tarefa tarefa);
    Task<bool> DeleteAsync(int id);
}