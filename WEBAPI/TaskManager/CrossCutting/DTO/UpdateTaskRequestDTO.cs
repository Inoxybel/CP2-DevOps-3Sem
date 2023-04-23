using TaskManager.CrossCutting.Enums;

namespace TaskManager.CrossCutting.DTO
{
    public record UpdateTaskRequestDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public State State { get; set; }
        public string AssignedTo { get; set; }
    }
}
