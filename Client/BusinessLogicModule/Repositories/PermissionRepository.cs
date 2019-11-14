using BusinessLogicModule.Services;
using Newtonsoft.Json;
using SharedServicesModule.Models;
using SharedServicesModule.ResponseModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogicModule.Interfaces
{
    public class PermissionRepository : IPermissionRepository
    {
        public async Task<NewResponseModel> AddPermission(Permission permission)
        {
            try
            {
                string json = JsonConvert.SerializeObject(permission);
                return await RequestService.Post("https://localhost:44393/api/permissions/new", json);
            }
            catch
            {
                throw;
            }
        }

        public async Task<Permission> GetPermission(string permissionName)
        {
            try
            {
                return await RequestService.Get<Permission>("https://localhost:44393/api/permissions/" + permissionName);

            }
            catch
            {
                throw;
            }
        }

        public async Task<List<Permission>> GetPermissionsFromRole(int roleId)
        {
            try
            {
                return await RequestService.Get<List<Permission>>("https://localhost:44393/api/permissions/roles/" + roleId.ToString());

            }
            catch
            {
                throw;
            }
        }
    }
}
