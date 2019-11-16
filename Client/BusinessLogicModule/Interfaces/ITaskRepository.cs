using SharedServicesModule.ResponseModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogicModule.Interfaces
{
    public interface ITaskRepository
    {
        Task<NewResponseModel> AddTask(SharedServicesModule.Models.Task task);

        Task ChangeTask(SharedServicesModule.Models.Task task, string taskName, string taskDescription, int userId, int statusId, DateTime taskFinishDate);

        Task DeleteTask(int taskId);
        Task DeleteTasksByUser(int userId, int projectId);

        Task<List<SharedServicesModule.Models.Task>> GetTasks();
        Task<SharedServicesModule.Models.Task> GetTask(int taskId);
        Task<List<SharedServicesModule.Models.Task>> GetTasksFromProject(int projectId);
        Task<List<SharedServicesModule.Models.Task>> GetProjectTasksByUser(int userId, int projectId);
    }
}
