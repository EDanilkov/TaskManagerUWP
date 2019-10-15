using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using ServerAPI.Data;
using ServerAPI.Data.Models;
using System.Collections.Generic;

namespace ServerAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            TaskManagerContext taskManagerContext = new TaskManagerContext();
            var webHost = CreateWebHostBuilder(args).Build();
            var status = System.Threading.Tasks.Task.Run(async () =>
            {
                await CheckRoles();
            });
            webHost.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();


        async static System.Threading.Tasks.Task CheckRoles()
        {
            DBRepository db = new DBRepository();
            List<Role> roles = await db.GetRoles();
            if (roles.Count == 0)
            {
                Permission AddNewTask = new Permission() { Name = "AddNewTask" };
                await db.AddPermission(AddNewTask);
                Permission ChangeTask = new Permission() { Name = "ChangeTask" };
                await db.AddPermission(ChangeTask);
                Permission DeleteTask = new Permission() { Name = "DeleteTask" };
                await db.AddPermission(DeleteTask);
                Permission VisibilityTask = new Permission() { Name = "VisibilityTask" };
                await db.AddPermission(VisibilityTask);
                Permission DeleteProject = new Permission() { Name = "DeleteProject" };
                await db.AddPermission(DeleteProject);
                Permission AddNewMembers = new Permission() { Name = "AddNewMembers" };
                await db.AddPermission(AddNewMembers);
                Permission DeleteMembers = new Permission() { Name = "DeleteMembers" };
                await db.AddPermission(DeleteMembers);
                Permission ChangeRole = new Permission() { Name = "ChangeRole" };
                await db.AddPermission(ChangeRole);
                List<Permission> permissions = new List<Permission>()
                {
                    (await db.GetPermission("AddNewTask")),
                    (await db.GetPermission("ChangeTask")),
                    (await db.GetPermission("DeleteTask")),
                    (await db.GetPermission("VisibilityTask")),
                    (await db.GetPermission("DeleteProject")),
                    (await db.GetPermission("AddNewMembers")),
                    (await db.GetPermission("DeleteMembers")),
                    (await db.GetPermission("ChangeRole"))
                };
                Role Admin = new Role() { Name = "Admin" };
                Role Developer = new Role() { Name = "Developer" };
                Role Manager = new Role() { Name = "Manager" };
                await db.AddRole(Admin);
                await db.AddRole(Developer);
                await db.AddRole(Manager);
                foreach (Permission permission in permissions)
                {
                    RolePermission rolePermission = new RolePermission()
                    {
                        RoleId = (await db.GetRole("Admin")).Id,
                        PermissionId = permission.Id
                    };
                    await db.AddRolePermission(rolePermission);
                }
                RolePermission rolePermission1 = new RolePermission()
                {
                    RoleId = (await db.GetRole("Manager")).Id,
                    PermissionId = (await db.GetPermission("AddNewTask")).Id
                };
                RolePermission rolePermission2 = new RolePermission()
                {
                    RoleId = (await db.GetRole("Manager")).Id,
                    PermissionId = (await db.GetPermission("ChangeTask")).Id
                };
                RolePermission rolePermission3 = new RolePermission()
                {
                    RoleId = (await db.GetRole("Manager")).Id,
                    PermissionId = (await db.GetPermission("DeleteTask")).Id
                };
                RolePermission rolePermission4 = new RolePermission()
                {
                    RoleId = (await db.GetRole("Manager")).Id,
                    PermissionId = (await db.GetPermission("VisibilityTask")).Id
                };
                await db.AddRolePermission(rolePermission1);
                await db.AddRolePermission(rolePermission2);
                await db.AddRolePermission(rolePermission3);
                await db.AddRolePermission(rolePermission4);
            }
        }
    }
}
