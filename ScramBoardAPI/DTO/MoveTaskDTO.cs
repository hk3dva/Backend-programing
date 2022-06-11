using ScramBoardAPI.Models;

namespace ScramBoardAPI.DTO
{
    public class MoveTaskDTO
    {
        public int Id { get; set; }
        public int ColumnFromId { get; set; }
        public int ColumnToId { get; set; }

    }
}
 