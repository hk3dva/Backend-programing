using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrumBoard
{
    public class Board
    {
        private const int MAX_COLUMS = 10;
        private List<Colum> ColumsList { get; }

        public string Title { get; }
        public Board(string title)
        {
            Title = title;
            ColumsList = new List<Colum>();
        }
        public void AddNewColumn(string title)
        {
            if (ColumsList.Count == MAX_COLUMS)
            {
                throw new Exception("Columns limit exceeding");
            }
            ColumsList.Add(new Colum(title));
        }
        public List<Colum> GetAllColums()
        {
            return ColumsList;
        }

        public void AddNewCard(string title)
        {
            if (ColumsList.Count == 0)
            {
                throw new Exception("Board has no columns");
            }

            ColumsList[0].AddTask(new Task(title));
        }

        public void MoveCard(int srcColumn, int taskOrder, int destColumn)
        {
            try
            {
                ColumsList[destColumn].AddTask(ColumsList[srcColumn].GetTask(taskOrder));
                ColumsList[srcColumn].DeleteCard(taskOrder);
            }
            catch (ArgumentOutOfRangeException)
            {
                throw;
            }
        }

        public Colum? FindColumnByTitle(string title)
        {
            return ColumsList.Find(column => column.Title == title);
        }

        public Colum GetColumn(int position)
        {
            try
            {
                return ColumsList[position];
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new Exception("Attempt to get column at out of range position");
            }
        }
        public void DeleteColumn(int position)
        {
            try
            {
                ColumsList.RemoveAt(position);
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new Exception("Attempt to delete column at out of range position");
            }
        }
    }
}
