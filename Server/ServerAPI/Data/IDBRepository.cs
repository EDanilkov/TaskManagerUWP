using ServerAPI.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskThreading = System.Threading.Tasks.Task;

namespace ServerAPI.Data
{
    public interface IDBRepository
    {
        #region Task

        TaskThreading AddTask(Models.Task task);
        TaskThreading DeleteTask(Models.Task task);
        TaskThreading DeleteTask(int taskId);
        TaskThreading DeleteProject(int projectId);
        TaskThreading ChangeTask(Models.Task task);
        Task<List<Models.Task>> GetTasks();
        Task<List<Models.Task>> GetTasks(int userId, int projectId);
        Task<Models.Task> GetTask(int taskId);
        Task<List<Models.Task>> GetTasksFromUser(int userId);
        Task<List<Models.Task>> GetProjectTasksFromUser(int userId, int projectId);

        #endregion

        #region Role

        TaskThreading AddRole(Role role);
        Task<List<Role>> GetRoles();
        Task<Role> GetRole(string roleName);
        Task<Role> GetRole(int roleId);

        #endregion

        #region Permission

        TaskThreading AddPermission(Permission permission);
        Task<Permission> GetPermission(int permissionId);
        Task<Permission> GetPermission(string permissionName);
        Task<List<Permission>> GetPermissionsByRole(int roleId);

        #endregion

        #region UserProject

        TaskThreading AddUserProject(UserProject userProject);
        TaskThreading DeleteUserProject(UserProject userProject);
        Task<List<UserProject>> GetUserProject();
        Task<UserProject> GetUserProject(int userId, int projectId);
        Task<UserProject> GetUserProject(string userName, int projectId);
        Task<List<UserProject>> GetUserProjectByProjectId(int projectId);
        Task<List<UserProject>> GetUserProjectByUserId(int userId);

        #endregion

        #region RolePermission

        TaskThreading AddRolePermission(RolePermission rolePermission);
        Task<List<RolePermission>> GetRolePermissionByRoleId(int roleId);

        #endregion

        #region Project

        TaskThreading AddProject(Project project);
        TaskThreading DeleteTasksFromProject(int projectId);
        TaskThreading DeleteUsersFromProject(int projectId);
        TaskThreading DeleteUserFromProject(int userId, int projectId);
        TaskThreading DeleteTasksFromUser(int userId, int projectId);
        Task<Project> GetProject(int projectId);
        Task<List<Project>> GetProjects();
        Task<List<User>> GetUsersFromProject(int projectId);
        Task<List<Models.Task>> GetTasksFromProject(int projectId);

        #endregion

        #region User

        TaskThreading AddUser(User user);
        Task<Role> GetRoleFromUser(string userName, int projectId);
        Task<List<User>> GetUsers();
        Task<User> GetUser(string name);
        Task<User> GetUser(int id);
        Task<List<Project>> GetProjectsFromUser(string userName);

        #endregion

        #region Comment

        TaskThreading AddComment(Comment comment);
        Task<List<Comment>> GetCommentByTaskId(int taskId);


        #endregion


        #region Status

        TaskThreading AddStatus(Status status);
        Task<Status> GetStatus(int statusId);
        Task<Status> GetStatus(string statusName);
        Task<List<Status>> GetStatuses();
        #endregion
    }
}
