using Microsoft.AspNetCore.Mvc;
using TodoAPI.DTOs;
using TodoAPI.Services.Interfaces;

namespace TodoAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class TarefasController : ControllerBase
{
    private readonly ITarefaService _tarefaService;

    public TarefasController(ITarefaService tarefaService)
    {
        _tarefaService = tarefaService;
    }

    /// <summary>Lista todas as tarefas</summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<TarefaDTO>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<TarefaDTO>>> GetAll()
    {
        var tarefas = await _tarefaService.GetAllAsync();
        return Ok(tarefas);
    }

    /// <summary>Filtra tarefas por status e/ou data de vencimento</summary>
    [HttpGet("filtro")]
    [ProducesResponseType(typeof(IEnumerable<TarefaDTO>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<TarefaDTO>>> GetByFiltro(
        [FromQuery] TarefaFiltroDTO filtro)
    {
        var tarefas = await _tarefaService.GetByFiltroAsync(filtro);
        return Ok(tarefas);
    }

    /// <summary>Busca tarefa por ID</summary>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(TarefaDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TarefaDTO>> GetById(int id)
    {
        var tarefa = await _tarefaService.GetByIdAsync(id);
        if (tarefa == null) return NotFound("Tarefa não encontrada.");
        return Ok(tarefa);
    }

    /// <summary>Cria uma nova tarefa</summary>
    [HttpPost]
    [ProducesResponseType(typeof(TarefaDTO), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<TarefaDTO>> Create(TarefaDTO dto)
    {
        var tarefa = await _tarefaService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = tarefa.Id }, tarefa);
    }

    /// <summary>Atualiza uma tarefa existente</summary>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, TarefaDTO dto)
    {
        var atualizado = await _tarefaService.UpdateAsync(id, dto);
        if (!atualizado) return NotFound("Tarefa não encontrada.");
        return NoContent();
    }

    /// <summary>Remove uma tarefa</summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var removido = await _tarefaService.DeleteAsync(id);
        if (!removido) return NotFound("Tarefa não encontrada.");
        return NoContent();
    }
}