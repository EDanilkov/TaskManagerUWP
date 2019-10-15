using SharedServicesModule.Models;
using SharedServicesModule.ResponseModel;
using System.Threading.Tasks;

namespace BusinessLogicModule.Interfaces
{
    public interface IUserProjectRepository
    {
        Task<NewResponseModel> AddUserProject(UserProject userProject);
    }
}
