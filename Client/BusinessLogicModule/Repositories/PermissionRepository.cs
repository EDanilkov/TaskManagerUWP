using BusinessLogicModule.Interfaces;
using BusinessLogicModule.Services;
using Newtonsoft.Json;
using SharedServicesModule;
using SharedServicesModule.Models;
using SharedServicesModule.ResponseModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogicModule.Repositories
{
    public class PermissionRepository : IPermissionRepository
    {
        public async Task<NewResponseModel> AddPermission(Permission permission)
        {
            try
            {
                string json = JsonConvert.SerializeObject(permission);
                return await RequestService.Post(Consts.BaseAddress + "api/permissions/new", json);
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
                return await RequestService.Get<Permission>(Consts.BaseAddress + "api/permissions/" + permissionName);

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
                return await RequestService.Get<List<Permission>>(Consts.BaseAddress + "api/permissions/roles/" + roleId.ToString());

            }
            catch
            {
                throw;
            }
        }
    }
}
