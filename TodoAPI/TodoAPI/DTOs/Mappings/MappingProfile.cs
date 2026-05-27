using AutoMapper;
using TodoAPI.DTOs;
using TodoAPI.Models;

namespace TodoAPI.DTOs.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Tarefa, TarefaDTO>().ReverseMap();
    }
}