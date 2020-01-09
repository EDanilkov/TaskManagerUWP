using BusinessLogicModule.Interfaces;
using BusinessLogicModule.Services;
using Newtonsoft.Json;
using SharedServicesModule;
using SharedServicesModule.Models;
using SharedServicesModule.ResponseModel;
using System.Threading.Tasks;

namespace BusinessLogicModule.Repositories
{
    public class RolePermissionRepository : IRolePermissionRepository
    {
        public async Task<NewResponseModel> AddRolePermission(RolePermission rolePermission)
        {
            try
            {

                string json = JsonConvert.SerializeObject(rolePermission);
                return await RequestService.Post(Consts.BaseAddress + "api/role-permissions/new", json);
            }
            catch
            {
                throw;
            }
        }
    }
}
