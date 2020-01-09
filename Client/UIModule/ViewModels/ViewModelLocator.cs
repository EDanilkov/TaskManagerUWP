using Autofac;
using BusinessLogicModule.Interfaces;
using BusinessLogicModule.Repositories;

namespace UIModule.ViewModels
{
    public class ViewModelLocator
    {
        private static readonly IContainer Container;

        static ViewModelLocator()
        {
            // register logger database by type i.e. ILogger
            var builder = new ContainerBuilder();

            // register ViewModels and Inject Repositories
            builder.RegisterType<UserRepository>().As<IUserRepository>().InstancePerDependency();
            builder.RegisterType<ProjectRepository>().As<IProjectRepository>().InstancePerDependency();
            builder.RegisterType<RoleRepository>().As<IRoleRepository>().InstancePerDependency();
            builder.RegisterType<CommentRepository>().As<ICommentRepository>().InstancePerDependency();
            builder.RegisterType<PermissionRepository>().As<IPermissionRepository>().InstancePerDependency();
            builder.RegisterType<RolePermissionRepository>().As<IRolePermissionRepository>().InstancePerDependency();
            builder.RegisterType<StatusRepository>().As<IStatusRepository>().InstancePerDependency();
            builder.RegisterType<TaskRepository>().As<ITaskRepository>().InstancePerDependency();
            builder.RegisterType<UserProjectRepository>().As<IUserProjectRepository>().InstancePerDependency();
            
            builder.RegisterType<ProjectViewModel>();
            builder.RegisterType<ProjectsViewModel>();
            builder.RegisterType<AddNewMemberViewModel>();
            builder.RegisterType<AddNewProjectViewModel>();
            builder.RegisterType<AddNewTaskViewModel>();
            builder.RegisterType<AuthorizationViewModel>();
            builder.RegisterType<ChangeTaskViewModel>();
            builder.RegisterType<MainPageViewModel>();
            builder.RegisterType<ProfileViewModel>();
            builder.RegisterType<SettingsViewModel>();
            builder.RegisterType<TaskViewModel>();

            Container = builder.Build();
        }
        
        public ICommentRepository CommentRepository => Container.Resolve<ICommentRepository>();
        public IPermissionRepository PermissionRepository => Container.Resolve<IPermissionRepository>();
        public IProjectRepository ProjectRepository => Container.Resolve<IProjectRepository>();
        public IRolePermissionRepository RolePermissionRepository => Container.Resolve<IRolePermissionRepository>();
        public IRoleRepository RoleRepository => Container.Resolve<IRoleRepository>();
        public IStatusRepository StatusRepository => Container.Resolve<IStatusRepository>();
        public ITaskRepository TaskRepository => Container.Resolve<ITaskRepository>();
        public IUserProjectRepository UserProjectRepository => Container.Resolve<IUserProjectRepository>();
        public IUserRepository UserRepository => Container.Resolve<IUserRepository>();

        // property to access ViewModel in XAML
        public ProjectsViewModel ProjectsViewModel => Container.Resolve<ProjectsViewModel>();
        public AddNewMemberViewModel AddNewMemberViewModel => Container.Resolve<AddNewMemberViewModel>();
        public AddNewProjectViewModel AddNewProjectViewModel => Container.Resolve<AddNewProjectViewModel>();
        public AddNewTaskViewModel AddNewTaskViewModel => Container.Resolve<AddNewTaskViewModel>();
        public AuthorizationViewModel AuthorizationViewModel => Container.Resolve<AuthorizationViewModel>();
        public ChangeTaskViewModel ChangeTaskViewModel => Container.Resolve<ChangeTaskViewModel>();
        public MainPageViewModel MainPageViewModel => Container.Resolve<MainPageViewModel>();
        public ProfileViewModel ProfileViewModel => Container.Resolve<ProfileViewModel>();
        public ProjectViewModel ProjectViewModel => Container.Resolve<ProjectViewModel>();
        public SettingsViewModel SettingsViewModel => Container.Resolve<SettingsViewModel>();
        public TaskViewModel TaskViewModel => Container.Resolve<TaskViewModel>();
    }
}
