using Microsoft.Extensions.Caching.Memory;
using ScramBoardAPI.Models;
using ScramBoardAPI.DTO;

namespace ScramBoardAPI.Repositories
{
    public class BoardRepository
    {
        private const string BOARDS_KEY = "boards";
        private readonly IMemoryCache _memoryCache;

        public BoardRepository(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public void AddBoard(Board board)
        {
            List<Board> boards = GetBoards();

            if (boards.FindIndex(x => x.Id == board.Id) == -1 && board.Name != null)
            {
                boards.Add(board);

                SaveBoards(boards);
            }
        }
        
        public void RemoveBoard(int boardId)
        {
            List<Board> boards = GetBoards();

            int index = boards.FindIndex(x => x.Id == boardId);
            if (index == -1)
            {
                throw new ArgumentException("Board not found");
            }

            boards.RemoveAt(index);
            SaveBoards(boards);
        }

        public void SaveBoard(Board board)
        {
            List<Board> boards = GetBoards();

            int index = boards.FindIndex(x => x.Id == board.Id);
            if (index == -1)
            {
                throw new ArgumentException("Board not found");
            }

            boards[index] = board;
            SaveBoards(boards);
        }

        public void SaveBoards(List<Board> boards)
        {
            _memoryCache.Set(BOARDS_KEY, boards);
        }

        public List<Board> GetBoards()
        {
            List<Board> boards = _memoryCache.Get<List<Board>>(BOARDS_KEY);

            if (boards == null)
            {
                boards = new List<Board>();
            }

            return boards;
        }

        public Board? GetBoard(int boardId)
        {
            return GetBoards().Find(x => x.Id == boardId);
        }
    }
}
