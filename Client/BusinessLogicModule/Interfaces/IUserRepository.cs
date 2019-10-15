using SharedServicesModule.Models;
using SharedServicesModule.ResponseModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogicModule.Interfaces
{
    public interface IUserRepository
    {
        Task<NewResponseModel> AddUser(User user);

        System.Threading.Tasks.Task DeleteUserFromProject(int userId, int projectId);

        Task<List<User>> GetUsers();
        Task<User> GetUser(string name = null, int? id = 0);
        Task<List<User>> GetUsersFromProject(int projectId);

    }
}
