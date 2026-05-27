using AutoMapper;
using Moq;
using TodoAPI.DTOs;
using TodoAPI.DTOs.Mappings;
using TodoAPI.Models;
using TodoAPI.Repositories.Interfaces;
using TodoAPI.Services;

namespace TodoAPI.Tests;

public class TarefaServiceTests
{
    private readonly Mock<ITarefaRepository> _repositoryMock;
    private readonly IMapper _mapper;
    private readonly TarefaService _service;

    public TarefaServiceTests()
    {
        _repositoryMock = new Mock<ITarefaRepository>();

        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new MappingProfile());
        });
        _mapper = config.CreateMapper();

        _service = new TarefaService(_repositoryMock.Object, _mapper);
    }

    [Fact]
    public async Task GetAllAsync_DeveRetornarListaDeTarefas()
    {
        // Arrange
        var tarefas = new List<Tarefa>
        {
            new Tarefa { Id = 1, Titulo = "Tarefa 1", Status = StatusTarefa.Pendente, DataVencimento = DateTime.Now.AddDays(1) },
            new Tarefa { Id = 2, Titulo = "Tarefa 2", Status = StatusTarefa.EmAndamento, DataVencimento = DateTime.Now.AddDays(2) }
        };

        _repositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(tarefas);

        // Act
        var resultado = await _service.GetAllAsync();

        // Assert
        Assert.NotNull(resultado);
        Assert.Equal(2, resultado.Count());
    }

    [Fact]
    public async Task GetByIdAsync_DeveRetornarTarefaQuandoExistir()
    {
        // Arrange
        var tarefa = new Tarefa { Id = 1, Titulo = "Tarefa 1", Status = StatusTarefa.Pendente, DataVencimento = DateTime.Now.AddDays(1) };

        _repositoryMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(tarefa);

        // Act
        var resultado = await _service.GetByIdAsync(1);

        // Assert
        Assert.NotNull(resultado);
        Assert.Equal("Tarefa 1", resultado.Titulo);
    }

    [Fact]
    public async Task GetByIdAsync_DeveRetornarNullQuandoNaoExistir()
    {
        // Arrange
        _repositoryMock.Setup(r => r.GetByIdAsync(99)).ReturnsAsync((Tarefa?)null);

        // Act
        var resultado = await _service.GetByIdAsync(99);

        // Assert
        Assert.Null(resultado);
    }

    [Fact]
    public async Task CreateAsync_DeveCriarTarefaComSucesso()
    {
        // Arrange
        var dto = new TarefaDTO
        {
            Titulo = "Nova Tarefa",
            Descricao = "Descrição",
            Status = StatusTarefa.Pendente,
            DataVencimento = DateTime.Now.AddDays(5)
        };

        var tarefa = new Tarefa
        {
            Id = 1,
            Titulo = dto.Titulo,
            Descricao = dto.Descricao,
            Status = dto.Status,
            DataVencimento = dto.DataVencimento
        };

        _repositoryMock.Setup(r => r.CreateAsync(It.IsAny<Tarefa>())).ReturnsAsync(tarefa);

        // Act
        var resultado = await _service.CreateAsync(dto);

        // Assert
        Assert.NotNull(resultado);
        Assert.Equal("Nova Tarefa", resultado.Titulo);
        Assert.Equal(StatusTarefa.Pendente, resultado.Status);
    }

    [Fact]
    public async Task UpdateAsync_DeveRetornarFalseQuandoTarefaNaoExistir()
    {
        // Arrange
        _repositoryMock.Setup(r => r.GetByIdAsync(99)).ReturnsAsync((Tarefa?)null);

        var dto = new TarefaDTO { Titulo = "Tarefa", DataVencimento = DateTime.Now.AddDays(1) };

        // Act
        var resultado = await _service.UpdateAsync(99, dto);

        // Assert
        Assert.False(resultado);
    }

    [Fact]
    public async Task UpdateAsync_DeveAtualizarTarefaComSucesso()
    {
        // Arrange
        var tarefa = new Tarefa { Id = 1, Titulo = "Tarefa Antiga", Status = StatusTarefa.Pendente, DataVencimento = DateTime.Now.AddDays(1) };

        var dto = new TarefaDTO
        {
            Titulo = "Tarefa Atualizada",
            Status = StatusTarefa.EmAndamento,
            DataVencimento = DateTime.Now.AddDays(3)
        };

        _repositoryMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(tarefa);
        _repositoryMock.Setup(r => r.UpdateAsync(It.IsAny<Tarefa>())).ReturnsAsync(true);

        // Act
        var resultado = await _service.UpdateAsync(1, dto);

        // Assert
        Assert.True(resultado);
    }

    [Fact]
    public async Task DeleteAsync_DeveRetornarTrueQuandoExistir()
    {
        // Arrange
        _repositoryMock.Setup(r => r.DeleteAsync(1)).ReturnsAsync(true);

        // Act
        var resultado = await _service.DeleteAsync(1);

        // Assert
        Assert.True(resultado);
    }

    [Fact]
    public async Task DeleteAsync_DeveRetornarFalseQuandoNaoExistir()
    {
        // Arrange
        _repositoryMock.Setup(r => r.DeleteAsync(99)).ReturnsAsync(false);

        // Act
        var resultado = await _service.DeleteAsync(99);

        // Assert
        Assert.False(resultado);
    }
}