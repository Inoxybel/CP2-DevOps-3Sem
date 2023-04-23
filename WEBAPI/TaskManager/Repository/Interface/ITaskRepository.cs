using TaskManager.CrossCutting.DTO;
using Task = TaskManager.Domain.Task;

namespace TaskManager.Repository.Interface
{
    public interface ITaskRepository
    {
        Task<Task?> CreateTask(Task request);
        Task<Task?> UpdateTask(string TaskId, Task request);
        Task<Task?> GetTask(string taskID);
        Task<List<Task>?> GetAllTasks();
        Task<List<Task>?> GetAllNotClosedTasks();
        Task<bool> DeleteTask(string taskID);
    }
}
