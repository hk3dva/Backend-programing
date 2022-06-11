using ScramBoardAPI.Models;
using ScramBoardAPI.DTO;

namespace ScramBoardAPI.Repositories
{
    public class TaskRepository
    {
        private readonly ColumnRepository _columnRepository;

        public TaskRepository(ColumnRepository columnRepository)
        {
            _columnRepository = columnRepository;
        }
         
        public List<CTask> GetTasks(int boardId, int columnId)
        {
            Column? column = _columnRepository.GetColumn(boardId, columnId);

            if (column == null)
            {
                throw new ArgumentException("Column not found");
            }

            return column.Tasks;
        }

        public CTask? GetTask(int boardId, int columnId, int taskId)
        {
            List<CTask> tasks = GetTasks(boardId, columnId);

            return tasks.Find(x => x.Id == taskId);
        }

        public void AddTask(int boardId, int? columnId, CTask task)
        {
            BoardRepository boardRepository = _columnRepository.GetBoardRepository();
            Board? board = boardRepository.GetBoard(boardId);

            if (board == null)
            {
                throw new ArgumentException("Board not found");
            }

            if (task.Name != null && task.Description != null)
            {
                int colIndex = -1;
                if (columnId is not null)
                    colIndex = _columnRepository.GetColumns(boardId).FindIndex(c => c.Id == columnId);

                board.AddTask(task, colIndex);
            }

            boardRepository.SaveBoard(board);
        }

        public void UpdateTask(int boardId, int columnId, CTask task)
        {
            BoardRepository boardRepository = _columnRepository.GetBoardRepository();
            Board? board = boardRepository.GetBoard(boardId);

            if (board == null)
            {
                throw new ArgumentException("Board not found");
            }

            List<Column> columns = board.Columns;

            int index = columns.FindIndex(x => x.Id == columnId);
            if (index == -1)
            {
                throw new ArgumentException("Column not found");
            }

            List<CTask> tasks = columns[index].Tasks;

            int taskIndex = tasks.FindIndex(x => x.Id == task.Id);
            if (taskIndex == -1)
            {
                throw new ArgumentException("Task not found");
            }

            tasks[taskIndex] = task;

            columns[index].SetTasks(tasks);
            board.SetColumns(columns);

            boardRepository.SaveBoard(board);
        }

        public void MoveTask(int boardId, int columnFromId, int columnToId, int taskId)
        {
            BoardRepository boardRepository = _columnRepository.GetBoardRepository();
            Board? board = boardRepository.GetBoard(boardId);

            if (board == null)
            {
                throw new ArgumentException("Board not found");
            }

            List<Column> columns = board.Columns;
            int indexFrom = columns.FindIndex(x => x.Id == columnFromId);
            if (indexFrom == -1)
            {
                throw new ArgumentException("Column from not found");
            }

            int indexTo = columns.FindIndex(x => x.Id == columnToId);
            if (indexTo == -1)
            {
                throw new ArgumentException("Column to not found");
            }

            List<CTask> tasks = columns[indexFrom].Tasks;
            int taskIndex = tasks.FindIndex(x => x.Id == taskId);
            if (taskIndex == -1)
            {
                throw new ArgumentException("Task not found");
            }

            CTask task = tasks[taskIndex];
            columns[indexFrom].RemoveTask(taskId);
            columns[indexTo].AddTask(task);

            board.SetColumns(columns);

            boardRepository.SaveBoard(board);
        }

        public void DeleteTask(int boardId, int columnId, int taskId)
        {
            BoardRepository boardRepository = _columnRepository.GetBoardRepository();
            Board? board = boardRepository.GetBoard(boardId);

            if (board == null)
            {
                throw new ArgumentException("Board not found");
            }

            List<Column> columns = this._columnRepository.GetColumns(boardId);
            int index = columns.FindIndex(x => x.Id == columnId);
            if (index == -1)
            {
                throw new ArgumentException("Column not found");
            }

            columns[index].RemoveTask(taskId);
            board.SetColumns(columns);

            boardRepository.SaveBoard(board);
        }
    }
}
