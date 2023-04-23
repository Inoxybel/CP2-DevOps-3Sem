using MongoDB.Driver;
using Task = TaskManager.Domain.Task;

namespace TaskManager.Repository.Config
{
    public class TaskDbContext
    {
        private readonly IMongoDatabase _mongo;

        public TaskDbContext(IConfiguration configuration)
        {
            var connectionString = Environment.GetEnvironmentVariable("MONGO_CONNECTION_STRING");
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                connectionString = configuration.GetConnectionString("MongoDB");
                Console.WriteLine($"String de Conexão via Environment: {connectionString}");
            }
			if (string.IsNullOrWhiteSpace(connectionString))
			{
				throw new Exception("A string de conexão do MongoDB não foi definida.");
			}
            var client = new MongoClient(connectionString);
            Console.WriteLine(connectionString);
            _mongo = client.GetDatabase("TarefasDB");
        }

        public IMongoCollection<Task> Tarefas => _mongo.GetCollection<Task>("Tarefas");
    }
}
