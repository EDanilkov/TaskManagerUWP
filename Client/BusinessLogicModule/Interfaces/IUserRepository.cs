using SharedServicesModule.Models;
using SharedServicesModule.ResponseModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogicModule.Interfaces
{
    public interface IUserRepository
    {
        /// <summary>
        /// Saves info about provided user to the database.
        /// </summary>
        /// <param name="user">The object to be added to the User.</param>
        /// <returns>Retrieves response to the query.</returns>
        Task<NewResponseModel> AddUser(User user);

        /// <summary>
        /// Deletes an user from the Project with provided userId and projectId.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="projectId"></param>
        System.Threading.Tasks.Task DeleteUserFromProject(int userId, int projectId);

        /// <summary>
        /// Returns all users.
        /// </summary>
        /// <returns>A  List&lt;User>; 
        /// otherwise, an empty  List&lt;User>.</returns>
        Task<List<User>> GetUsers();
        /// <summary>
        /// Returns user with provided name or id.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="id"></param>
        /// <returns>A user that match the conditions defined by the specified parameters if found; 
        /// otherwise, an empty.
        /// </returns>
        Task<User> GetUser(string name = null, int? id = 0);
        /// <summary>
        /// Returns users with provided projectId.
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns>
        /// A  List&lt;User> containing all the elements that match the conditions defined by the specified parameter if found; 
        /// otherwise, an empty  List&lt;User>.
        /// </returns>
        Task<List<User>> GetUsersFromProject(int projectId);

    }
}
