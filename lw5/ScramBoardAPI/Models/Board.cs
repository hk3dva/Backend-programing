using System.ComponentModel.DataAnnotations;

namespace ScramBoardAPI.Models
{
    public class Board
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Column> Columns { get; set; }

        public Board(int id, string name)
        {
            Id = id;
            Name = name;
            Columns = new List<Column>();
        }

        public Column? GetColumn(int columnId)
        {
            return Columns.Find(x => x.Id == columnId);
        }


        public void AddColumn(Column column)
        {
            if (GetColumn(column.Id) == null && Columns.Count < 10)
            {
                column.BoardId = this.Id;
                Columns.Add(column);
            }
        }

        public void SetColumns(List<Column> columns)
        {
            Columns = new List<Column>(columns);
        }

        public void RemoveColumn(int columnId)
        {
            int index = Columns.FindIndex(x => x.Id == columnId);
            Columns.RemoveAt(index);
        }

        public void AddTask(CTask task, int columnIndex = -1)
        {
            if (columnIndex == -1)
                Columns[0].AddTask(task);
            else
                Columns[columnIndex].AddTask(task);
        }

        public void MoveTask(Column columnFrom, Column columnTo, CTask task)
        {
            columnFrom.RemoveTask(task.Id);
            columnTo.AddTask(task);
        }
    }
}
