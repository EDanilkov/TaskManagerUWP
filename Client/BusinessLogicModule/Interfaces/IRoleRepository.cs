using SharedServicesModule.Models;
using SharedServicesModule.ResponseModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogicModule.Interfaces
{
    public interface IRoleRepository
    {
        /// <summary>
        /// Saves info about provided role to the database.
        /// </summary>
        /// <param name="role">The object to be added to the Role.</param>
        /// <returns>Retrieves response to the query.</returns>
        Task<NewResponseModel> AddRole(Role role);

        /// <summary>
        /// Returns role with provided userName and projectId.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="projectId"></param>
        /// <returns>A role that match the conditions defined by the specified parameters if found; 
        /// otherwise, an empty.
        /// </returns>
        Task<Role> GetRoleFromUser(string userName, int projectId);
        /// <summary>
        /// Returns all roles.
        /// </summary>
        /// <returns>A  List&lt;Role>; 
        /// otherwise, an empty  List&lt;Role>.
        /// </returns>
        Task<List<Role>> GetRoles();
        /// <summary>
        /// Returns role with provided name or id.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="id"></param>
        /// <returns>A role that match the conditions defined by the specified parameters if found; 
        /// otherwise, an empty.
        /// </returns>
        Task<Role> GetRole(string name = null, int? id = 0);
    }
}
