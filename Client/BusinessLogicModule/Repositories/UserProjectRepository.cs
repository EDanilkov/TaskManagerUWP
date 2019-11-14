using BusinessLogicModule.Services;
using Newtonsoft.Json;
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
                return await RequestService.Post("https://localhost:44393/api/userprojects/new", json);
            }
            catch
            {
                throw;
            }
        }
    }
}
