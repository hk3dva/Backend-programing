using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrumBoard
{
    public class Task
    {
        public enum TaskPriority
        {
            Low,
            Middle,
            High
        }
        public string Title { get; set; }
        public string Description { get; set; }
        public TaskPriority Priority { get; set; }
        public Task(string title)
        {
            Title = title;
            Description = "";
            Priority = TaskPriority.Low;
        }
    }
}
