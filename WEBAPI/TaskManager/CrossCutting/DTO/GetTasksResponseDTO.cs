namespace TaskManager.CrossCutting.DTO
{
    public record GetTasksResponseDTO
    {
        public List<GetTaskResponseDTO> Tarefas { get; set; }
    }
}
