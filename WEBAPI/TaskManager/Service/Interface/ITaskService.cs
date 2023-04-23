using Microsoft.AspNetCore.Mvc;
using TaskManager.CrossCutting.DTO;

namespace TaskManager.Service.Interface
{
    public interface ITaskService
    {
        Task<CreateTaskResponseDTO> CreateTask(CreateTaskRequestDTO createTaskRequestDTO);
        Task<GetTaskResponseDTO> UpdateTask(string taskID, UpdateTaskRequestDTO updateTaskRequestDTO);
        Task<GetTaskResponseDTO> GetTask(string taskId);
        Task<GetTasksResponseDTO> GetAllNotClosedTasks();
        Task<GetTasksResponseDTO> GetTasks();
        Task Delete(string taskId);
    }
}
