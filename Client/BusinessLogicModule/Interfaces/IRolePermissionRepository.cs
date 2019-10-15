using SharedServicesModule.Models;
using SharedServicesModule.ResponseModel;
using System.Threading.Tasks;

namespace BusinessLogicModule.Interfaces
{
    public interface IRolePermissionRepository
    {
        Task<NewResponseModel> AddRolePermission(RolePermission rolePermission);
    }
}
