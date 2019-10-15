using SharedServicesModule.Models;
using SharedServicesModule.ResponseModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogicModule.Interfaces
{
    public interface IPermissionRepository
    {
        Task<NewResponseModel> AddPermission(Permission permission);

        Task<Permission> GetPermission(string permissionName);
        Task<List<Permission>> GetPermissionsFromRole(int roleId);
    }
}
