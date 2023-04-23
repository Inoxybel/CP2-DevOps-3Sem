using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManager.CrossCutting.DTO;
using TaskManager.Service.Interface;

namespace TaskManager.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/tarefa")]
    [Produces("application/json")]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;
        private readonly ILogger<TaskController> _logger;

        public TaskController(ITaskService taskservice, ILogger<TaskController> logger)
        {
            _taskService = taskservice;
            _logger = logger;
        }

        /// <summary>
        ///     Cria uma nova Tarefa
        /// </summary>
        /// <param name="requestDTO">Tarefa</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<CreateTaskResponseDTO> CreateTask([FromBody] CreateTaskRequestDTO requestDTO)
        {
            _logger.LogInformation("Recebido solicitação de criação de uma tarefa.");
            var result = await _taskService.CreateTask(requestDTO);

            _logger.LogInformation("Retornando resultado da solicitação.");
            return result;
        }

        /// <summary>
        ///     Atualizar uma Tarefa existente
        /// </summary>
        /// <param name="Id">ID da Tarefa</param>
        /// <param name="requestDTO">Tarefa</param>
        /// <returns></returns>
        [HttpPut("{Id}")]
        public async Task<GetTaskResponseDTO?> UpdateTask([FromRoute] string Id, [FromBody] UpdateTaskRequestDTO requestDTO)
        {
            _logger.LogInformation($"Recebido solicitação de atualização de tarefa com id: {Id}");
            var result = await _taskService.UpdateTask(Id, requestDTO);

            _logger.LogInformation("Retornando resultado da solicitação.");
            return result;
        }

        /// <summary>
        ///     Recupera uma tarefa a partir da ID informada
        /// </summary>
        /// <param name="Id">ID da Tarefa</param>
        /// <returns>Retorna uma Tarefa</returns>
        /// <response code="200">Retorna uma Tarefa</response>
        [HttpGet("{Id}")]
        [ProducesResponseType(typeof(GetTaskResponseDTO), StatusCodes.Status200OK)]
        public async Task<GetTaskResponseDTO> GetTask([FromRoute] string Id)
        {
            _logger.LogInformation($"Recebido solicitação para recuperar tarefa de id: {Id}");
            var result = await _taskService.GetTask(Id);

            _logger.LogInformation("Retornando resultado da solicitação");
            return result;
        }

        /// <summary>
        ///     Recupera todas Tarefas existentes no banco
        /// </summary>
        /// <returns>Retorna todas tarefas</returns>
        /// <response code="200">Retorna todas tarefas</response>
        [HttpGet("all")]
        [ProducesResponseType(typeof(GetTasksResponseDTO), StatusCodes.Status200OK)]
        public async Task<GetTasksResponseDTO> GetTasks()
        {
            _logger.LogInformation("Recebido solicitação para recuperar todas tarefas.");
            var result = await _taskService.GetTasks();

            _logger.LogInformation("Retornando resultado da solicitação.");
            return result;
        }

        /// <summary>
        ///     Recupera todas Tarefas com status diferentes de Closed no banco
        /// </summary>
        /// <returns>Retorna todas tarefas</returns>
        /// <response code="200">Retorna todas tarefas</response>
        [HttpGet("all-not-closed")]
        [ProducesResponseType(typeof(GetTasksResponseDTO), StatusCodes.Status200OK)]
        public async Task<GetTasksResponseDTO> GetAllNotClosedTasks()
        {
            _logger.LogInformation("Recebido solicitação para recuperar todas tarefas não finalizadas");
            var result = await _taskService.GetAllNotClosedTasks();

            _logger.LogInformation("Retornando resultado da solicitação.");
            return result;
        }

        /// <summary>
        ///     Deleta uma tarefa a partir do ID informado
        /// </summary>
        /// <param name="Id">ID da tarefa</param>
        /// <returns></returns>
        [HttpDelete("{Id}")]
        public async Task DeleteTask([FromRoute] string Id)
        {
            _logger.LogInformation($"Recebido solicitação para exclusão da tarefa de id: {Id}");
            await _taskService.Delete(Id);
        }
    }
}
