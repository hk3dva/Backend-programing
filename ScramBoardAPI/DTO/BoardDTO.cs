using ScramBoardAPI.Models;

namespace ScramBoardAPI.DTO
{
    public class BoardDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ColumnDTO> Columns { get; set; }
        
        public BoardDTO(Board board)
        {
            Id = board.Id;
            Name = board.Name;
            Columns = board.Columns.Select(c => new ColumnDTO(c)).ToList();
        }
    }
}
