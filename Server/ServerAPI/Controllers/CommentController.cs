using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServerAPI.Data;
using ServerAPI.Data.Models;
using ServerAPI.Data.ResponseModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServerAPI.Controllers
{
    [Authorize]
    [Route("api/comments")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private IDBRepository _db;

        public CommentController(IDBRepository db)
        {
            _db = db;
        }

        [HttpPost("new")]
        public async Task<IActionResult> AddComment([FromBody]Comment comment)
        {

            NewResponseModel newRolePermissionResponseModel = new NewResponseModel();
            try
            {
                await _db.AddComment(comment);
                newRolePermissionResponseModel.Message = "Success !!!";
                return Ok(newRolePermissionResponseModel);
            }
            catch (Exception ex)
            {
                newRolePermissionResponseModel.Message = ex.Message;
                return BadRequest(newRolePermissionResponseModel);
            }

        }

        [HttpGet("{taskId}")]
        public async Task<ActionResult<IEnumerable<Comment>>> GetCommentByTaskId(int taskId)
          => await _db.GetCommentByTaskId(taskId);
    }
}