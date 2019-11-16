using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServerAPI.Data;
using ServerAPI.Data.ResponseModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServerAPI.Controllers
{
    [Authorize]
    [Route("api/tasks")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private IDBRepository _db;

        public TaskController(IDBRepository db)
        {
            _db = db;
        }

        [HttpPost("new")]
        public async Task<IActionResult> AddTask([FromBody]Data.Models.Task task)
        {
            NewResponseModel newTaskResponseModel = new NewResponseModel();
            try
            {
                await _db.AddTask(task);
                newTaskResponseModel.Message = "Success !!!";
                newTaskResponseModel.CreatedId = task.Id;
                return Ok(newTaskResponseModel);
            }
            catch (Exception ex)
            {
                newTaskResponseModel.Message = ex.Message;
                return BadRequest(newTaskResponseModel);
            }
        }
        
        [HttpPut]
        public async Task<IActionResult> ChangeTask([FromBody]UpdateTaskModel updateTaskModel)
        {
            NewResponseModel newTaskResponseModel = new NewResponseModel();
            try
            {
                Data.Models.Task task = await _db.GetTask(updateTaskModel.Task.Id); //Data.Task.FirstAsync(c => c.Id == updateTaskModel.Task.Id);
                task.Name = updateTaskModel.TaskName;
                task.Description = updateTaskModel.TaskDescription;
                task.UserId = updateTaskModel.UserId;
                task.StatusId = updateTaskModel.StatusId;
                task.EndDate = updateTaskModel.TaskFinishDate;


                /*newTaskResponseModel =*/ await _db.ChangeTask(task);
                newTaskResponseModel.CreatedId = task.Id;
                newTaskResponseModel.Message = "Success !!!";
                return Ok(newTaskResponseModel);
            }
            catch (Exception ex)
            {
                newTaskResponseModel.Message = ex.Message;
                return BadRequest(newTaskResponseModel);
            }
        }

        [HttpDelete("{taskId}")]
        public async System.Threading.Tasks.Task DeleteTask(int taskId)
        {
            await _db.DeleteTask(taskId);
        }

        [HttpDelete("{userId}/{projectId}")]
        public async System.Threading.Tasks.Task DeleteTasksByUser(int userId, int projectId)
        {
            await _db.DeleteTasksFromUser(userId, projectId);
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<Data.Models.Task>>> GetTasks()
          => await _db.GetTasks();

        [HttpGet("{taskId}")]
        public async Task<ActionResult<Data.Models.Task>> GetTask(int taskId)
          => await _db.GetTask(taskId);
        
        [HttpGet("projects/{projectId}")]
        public async Task<ActionResult<IEnumerable<Data.Models.Task>>> GetTasksFromProject(int projectId)
            => await _db.GetTasksFromProject(projectId);
        
        [HttpGet("{userId}/{projectId}")]
        public async Task<List<Data.Models.Task>> GetProjectTasksByUser(int userId, int projectId)
            => await _db.GetProjectTasksFromUser(userId, projectId);

    }
}