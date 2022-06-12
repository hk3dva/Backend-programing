using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using ScramBoardAPI.Models;
using ScramBoardAPI.DTO;

namespace ScramBoardAPI.Repositories
{
    public class BoardRepository
    {
        private const string BOARDS_KEY = "boards";
        private readonly ScarmBoardDbContext _dbContext;

        public BoardRepository(ScarmBoardDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddBoard(Board board)
        {
            DbSet<Board> boards = _dbContext.Boards;

            if (boards.ToList().FindIndex(x => x.Id == board.Id) == -1 && board.Name != null)
            {
                boards.Add(board);
                Save();
            }
        }

        public void RemoveBoard(int boardId)
        {
            DbSet<Board> boards = _dbContext.Boards;

            Board board = boards.ToList().Find(x => x.Id == boardId);
            if (board is null)
            {
                throw new ArgumentException("Board not found");
            }

            boards.Remove(board);
            Save();
        }

        // public void SaveBoard(Board board)
        // {
        //     DbSet<Board> boards = GetBoards();
        //
        //     int index = boards.FindIndex(x => x.Id == board.Id);
        //     if (index == -1)
        //     {
        //         throw new ArgumentException("Board not found");
        //     }
        //
        //     boards[index] = board;
        //     Save(boards);
        // }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public List<Board> GetBoards()
        {
            DbSet<Column> columns = _dbContext.Columns;
            List<Board> boards = _dbContext.Boards.ToList();
            List<CTask> tasks = _dbContext.Tasks.ToList();

            // ОЧЕНЬ СТРАННЫЙ И НЕПОНЯТНЫЙ БАГ:
            // без этого пустого цикла и без ToList() в API у board'ов не выводит список колонок,
            // абсолютно не понимаю почему
            // если посмотреть на dbcontext.boards то там у любой доски columns = null,
            //     но если пройтись циклом и вычленить любой b из boards то по какому-то волшебству у него появляется
            //     columns, который составлен из колонок с айди из связи в бд, БРЕД!

            foreach (var col in columns)
            {
                foreach (var b in boards)
                {
                    foreach (var t in tasks)
                    {

                    }

                    // if (col.BoardId == b.Id)
                    //     b.Columns.Add(col);
                }
            }

            return boards;
        }

        public Board? GetBoard(int boardId)
        {
            return GetBoards().ToList().Find(x => x.Id == boardId);
        }
    }
}
