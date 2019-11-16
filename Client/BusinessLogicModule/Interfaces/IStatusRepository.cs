using SharedServicesModule.Models;
using SharedServicesModule.ResponseModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogicModule.Interfaces
{
    public interface IStatusRepository
    {
        Task<NewResponseModel> AddStatus(Status status);
        Task<Status> GetStatus(string name = null, int? id = 0);
        Task<List<Status>> GetStatuses();
    }
}
