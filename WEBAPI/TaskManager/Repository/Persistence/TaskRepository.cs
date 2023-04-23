using MongoDB.Driver;
using TaskManager.Repository.Config;
using TaskManager.Repository.Interface;
using Task = TaskManager.Domain.Task;

namespace TaskManager.Repository.Persistence
{
    public class TaskRepository : ITaskRepository
    {
        private readonly IMongoCollection<Task> _repository;
        private readonly ILogger<TaskRepository> _logger;

        public TaskRepository(TaskDbContext dbContext, ILogger<TaskRepository> logger)
        {
            _repository = dbContext.Tarefas;
            _logger = logger;
        }

        public async Task<Task?> CreateTask(Task task)
        {
            try
            {
                _logger.LogInformation("Solicitando persistência para o SGBD");
                await _repository.InsertOneAsync(task);
                return task;
            }
            catch (MongoException ex)
            {
                _logger.LogInformation("Erro durante a solicitação para o SGBD");
                return null;
            }
        }

        public async Task<Task?> UpdateTask(string Id, Task task)
        {
            try
            {
                var filterDefinition = Builders<Task>.Filter.Eq("Id", Id);
                var updateDefinition = Builders<Task>.Update
                    .Set("Title", task.Title)
                    .Set("Description", task.Description)
                    .Set("State", task.State)
                    .Set("AssignedTo", task.AssignedTo)
                    .Set("ActivedDate", task.ActivedDate)
                    .Set("ResolvedDate", task.ResolvedDate)
                    .Set("ClosedDate", task.ClosedDate);

                var option = new FindOneAndUpdateOptions<Task>()
                {
                    ReturnDocument = ReturnDocument.After
                };

                _logger.LogInformation("Solicitando persistência para o SGBD");
                var result = await _repository.FindOneAndUpdateAsync(filterDefinition, updateDefinition, option);
                return result;
            }
            catch (MongoException ex)
            {
                _logger.LogInformation("Erro durante solicitação para o SGBD");
                return null;
            }
        }

        public async Task<Task?> GetTask(string Id)
        {
            try
            {
                var filterDefinition = Builders<Task>.Filter.Eq("Id", Id);
                var cursor = await _repository.FindAsync(filterDefinition);

                _logger.LogInformation("Solicitando recuperação para o SGBD");
                return await cursor.FirstOrDefaultAsync();
            }
            catch (MongoException ex)
            {
                _logger.LogInformation("Erro durante solicitação para o SGBD");
                return null;
            }
        }

        public async Task<List<Task>?> GetAllTasks()
        {
            try
            {                
                List<Task> tarefas;
                var filterDefinition = Builders<Task>.Filter.Empty;

                _logger.LogInformation("Solicitando recuperação para o SGBD");
                var cursor = await _repository.FindAsync(filterDefinition);
                tarefas = await cursor.ToListAsync();

                return tarefas;
            }
            catch (MongoException ex)
            {
                _logger.LogInformation("Erro durante solicitação para o SGBD");
                return null;
            }
        }
        
        public async Task<List<Task>?>  GetAllNotClosedTasks()
        {
            try
            {                
                List<Task> tarefas;
                var filterDefinition = Builders<Task>.Filter.Eq<DateTime?>(x => x.ClosedDate, null);

                _logger.LogInformation("Solicitando recuperação para o SGBD");
                var cursor = await _repository.FindAsync(filterDefinition);
                tarefas = await cursor.ToListAsync();

                return tarefas;
            }
            catch (MongoException ex)
            {
                _logger.LogInformation("Erro durante solicitação para o SGBD");
                return null;
            }
        }

        public async Task<bool> DeleteTask(string Id)
        {
            try
            {
                var filterDefinition = Builders<Task>.Filter.Eq("Id", Id);

                _logger.LogInformation("Solicitando deleção para o SGBD");
                await _repository.DeleteOneAsync(filterDefinition);
                return true;
            }
            catch (MongoException ex)
            {
                _logger.LogInformation("Erro durante solicitação para o SGBD");
                return false;
            }
        }
    }
}
