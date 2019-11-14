using BusinessLogicModule.Services;
using Newtonsoft.Json;
using SharedServicesModule.ResponseModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogicModule.Interfaces
{
    public class TaskRepository : ITaskRepository
    {
        public async Task<NewResponseModel> AddTask(SharedServicesModule.Models.Task task)
        {
            try
            {
                string json = JsonConvert.SerializeObject(task);
                return await RequestService.Post("https://localhost:44393/api/tasks/new", json);
            }
            catch
            {
                throw;
            }
        }

        public async System.Threading.Tasks.Task ChangeTask(SharedServicesModule.Models.Task task, string taskName, string taskDescription, int userId, DateTime taskFinishDate)
        {
            try
            {

                UpdateTaskModel updateTaskModel = new UpdateTaskModel() { Task = task, TaskName = taskName, TaskDescription = taskDescription, UserId = userId, TaskFinishDate = taskFinishDate };
                string json = JsonConvert.SerializeObject(updateTaskModel);
                await RequestService.Put("https://localhost:44393/api/tasks/", json);
            }
            catch
            {
                throw;
            }
        }

        public async System.Threading.Tasks.Task DeleteTask(int taskId)
        {
            try
            {
                await RequestService.Delete("https://localhost:44393/api/tasks/" + taskId.ToString());
            }
            catch
            {
                throw;
            }
        }

        public async System.Threading.Tasks.Task DeleteTasksByUser(int userId, int projectId)
        {
            try
            {

                await RequestService.Delete("https://localhost:44393/api/tasks/" + userId.ToString() + "/" + projectId.ToString());
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<SharedServicesModule.Models.Task>> GetTasks()
        {
            try
            {
                return await RequestService.Get<List<SharedServicesModule.Models.Task>>("https://localhost:44393/api/tasks/all");

            }
            catch
            {
                throw;
            }
        }
        
        public async Task<SharedServicesModule.Models.Task> GetTask(int taskId)
        {
            try
            {
                return await RequestService.Get<SharedServicesModule.Models.Task>("https://localhost:44393/api/tasks/" + taskId.ToString());

            }
            catch
            {
                throw;
            }
        }

        public async Task<List<SharedServicesModule.Models.Task>> GetTasksFromProject(int projectId)
        {
            try
            {
                return await RequestService.Get<List<SharedServicesModule.Models.Task>>("https://localhost:44393/api/tasks/projects/" + projectId.ToString());
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<SharedServicesModule.Models.Task>> GetProjectTasksByUser(int userId, int projectId)
        {
            try
            {
                return await RequestService.Get<List<SharedServicesModule.Models.Task>>("https://localhost:44393/api/tasks/" + userId.ToString() + "/" + projectId.ToString());

            }
            catch
            {
                throw;
            }
        }
        
    }
}
