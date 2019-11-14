using BusinessLogicModule.Services;
using Newtonsoft.Json;
using SharedServicesModule.Models;
using SharedServicesModule.ResponseModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogicModule.Interfaces
{
    public class RoleRepository : IRoleRepository
    {
        public async Task<NewResponseModel> AddRole(Role role)
        {
            try
            {
                string json = JsonConvert.SerializeObject(role);
                return await RequestService.Post("https://localhost:44393/api/roles/new", json);

            }
            catch
            {
                throw;
            }
        }
        
        public async Task<Role> GetRoleFromUser(string userName, int projectId)
        {
            try
            {
                return await RequestService.Get<Role>("https://localhost:44393/api/roles/" + userName + "/" + projectId.ToString());
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<Role>> GetRoles()
        {
            try
            {
                return await RequestService.Get<List<Role>>("https://localhost:44393/api/roles/all");

            }
            catch
            {
                throw;
            }
        }

        public async Task<Role> GetRole(string name = null, int? id = 0)
        {
            try
            {
                return await RequestService.Get<Role>("https://localhost:44393/api/roles?name=" + name + "&id=" + id);

            }
            catch
            {
                throw;
            }
        }
    }
}
