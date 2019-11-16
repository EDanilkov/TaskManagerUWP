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
    [Route("api/statuses")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        private IDBRepository _db;

        public StatusController(IDBRepository db)
        {
            _db = db;
        }

        [HttpPost("new")]
        public async Task<IActionResult> AddStatus([FromBody]Status status)
        {

            NewResponseModel newRolePermissionResponseModel = new NewResponseModel();
            try
            {
                await _db.AddStatus(status);
                newRolePermissionResponseModel.Message = "Success !!!";
                return Ok(newRolePermissionResponseModel);
            }
            catch (Exception ex)
            {
                newRolePermissionResponseModel.Message = ex.Message;
                return BadRequest(newRolePermissionResponseModel);
            }

        }
        
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<Status>>> GetStatuses()
            => await _db.GetStatuses();
        
        [HttpGet]
        public async Task<ActionResult<Status>> GetStatus(string name, int id)
        {
            if (!string.IsNullOrEmpty(name))
            {
                return await _db.GetStatus(name);
            }
            if (id != 0)
            {
                return await _db.GetStatus(id);
            }
            return null;
        }
    }
}