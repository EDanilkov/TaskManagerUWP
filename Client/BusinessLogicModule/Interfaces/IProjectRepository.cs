using SharedServicesModule.Models;
using SharedServicesModule.ResponseModel;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace BusinessLogicModule.Interfaces
{
    public interface IProjectRepository
    {
        /// <summary>
        /// Saves info about provided project to the database.
        /// </summary>
        /// <param name="project">The object to be added to the Project.</param>
        /// <returns>Retrieves response to the query</returns>
        Task<NewResponseModel> AddProject(Project project);

        /// <summary>
        /// Deletes an object from the Project.
        /// </summary>
        /// <param name="projectId"></param>
        System.Threading.Tasks.Task DeleteProject(int projectId);
        /// <summary>
        /// Deletes tasks from the Project that match the conditions defined by the specified parameter.
        /// </summary>
        /// <param name="projectId"></param>
        System.Threading.Tasks.Task DeleteTasksFromProject(int projectId);
        /// <summary>
        /// Deletes users from the Project that match the conditions defined by the specified parameter.
        /// </summary>
        /// <param name="projectId"></param>
        System.Threading.Tasks.Task DeleteUsersFromProject(int projectId);

        /// <summary>
        /// Returns project with provided projectId.
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns>A project that match the conditions defined by the specified parameter if found; 
        /// otherwise, an empty.
        /// </returns>
        Task<Project> GetProject(int projectId);
        /// <summary>
        /// Returns all projects.
        /// </summary>
        /// <returns>A  List&lt;Project>; 
        /// otherwise, an empty  List&lt;Project>.
        /// </returns>
        Task<List<Project>> GetProjects();
        /// <summary>
        /// Retrieves all the tasks from projects that match the conditions defined by the specified parameter.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>A  List&lt;Task> containing all the elements that match the conditions defined by the specified parameter if found; 
        /// otherwise, an empty  List&lt;Task>.
        /// </returns>
        Task<List<SharedServicesModule.Models.Task>> GetTasksFromUser(int userId);
        /// <summary>
        /// Returns all projects with provided userName.
        /// </summary>
        /// <param name="userName"></param>
        /// <returns>A  List&lt;Project> containing all the elements that match the conditions defined by the specified parameter if found; 
        /// otherwise, an empty  List&lt;Project>.
        /// </returns>
        Task<List<Project>> GetProjectsFromUser(string userName);
    }
}
