using AutoMapper;
using TaskManager.CrossCutting.DTO;
using TaskManager.CrossCutting.Enums;
using TaskManager.Repository.Interface;
using TaskManager.Service.Interface;

namespace TaskManager.Service
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<TaskService> _logger;

        public TaskService(ITaskRepository repository, IMapper mapper, ILogger<TaskService> logger)
        {
            _taskRepository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<CreateTaskResponseDTO> CreateTask(CreateTaskRequestDTO createTaskRequestDTO)
        {
            _logger.LogInformation("Criando Tarefa a partir dos dados informados para o repositório");
            var task = new Domain.Task(createTaskRequestDTO.Title, createTaskRequestDTO.Description, CrossCutting.Enums.State.New, null);

            _logger.LogInformation("Solicitando persistencia da tarefa.");
            var result = await _taskRepository.CreateTask(task);

            if (result is null)
                throw new Exception("Erro ao criar tarefa.");

            _logger.LogInformation("Mapeando tarefa para o objeto de transferência.");
            return _mapper.Map<CreateTaskResponseDTO>(result);
        }
        public async Task<GetTaskResponseDTO> UpdateTask(string Id, UpdateTaskRequestDTO updateTaskRequestDTO)
        {
            _logger.LogInformation("Criando objeto de atualização a partir dos dados informados para o repositório");
            var task = new Domain.Task(
                    updateTaskRequestDTO.Title,
                    updateTaskRequestDTO.Description,
                    updateTaskRequestDTO.State,
                    updateTaskRequestDTO.AssignedTo
                );

            switch(updateTaskRequestDTO.State)
            {
                case State.Active:
                    task.SetActivedDate();
                    break;
                case State.Resolved:
                    task.SetResolvedDate();
                    break;
                case State.Closed:
                    task.SetClosedDate();
                    break;
            }

            _logger.LogInformation("Solicitando persistencia da atualização da tarefa.");
            var result = await _taskRepository.UpdateTask(Id, task);

            if (result is null)
                throw new Exception("Erro ao atualizar tarefa.");

            _logger.LogInformation("Mapeando tarefa atualizada para o objeto de transferência");
            return _mapper.Map<GetTaskResponseDTO>(result);
        }

        public async Task<GetTaskResponseDTO> GetTask(string Id)
        {
            _logger.LogInformation("Solicitando recuperação da tarefa para o repositório");
            var task = await _taskRepository.GetTask(Id);

            if(task is null)
                return new GetTaskResponseDTO();

            _logger.LogInformation("Mapeando tarefa recuperada para o objeto de transferência");
            return _mapper.Map<GetTaskResponseDTO>(task);
        }

        public async Task<GetTasksResponseDTO> GetTasks()
        {
            _logger.LogInformation("Solicitando recuperação de todas tarefas para o repositório");
            var tasks = await _taskRepository.GetAllTasks();
            List<GetTaskResponseDTO> tarefas = new();

            if (tasks is not null)
            {
                _logger.LogInformation("Mapeando tarefas para o objeto de transferência");
                tarefas = _mapper.Map<List<Domain.Task>, List<GetTaskResponseDTO>>(tasks);
            }
            
            return new GetTasksResponseDTO()
            {
                Tarefas = tarefas
            };
        }
        
        public async Task<GetTasksResponseDTO> GetAllNotClosedTasks()
        {
            _logger.LogInformation("Solicitando recuperação de todas tarefas não fechadas para o repositório");
            var tasks = await _taskRepository.GetAllNotClosedTasks();
            List<GetTaskResponseDTO> tarefas = new();

            if (tasks is not null)
            {
                _logger.LogInformation("Mapeando tarefas para o objeto de transferência");
                tarefas = _mapper.Map<List<Domain.Task>, List<GetTaskResponseDTO>>(tasks);
            }

            return new GetTasksResponseDTO()
            {
                Tarefas = tarefas
            };
        }

        public async Task Delete(string Id)
        {
            _logger.LogInformation("Solicitando exclusão para o repositório");
            var result = await _taskRepository.DeleteTask(Id);

            if (!result)
                throw new Exception("Erro ao deletar tarefa.");
        }
    }
}
