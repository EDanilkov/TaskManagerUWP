using SharedServicesModule.Models;
using SharedServicesModule.ResponseModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogicModule.Interfaces
{
    public interface IRoleRepository
    {
        Task<NewResponseModel> AddRole(Role role);

        Task<Role> GetRoleFromUser(string userName, int projectId);
        Task<List<Role>> GetRoles();
        Task<Role> GetRole(string name = null, int? id = 0);
    }
}
