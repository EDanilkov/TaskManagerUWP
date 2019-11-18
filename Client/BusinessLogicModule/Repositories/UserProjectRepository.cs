using BusinessLogicModule.Services;
using Newtonsoft.Json;
using SharedServicesModule;
using SharedServicesModule.Models;
using SharedServicesModule.ResponseModel;
using System.Threading.Tasks;

namespace BusinessLogicModule.Interfaces
{
    public class UserProjectRepository : IUserProjectRepository
    {
        public async Task<NewResponseModel> AddUserProject(UserProject userProject)
        {
            try
            {
                string json = JsonConvert.SerializeObject(userProject);
                return await RequestService.Post(Consts.BaseAddress + "api/userprojects/new", json);
            }
            catch
            {
                throw;
            }
        }
    }
}
