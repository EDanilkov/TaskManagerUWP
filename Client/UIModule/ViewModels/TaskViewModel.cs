using BusinessLogicModule.Interfaces;
using BusinessLogicModule.Repositories;
using NLog;
using SharedServicesModule.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UIModule.Utils;
using Windows.UI.Xaml;
using Task = SharedServicesModule.Models.Task;

namespace UIModule.ViewModels
{
    class TaskViewModel : NavigateViewModel
    {

        private static Logger logger = LogManager.GetCurrentClassLogger();
        IUserRepository _userRepository = new UserRepository();
        ITaskRepository _taskRepository = new TaskRepository();
        IRoleRepository _roleRepository = new RoleRepository();
        IPermissionRepository _permissionRepository = new PermissionRepository();
        IProjectRepository _projectRepository = new ProjectRepository();
        ICommentRepository _commentRepository = new CommentRepository();


        /*public TaskViewModel(IUserRepository userRepository, ITaskRepository taskRepository, IProjectRepository projectRepository, IPermissionRepository permissionRepository, IRoleRepository roleRepository, IUserProjectRepository userProjectRepository)
        {
            Title = "Task";

            _userRepository = userRepository;
            _taskRepository = taskRepository;
            _projectRepository = projectRepository;
            _permissionRepository = permissionRepository;
            _roleRepository = roleRepository;
        }*/

        private string _titleName;
        public string TitleName
        {
            get { return _titleName; }
            set
            {
                _titleName = value;
                OnPropertyChanged();
            }
        }

        private string _titleProject;
        public string TitleProject
        {
            get { return _titleProject; }
            set
            {
                _titleProject = value;
                OnPropertyChanged();
            }
        }

        private string _userName;
        public string UserName
        {
            get { return _userName; }
            set
            {
                _userName = value;
                OnPropertyChanged();
            }
        }

        private string _taskStartDate;
        public string TaskStartDate
        {
            get { return _taskStartDate; }
            set
            {
                _taskStartDate = value;
                OnPropertyChanged();
            }
        }

        private string _taskFinishDate;
        public string TaskFinishDate
        {
            get { return _taskFinishDate; }
            set
            {
                _taskFinishDate = value;
                OnPropertyChanged();
            }
        }

        private string _taskDescriprion;
        public string TaskDescriprion
        {
            get { return _taskDescriprion; }
            set
            {
                _taskDescriprion = value;
                OnPropertyChanged();
            }
        }


        
        private string _comment;
        public string Comment
        {
            get { return _comment; }
            set
            {
                _comment = value;
                OnPropertyChanged();
            }
        }

        private List<Comment> _comments = new List<Comment>();
        public List<Comment> Comments
        {
            get { return _comments; }
            set
            {
                _comments = value;
                OnPropertyChanged();
            }
        }

        private Visibility _deleteTaskVisibility = Visibility.Collapsed;
        public Visibility DeleteTaskVisibility
        {
            get { return _deleteTaskVisibility; }
            set
            {
                _deleteTaskVisibility = value;
                OnPropertyChanged();
            }
        }

        private Visibility _changeTaskVisibility = Visibility.Collapsed;
        public Visibility ChangeTaskVisibility
        {
            get { return _changeTaskVisibility; }
            set
            {
                _changeTaskVisibility = value;
                OnPropertyChanged();
            }
        }
        
        private Visibility _commentsVisibility = Visibility.Collapsed;
        public Visibility CommentsVisibility
        {
            get { return _commentsVisibility; }
            set
            {
                _commentsVisibility = value;
                OnPropertyChanged();
            }
        }


