using ScramBoardAPI.Models;
using ScramBoardAPI.DTO;

namespace ScramBoardAPI.Repositories
{
    public class ColumnRepository
    {
        private readonly BoardRepository _boardRepository;
        private  readonly ScarmBoardDbContext _dbContext;


        public ColumnRepository(ScarmBoardDbContext dbContext,BoardRepository boardRepository)
        {
            _boardRepository = boardRepository;
            _dbContext = dbContext;
        }

        public BoardRepository GetBoardRepository()
        {
            return _boardRepository;
        }

        public List<Column> GetColumns(int boardId)
        {

            var z = _dbContext.Columns;


            Board? board = _boardRepository.GetBoard(boardId);

            if (board == null)
            {
                throw new ArgumentException("Board not found");
            }

            return board.Columns;
        }

        public Column? GetColumn(int boardId, int? columnId)
        {
            if (columnId is null) return null;
            List<Column> columns = GetColumns(boardId);

            return columns.Find(x => x.Id == columnId);
        }

        public void AddColumn(int boardId, Column column)
        {
            Board? board = _boardRepository.GetBoard(boardId);

            if (board == null)
            {
                throw new ArgumentException("Board not found");
            }

            if (board.Columns.FindIndex(x => x.Id == column.Id) == -1 && column.Name != null)
            {
                board.AddColumn(column);
                _dbContext.Columns.Add(column);
                Save();

            }
        }

        public void UpdateColumn(int boardId, Column column)
        {
            Board? board = _boardRepository.GetBoard(boardId);
            if (board == null)
            {
                throw new ArgumentException("Board not found");
            }

            List<Column> columns = board.Columns;

            int columnIndex = columns.FindIndex(x => x.Id == column.Id);
            if (columnIndex == -1)
            {
                throw new ArgumentException("Column not found");
            }

            columns[columnIndex] = column;

            board.SetColumns(columns);
            Save();

        }

        public void RemoveColumn(int boardId, int columnId)
        {
            Board? board = _boardRepository.GetBoard(boardId);
            if (board == null)
            {
                throw new ArgumentException("Board not found");
            }

            board.RemoveColumn(columnId);
            GetColumns(boardId).Remove(GetColumn(boardId, columnId));
            Save();

        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}
