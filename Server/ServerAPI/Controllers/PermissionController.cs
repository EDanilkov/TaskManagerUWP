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
    [Route("api/permissions")]
    [ApiController]
    public class PermissionController : ControllerBase
    {
        private IDBRepository _db;

        public PermissionController(IDBRepository db)
        {
            _db = db;
        }

        [HttpPost("new")]
        public async Task<IActionResult> AddPermission([FromBody]Permission permission)
        {

            NewResponseModel newPermissionResponseModel = new NewResponseModel();
            try
            {
                await _db.AddPermission(permission);
                newPermissionResponseModel.Message = "Success !!!";
                newPermissionResponseModel.CreatedId = permission.Id;
                return Ok(newPermissionResponseModel);
            }
            catch (Exception ex)
            {
                newPermissionResponseModel.Message = ex.Message;
                return BadRequest(newPermissionResponseModel);
            }

        }
        
        [HttpGet("{permissionName}")]
        public async Task<ActionResult<Permission>> GetPermission(string permissionName)
            => await _db.GetPermission(permissionName);
        
        [HttpGet("roles/{roleId}")]
        public async Task<IEnumerable<Permission>> GetPermissionsByRole(int roleId)
            => await _db.GetPermissionsByRole(roleId);

    }
}