        public ICommand Loaded
        {
            get
            {
                return new DelegateCommand(async (obj) =>
                {
                    try
                    {

                        List<Permission> permissions = await _permissionRepository.GetPermissionsFromRole((await _roleRepository.GetRoleFromUser(Consts.UserName, Consts.ProjectId)).Id);
                        if (permissions.Any(c => string.Equals(c.Name, "ChangeTask")) != false)
                        {
                            ChangeTaskVisibility = Visibility.Visible;
                        }
                        if (permissions.Any(c => string.Equals(c.Name, "DeleteTask")) != false)
                        {
                            DeleteTaskVisibility = Visibility.Visible;
                        }
                        string userRole = (await _roleRepository.GetRoleFromUser(Consts.UserName, Consts.ProjectId)).Name;
                        if (string.Equals(userRole, "Admin") || (await _taskRepository.GetTask(Consts.TaskId)).UserId == (await _userRepository.GetUser(Consts.UserName)).Id)
                        {
                            CommentsVisibility = Visibility.Visible;
                        }
                        Comments = (await _commentRepository.GetComment(Consts.TaskId)).OrderByDescending(c => c.DateTime).ToList();

                        Task task = await _taskRepository.GetTask(Consts.TaskId);
                        TitleName = task.Name;
                        Project project = await _projectRepository.GetProject(Consts.ProjectId);
                        TitleProject += project.Name;
                        UserName = ": " + (await _userRepository.GetUser(id: task.UserId)).Login;
                        TaskDescriprion = task.Description;
                        TaskFinishDate = ": " + task.EndDate;
                        TaskStartDate = ": " + task.BeginDate;
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex.ToString());
                        //ErrorHandler.Show(Application.Current.Resources["m_error_download"].ToString() + "\n" + ex.Message, _dialogIdentifier);
                    }
                });
            }
        }

        public ICommand AddComment
        {
            get
            {
                return new DelegateCommand(async (obj) =>
                {
                    try
                    {
                        await _commentRepository.AddComment(new Comment()
                        {
                            UserName = Consts.UserName,
                            DateTime = DateTime.Now,
                            TaskId = Consts.TaskId,
                            UserId = (await _userRepository.GetUser(Consts.UserName)).Id,
                            Text = Comment
                        });
                        Comment = "";
                        Comments = (await _commentRepository.GetComment(Consts.TaskId)).OrderByDescending(c => c.DateTime).ToList();
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex.ToString());
                        //ErrorHandler.Show(Application.Current.Resources["m_error_download"].ToString() + "\n" + ex.Message, _dialogIdentifier);
                    }
                });
            }
        }

        public ICommand Back
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    NavigationService.Instance.NavigateTo(typeof(Pages.Project));
                });
            }
        }

        public ICommand DeleteTask
        {
            get
            {
                return new DelegateCommand(async (obj) =>
                {
                    try
                    {
                        string taskName = (await _taskRepository.GetTask(Consts.TaskId)).Name;
                        await _taskRepository.DeleteTask(Consts.TaskId);
                        NavigationService.Instance.NavigateTo(typeof(Pages.Project));
                        logger.Debug("user " + Consts.UserName + " deleted task " + taskName + " to the project " + (await _projectRepository.GetProject(Consts.ProjectId)).Name);
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex.ToString());
                        //ErrorHandler.Show(Application.Current.Resources["m_error_delete_task"].ToString() + "\n" + ex.Message, _dialogIdentifier);
                    }
                });
            }
        }
        
        public ICommand ChangeTaskClick
        {
            get
            {
                return new DelegateCommand(async (obj) =>
                {
                    try
                    {
                        NavigationService.Instance.NavigateTo(typeof(Pages.ChangeTask));
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex.ToString());
                        //ErrorHandler.Show(Application.Current.Resources["m_error_delete_task"].ToString() + "\n" + ex.Message, _dialogIdentifier);
                    }
                });
            }
        }

        /*public ICommand ChangeTaskClick => new DelegateCommand(ChangeTask);

        private async void ChangeTask(object o)
        {
            try
            {
                var view = new Pages.ChangeTask
                {
                    DataContext = new ChangeTaskViewModel(_userRepository, _taskRepository, _projectRepository)
                };

                var result = await DialogHost.Show(view, _dialogIdentifier);

                string userName = System.Windows.Application.Current.Properties["UserName"].ToString();
                int taskId = int.Parse(System.Windows.Application.Current.Properties["TaskId"].ToString());
                int projectId = int.Parse(System.Windows.Application.Current.Properties["ProjectId"].ToString());
                Task task = await _taskRepository.GetTask(taskId);
                TitleName = "";
                TitleName += "/" + task.Name;
                Project project = await _projectRepository.GetProject(projectId);
                TitleProject = "";
                TitleProject += project.Name;
                UserName = ": " + (await _userRepository.GetUser(id: task.UserId)).Login;
                TaskDescriprion = task.Description;
                TaskFinishDate = ": " + task.EndDate.ToShortDateString();
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
            }
        }*/
    }
}
