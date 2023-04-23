namespace TaskManager.CrossCutting.DTO
{
    public record CreateTaskRequestDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
