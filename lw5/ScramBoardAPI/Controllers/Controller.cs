using Microsoft.AspNetCore.Mvc;
using ScramBoardAPI.DTO;
using ScramBoardAPI.Models;
using ScramBoardAPI.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ScramBoardAPI.Controllers
{
    [Route("api/boards")]
    [ApiController]
    public class BoardController : Controller
    {
        private readonly BoardRepository _boardRepository;
        private readonly ColumnRepository _columnRepository;
        private readonly TaskRepository _taskRepository;

        public BoardController(BoardRepository boardRepository, ColumnRepository columnRepository, TaskRepository taskRepository)
        {
            _boardRepository = boardRepository;
            _columnRepository = columnRepository;
            _taskRepository = taskRepository;
        }

        [HttpGet]
        public IActionResult GetBoards()
        {
            IEnumerable<BoardDTO> boards = _boardRepository.GetBoards().Select(board => new BoardDTO(board));

            return Ok(boards);
        }

        [HttpGet("{boardId}")]
        public IActionResult GetBoardByUUID(int boardId)
        {
            Board? board = _boardRepository.GetBoard(boardId);

            if (board == null)
            {
                return NotFound();
            }

            return Ok(new BoardDTO(board));
        }

        [HttpPost]
        public IActionResult CreateBoard(int id, string name)
        {

            _boardRepository.AddBoard(new Board(id,name));

            return Ok();
        }

        [HttpDelete("{boardId}")]
        public IActionResult DeleteBoard(int boardId)
        {
            try
            {
                _boardRepository.RemoveBoard(boardId);
            }
            catch (Exception exception)
            {
                return BadRequest(exception);
            }

            return Ok();
        }

        [HttpGet("{boardId}/columns")]
        public IActionResult GetColumns(int boardId)
        {
            IEnumerable<ColumnDTO> columns;

            try
            {
                columns = _columnRepository.GetColumns(boardId).Select(column => new ColumnDTO(column));
            }
            catch
            {
                return NotFound();
            }

            return Ok(columns);
        }

        [HttpGet("{boardId}/columns/{columnId}")]
        public IActionResult GetColumn(int boardId, int columnId)
        {
            try
            {
                Column? column = _columnRepository.GetColumn(boardId, columnId);
                if (column == null)
                {
                    return NotFound();
                }

                return Ok(new ColumnDTO(column));
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpPost("{boardId}/columns")]
        public IActionResult AddColumn(int boardId, int columnId, string columnName)
        {
            try
            {
                Column newColumn = new Column(columnId,columnName);
                _columnRepository.AddColumn(boardId, newColumn);
            }
            catch
            {
                return NotFound();
            }
            return Ok();
        }

        [HttpPut("{boardId}/columns")]
        public IActionResult UpdateColumn(int boardId, int columnId, string columnName)
        {
            try
            {
                Column? boardColumn = _columnRepository.GetColumn(boardId, columnId);

                if (boardColumn == null)
                {
                    return NotFound();
                }

                boardColumn.Name = columnName;
                _columnRepository.UpdateColumn(boardId, boardColumn);

            }
            catch
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpDelete("{boardId}/columns/{columnId}")]
        public IActionResult DeleteColumn(int boardId, int columnId)
        {
            try
            {
                _columnRepository.RemoveColumn(boardId, columnId);
            }
            catch
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpGet("{boardId}/columns/{columnId}/tasks")]
        public IActionResult GetTasks(int boardId, int columnId)
        {
            try
            {
                IEnumerable<TaskDTO> tasks = _taskRepository.GetTasks(boardId, columnId).Select(task => new TaskDTO(task));

                return Ok(tasks);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpGet("{boardId}/columns/{columnId}/tasks/{taskId}")]
        public IActionResult GetTask(int boardId, int columnId, int taskId)
        {
            try
            {
                CTask? task = _taskRepository.GetTask(boardId, columnId, taskId);

                if (task == null)
                {
                    return NotFound();
                }

                return Ok(new TaskDTO(task));
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpPost("{boardId}/tasks")]
        public IActionResult AddTask(int boardId,int? columnId, int taskId, string taskName, string taskDescription, Priority taskPriority)
        {
            try
            {
                CTask newTask = new CTask(taskId, taskName, taskDescription, taskPriority);

                _taskRepository.AddTask(boardId,columnId,newTask);
            }
            catch
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpPut("{boardId}/columns/{columnId}/tasks")]
        public IActionResult UpdateTask(int boardId, int columnId, int taskId, string taskName, string taskDescription, Priority taskPriority)
        {
            try
            {
                CTask? task1 = _taskRepository.GetTask(boardId, columnId, taskId);

                if (task1 == null)
                {
                    return NotFound();
                }

                task1.Name = taskName;
                task1.Description = taskDescription;
                task1.Priority = taskPriority;

                _taskRepository.UpdateTask(boardId, columnId, task1);
            }
            catch
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpDelete("{boardId}/columns/{columnId}/tasks/{taskId}")]
        public IActionResult DeleteTask(int boardId, int columnId, int taskId)
        {
            try
            {
                _taskRepository.DeleteTask(boardId, columnId, taskId);
            }
            catch
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpPut("{boardId}/tasks")]
        public IActionResult MoveTask(int boardId, int columnFromId, int columnToId, int taskId)
        {
            try
            {
                _taskRepository.MoveTask(boardId, columnFromId, columnToId, taskId);
            }
            catch
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
