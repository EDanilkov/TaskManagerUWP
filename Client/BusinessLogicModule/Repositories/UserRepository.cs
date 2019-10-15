using BusinessLogicModule.Services;
using Newtonsoft.Json;
using SharedServicesModule.Models;
using SharedServicesModule.ResponseModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogicModule.Interfaces
{
    public class UserRepository : IUserRepository
    {
        public async Task<NewResponseModel> AddUser(User user)
        {
            try
            {
                string json = JsonConvert.SerializeObject(user);
                return await RequestService.Post("https://localhost:44316/api/users/new", json);
            }
            catch
            {
                throw;
            }
        }
        
        public async System.Threading.Tasks.Task DeleteUserFromProject(int userId, int projectId)
        {
            try
            {
                await RequestService.Delete("https://localhost:44316/api/users/" + userId.ToString() + "/" + projectId.ToString());

            }
            catch
            {
                throw;
            }
        }

        public async Task<List<User>> GetUsers()
        {
            try
            {
                return await RequestService.Get<List<User>>("https://localhost:44316/api/users/all");

            }
            catch
            {
                throw;
            }
        }

        public async Task<User> GetUser(string name = null, int? id = 0)
        {
            try
            {
                return await RequestService.Get<User>("https://localhost:44316/api/users?name=" + name + "&id=" + id);

            }
            catch
            {
                throw;
            }
        }

        public async Task<List<User>> GetUsersFromProject(int projectId)
        {
            try
            {
                return await RequestService.Get<List<User>>("https://localhost:44316/api/users/" + projectId.ToString());

            }
            catch
            {
                throw;
            }
        }
    }
}
