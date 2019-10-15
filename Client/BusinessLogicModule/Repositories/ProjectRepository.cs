using BusinessLogicModule.Services;
using Newtonsoft.Json;
using SharedServicesModule.Models;
using SharedServicesModule.ResponseModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogicModule.Interfaces
{
    public class ProjectRepository : IProjectRepository
    {

        public async Task<NewResponseModel> AddProject(Project project)
        {
            try
            {

                string json = JsonConvert.SerializeObject(project);
                return await RequestService.Post("https://localhost:44316/api/projects/new", json);
            }
            catch
            {
                throw;
            }
        }

        public async System.Threading.Tasks.Task DeleteProject(int projectId)
        {
            try
            {
                await RequestService.Delete("https://localhost:44316/api/projects/" + projectId.ToString());

            }
            catch
            {
                throw;
            }
        }

        public async System.Threading.Tasks.Task DeleteTasksFromProject(int projectId)
        {
            try
            {
                await RequestService.Delete("https://localhost:44316/api/projects/tasks/" + projectId.ToString());

            }
            catch
            {
                throw;
            }
        }

        public async System.Threading.Tasks.Task DeleteUsersFromProject(int projectId)
        {
            try
            {
                await RequestService.Delete("https://localhost:44316/api/projects/users/" + projectId.ToString());

            }
            catch
            {
                throw;
            }
        }

        public async Task<Project> GetProject(int projectId)
        {
            try
            {
                return await RequestService.Get<Project>("https://localhost:44316/api/projects/" + projectId.ToString());

            }
            catch
            {
                throw;
            }
        }

        public async Task<List<Project>> GetProjects()
        {
            try
            {
                return await RequestService.Get<List<Project>>("https://localhost:44316/api/projects/all");

            }
            catch
            {
                throw;
            }
        }

        public async Task<List<SharedServicesModule.Models.Task>> GetTasksFromUser(int userId)
        {
            try
            {
                return await RequestService.Get<List<SharedServicesModule.Models.Task>>("https://localhost:44316/api/projects/tasks/" + userId.ToString());

            }
            catch
            {
                throw;
            }
        }
        
        public async Task<List<Project>> GetProjectsFromUser(string userName)
        {
            try
            {
                return await RequestService.Get<List<Project>>("https://localhost:44316/api/projects/users/" + userName);
            }
            catch
            {
                throw;
            }
        }
    }
}
