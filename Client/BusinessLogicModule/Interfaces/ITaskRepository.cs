using SharedServicesModule.ResponseModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogicModule.Interfaces
{
    public interface ITaskRepository
    {
        /// <summary>
        /// Saves info about provided task to the database.
        /// </summary>
        /// <param name="task">The object to be added to the Task.</param>
        /// <returns>Retrieves response to the query.</returns>
        Task<NewResponseModel> AddTask(SharedServicesModule.Models.Task task);

        /// <summary>
        /// Changes an object from the Task.
        /// </summary>
        /// <param name="task"></param>
        /// <param name="taskName"></param>
        /// <param name="taskDescription"></param>
        /// <param name="userId"></param>
        /// <param name="statusId"></param>
        /// <param name="taskFinishDate"></param>
        /// <returns></returns>
        Task ChangeTask(SharedServicesModule.Models.Task task, string taskName, string taskDescription, int userId, int statusId, DateTime taskFinishDate);

        /// <summary>
        /// Deletes an object from the Task with provided taskId.
        /// </summary>
        /// <param name="taskId"></param>
        Task DeleteTask(int taskId);
        /// <summary>
        /// Deletes an object from the Task with provided userId and projectId.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        Task DeleteTasksByUser(int userId, int projectId);

        /// <summary>
        /// Returns all tasks.
        /// </summary>
        /// <returns>A  List&lt;Task>; 
        /// otherwise, an empty  List&lt;Task>.
        /// </returns>
        Task<List<SharedServicesModule.Models.Task>> GetTasks();
        /// <summary>
        /// Returns task with provided taskId.
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns>A task that match the conditions defined by the specified parameters if found; 
        /// otherwise, an empty.
        /// </returns>
        Task<SharedServicesModule.Models.Task> GetTask(int taskId);
        /// <summary>
        /// Returns all projects with provided projectId.
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns>A  List&lt;Task> containing all the elements that match the conditions defined by the specified parameter if found; 
        /// otherwise, an empty  List&lt;Task>.
        /// </returns>
        Task<List<SharedServicesModule.Models.Task>> GetTasksFromProject(int projectId);
        /// <summary>
        /// Returns all projects with provided userId and projectId.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="projectId"></param>
        /// <returns>A  List&lt;Task> containing all the elements that match the conditions defined by the specified parameters if found; 
        /// otherwise, an empty  List&lt;Task>.
        /// </returns>
        Task<List<SharedServicesModule.Models.Task>> GetProjectTasksByUser(int userId, int projectId);
    }
}
