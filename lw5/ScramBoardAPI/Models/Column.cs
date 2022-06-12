﻿using System.ComponentModel.DataAnnotations;
using ScramBoardAPI.DTO;

namespace ScramBoardAPI.Models
{
    public class Column
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public List<CTask> Tasks { get; set; }
        public int BoardId { get; set; }

        public Column(int id,string name)
        {
            Id = id;
            Name = name;
            Tasks = new List<CTask>();
        }

        public Column(ColumnDTO column)
        {
            Id = column.Id;
            Name = column.Name;
            Tasks = new List<CTask>(column.Tasks.Select(t => new CTask(t)));
        }

        public CTask? GetTask(int taskId)
        {
            return Tasks.Find(x => x.Id == taskId);
        }


        public void SetTasks(List<CTask> tasks)
        {
            foreach (var t in tasks)
                t.ColumnId = this.Id;

            Tasks = new List<CTask>(tasks);
        }

        public void AddTask(CTask task)
        {
            task.ColumnId = this.Id;
            Tasks.Add(task);

        }

        public void RemoveTask(int taskId)
        {
            int index = Tasks.FindIndex(x => x.Id == taskId);

            Tasks.RemoveAt(index);
        }
    }
}
