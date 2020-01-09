using SharedServicesModule.Models;
using SharedServicesModule.ResponseModel;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace BusinessLogicModule.Interfaces
{
    public interface IProjectRepository
    {
        Task<NewResponseModel> AddProject(Project project);

        System.Threading.Tasks.Task DeleteProject(int projectId);
        System.Threading.Tasks.Task DeleteTasksFromProject(int projectId);
        System.Threading.Tasks.Task DeleteUsersFromProject(int projectId);

        Task<Project> GetProject(int projectId);
        Task<List<Project>> GetProjects();
        Task<List<SharedServicesModule.Models.Task>> GetTasksFromUser(int userId);
        Task<List<Project>> GetProjectsFromUser(string userName);
    }
}
