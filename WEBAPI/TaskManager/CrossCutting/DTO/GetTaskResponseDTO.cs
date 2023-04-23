using TaskManager.CrossCutting.Enums;

namespace TaskManager.CrossCutting.DTO
{
    public record GetTaskResponseDTO
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public State State { get; set; }
        public string? AssignedTo { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ActivedDate { get; set; }
        public DateTime? ResolvedDate { get; set; }
        public DateTime? ClosedDate { get; set; }
    }
}
