using AutoMapper;
using TodoAPI.DTOs;
using TodoAPI.Models;
using TodoAPI.Repositories.Interfaces;
using TodoAPI.Services.Interfaces;

namespace TodoAPI.Services;

public class TarefaService : ITarefaService
{
    private readonly ITarefaRepository _tarefaRepository;
    private readonly IMapper _mapper;

    public TarefaService(ITarefaRepository tarefaRepository, IMapper mapper)
    {
        _tarefaRepository = tarefaRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<TarefaDTO>> GetAllAsync()
    {
        var tarefas = await _tarefaRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<TarefaDTO>>(tarefas);
    }

    public async Task<IEnumerable<TarefaDTO>> GetByFiltroAsync(TarefaFiltroDTO filtro)
    {
        var tarefas = await _tarefaRepository.GetByFiltroAsync(filtro);
        return _mapper.Map<IEnumerable<TarefaDTO>>(tarefas);
    }

    public async Task<TarefaDTO?> GetByIdAsync(int id)
    {
        var tarefa = await _tarefaRepository.GetByIdAsync(id);
        return _mapper.Map<TarefaDTO>(tarefa);
    }

    public async Task<TarefaDTO> CreateAsync(TarefaDTO dto)
    {
        var tarefa = _mapper.Map<Tarefa>(dto);
        var criada = await _tarefaRepository.CreateAsync(tarefa);
        return _mapper.Map<TarefaDTO>(criada);
    }

    public async Task<bool> UpdateAsync(int id, TarefaDTO dto)
    {
        var tarefa = await _tarefaRepository.GetByIdAsync(id);
        if (tarefa == null) return false;

        _mapper.Map(dto, tarefa);
        return await _tarefaRepository.UpdateAsync(tarefa);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _tarefaRepository.DeleteAsync(id);
    }
}