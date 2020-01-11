using SharedServicesModule.Models;
using SharedServicesModule.ResponseModel;
using System.Threading.Tasks;

namespace BusinessLogicModule.Interfaces
{
    public interface IUserProjectRepository
    {
        /// <summary>
        /// Saves info about provided userProject to the database.
        /// </summary>
        /// <param name="userProject">The object to be added to the UserProject.</param>
        /// <returns>Retrieves response to the query.</returns>
        Task<NewResponseModel> AddUserProject(UserProject userProject);
    }
}
