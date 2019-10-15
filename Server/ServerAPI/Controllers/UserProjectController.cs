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
    [Route("api/userprojects")]
    [ApiController]
    public class UserProjectController : ControllerBase
    {
        private IDBRepository _db;

        public UserProjectController(IDBRepository db)
        {
            _db = db;
        }
        
        [HttpPost("new")]
        public async Task<IActionResult> AddUserProject([FromBody]UserProject userProject)
        {
            NewUserProjectResponseModel newUserProjectResponseModel = new NewUserProjectResponseModel();
            try
            {
                await _db.AddUserProject(userProject);
                newUserProjectResponseModel.Message = "Success !!!";
                newUserProjectResponseModel.CreatedUserId = userProject.UserId;
                newUserProjectResponseModel.CreatedProjectId = userProject.ProjectId;
                newUserProjectResponseModel.CreatedRoleId = userProject.RoleId;
                return Ok(newUserProjectResponseModel);
            }
            catch (Exception ex)
            {
                newUserProjectResponseModel.Message = ex.Message;
                return BadRequest(newUserProjectResponseModel);
            }
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<UserProject>>> GetUserProject()
          => await _db.GetUserProject();

        [HttpGet("{userName}/{projectId}")]
        public async Task<ActionResult<UserProject>> GetUserProject(string userName, int projectId)
          => await _db.GetUserProject(userName, projectId);

        [HttpGet("projects/{projectId}")]
        public async Task<ActionResult<IEnumerable<UserProject>>> GetUserProjectByProjectId(int projectId)
          => await _db.GetUserProjectByProjectId(projectId);

        [HttpGet("users/{userid}")]
        public async Task<ActionResult<IEnumerable<UserProject>>> GetUserProjectByUserId(int userId)
          => await _db.GetUserProjectByUserId(userId);
    }
}