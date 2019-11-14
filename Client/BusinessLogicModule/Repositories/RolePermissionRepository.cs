using BusinessLogicModule.Services;
using Newtonsoft.Json;
using SharedServicesModule.Models;
using SharedServicesModule.ResponseModel;
using System.Threading.Tasks;

namespace BusinessLogicModule.Interfaces
{
    public class RolePermissionRepository : IRolePermissionRepository
    {
        public async Task<NewResponseModel> AddRolePermission(RolePermission rolePermission)
        {
            try
            {

                string json = JsonConvert.SerializeObject(rolePermission);
                return await RequestService.Post("https://localhost:44393/api/role-permissions/new", json);
            }
            catch
            {
                throw;
            }
        }
    }
}
