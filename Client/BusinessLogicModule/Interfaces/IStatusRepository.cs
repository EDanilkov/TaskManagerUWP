using SharedServicesModule.Models;
using SharedServicesModule.ResponseModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogicModule.Interfaces
{
    public interface IStatusRepository
    {
        /// <summary>
        /// Saves info about provided status to the database.
        /// </summary>
        /// <param name="status">The object to be added to the Status.</param>
        /// <returns>Retrieves response to the query.</returns>
        Task<NewResponseModel> AddStatus(Status status);

        /// <summary>
        /// Returns status with provided name or id.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="id"></param>
        /// <returns>A status that match the conditions defined by the specified parameters if found; 
        /// otherwise, an empty.
        /// </returns>
        Task<Status> GetStatus(string name = null, int? id = 0);
        /// <summary>
        /// Returns all statuses.
        /// </summary>
        /// <returns>A  List&lt;Status>; 
        /// otherwise, an empty  List&lt;Status>.
        /// </returns>
        Task<List<Status>> GetStatuses();
    }
}
