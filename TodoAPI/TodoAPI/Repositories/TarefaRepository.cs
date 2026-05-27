using Microsoft.EntityFrameworkCore;
using TodoAPI.Data;
using TodoAPI.DTOs;
using TodoAPI.Models;
using TodoAPI.Repositories.Interfaces;

namespace TodoAPI.Repositories;

public class TarefaRepository : ITarefaRepository
{
    private readonly AppDbContext _context;

    public TarefaRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Tarefa>> GetAllAsync()
    {
        return await _context.Tarefas
            .OrderBy(t => t.DataVencimento)
            .ToListAsync();
    }

    public async Task<IEnumerable<Tarefa>> GetByFiltroAsync(TarefaFiltroDTO filtro)
    {
        var query = _context.Tarefas.AsQueryable();

        if (filtro.Status.HasValue)
            query = query.Where(t => t.Status == filtro.Status.Value);

        if (filtro.DataVencimentoInicio.HasValue)
            query = query.Where(t => t.DataVencimento >= filtro.DataVencimentoInicio.Value);

        if (filtro.DataVencimentoFim.HasValue)
            query = query.Where(t => t.DataVencimento <= filtro.DataVencimentoFim.Value);

        return await query.OrderBy(t => t.DataVencimento).ToListAsync();
    }

    public async Task<Tarefa?> GetByIdAsync(int id)
    {
        return await _context.Tarefas.FindAsync(id);
    }

    public async Task<Tarefa> CreateAsync(Tarefa tarefa)
    {
        _context.Tarefas.Add(tarefa);
        await _context.SaveChangesAsync();
        return tarefa;
    }

    public async Task<bool> UpdateAsync(Tarefa tarefa)
    {
        _context.Tarefas.Update(tarefa);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var tarefa = await _context.Tarefas.FindAsync(id);
        if (tarefa == null) return false;

        _context.Tarefas.Remove(tarefa);
        await _context.SaveChangesAsync();
        return true;
    }
}