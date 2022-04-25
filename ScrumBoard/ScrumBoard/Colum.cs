using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrumBoard
{
    public class Colum
    {
        private List<Task> TaskList { get; }
        public string Title { get; set; }

        public Colum(string title)
        {
            Title = title;
            TaskList = new();
        }

        public void AddTask(Task task)
        {
            if (FindTaskByTitle(task.Title) != null)
            {
                throw new Exception("{task.Title} Данное имя задачи уже есть в коллонке");
            }

            TaskList.Add(task);
        }
        public void RemoveTask(string title)
        {
            TaskList.RemoveAll(task => task.Title == title);
        }
        public Task? FindTaskByTitle(string title)
        {
            return TaskList.Find(task => task.Title == title);
        }
        public List<Task> GetAllTasksFromColum()
        {
            return TaskList;
        }
        public Task GetTask(int position)
        {
            try
            {
                return TaskList[position];
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new ArgumentOutOfRangeException("Attempt to get card at out of range position");
            }
        }
        public void DeleteCard(int position)
        {
            try
            {
                TaskList.RemoveAt(position);
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new ArgumentOutOfRangeException("Attempt to delete card at out of range position");
            }
        }
    }
}
