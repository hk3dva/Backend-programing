using System.ComponentModel.DataAnnotations;
using ScramBoardAPI.DTO;

namespace ScramBoardAPI.Models
{
    public enum Priority
    {
        Low = 0,
        Medium = 1,
        High = 2,
    }

    public class CTask
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Priority Priority { get; set; }
        public int ColumnId { get; set; }

        public CTask(int id, string name, string description, Priority priority)
        {
            Id = id;
            Name = name;
            Description = description;
            Priority = priority;
        }

        public CTask(TaskDTO taskDto)
        {
            Id = taskDto.Id;
            Name = taskDto.Name;
            Description = taskDto.Description;
            Priority = taskDto.Priority;
        }
    }
}
