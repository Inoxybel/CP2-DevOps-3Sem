using AutoMapper;
using TaskManager.CrossCutting.DTO;
using Task = TaskManager.Domain.Task;

namespace TaskManager.CrossCutting.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<Task, CreateTaskResponseDTO>();
            CreateMap<Task, GetTaskResponseDTO>();
        }
    }
}
