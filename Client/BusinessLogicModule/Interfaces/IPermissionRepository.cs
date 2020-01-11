using SharedServicesModule.Models;
using SharedServicesModule.ResponseModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogicModule.Interfaces
{
    public interface IPermissionRepository
    {
        /// <summary>
        /// Saves info about provided permission to the database.
        /// </summary>
        /// <param name="permission">The object to be added to the Permission.</param>
        /// <returns>Retrieves response to the query.</returns>
        Task<NewResponseModel> AddPermission(Permission permission);

        /// <summary>
        /// Returns all permissions with provided roleId.
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns>A  List&lt;Permission> containing all the elements that match the conditions defined by the specified parameter if found; 
        /// otherwise, an empty  List&lt;Permission>.
        /// </returns>
        Task<List<Permission>> GetPermissionsFromRole(int roleId);

        /// <summary>
        /// Returns permission with provided permissionName.
        /// </summary>
        /// <param name="permissionName"></param>
        /// <returns>A  List&lt;Permission> containing all the elements that match the conditions defined by the specified parameter if found; 
        /// otherwise, an empty  List&lt;Permission>.
        /// </returns>
        Task<Permission> GetPermission(string permissionName);
    }
}
