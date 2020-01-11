using SharedServicesModule.Models;
using SharedServicesModule.ResponseModel;
using System.Threading.Tasks;

namespace BusinessLogicModule.Interfaces
{
    public interface IRolePermissionRepository
    {
        /// <summary>
        /// Saves info about provided rolePermission to the database.
        /// </summary>
        /// <param name="rolePermission">The object to be added to the RolePermission.</param>
        /// <returns>Retrieves response to the query.</returns>
        Task<NewResponseModel> AddRolePermission(RolePermission rolePermission);
    }
}
