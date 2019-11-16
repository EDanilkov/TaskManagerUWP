using BusinessLogicModule.Interfaces;
using BusinessLogicModule.Repositories;
using BusinessLogicModule.Services;
using NLog;
using SharedServicesModule.Models;
using SharedServicesModule.ResponseModel;
using SharedServicesModule.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UIModule.Utils;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Task = SharedServicesModule.Models.Task;

namespace UIModule.ViewModels
{
    class ProjectViewModel : NavigateViewModel
    {
        IUserRepository _userRepository = new UserRepository();
        IRoleRepository _roleRepository = new RoleRepository();
        IProjectRepository _projectRepository = new ProjectRepository();
        ITaskRepository _taskRepository = new TaskRepository();
        IPermissionRepository _permissionRepository = new PermissionRepository();
        ICommentRepository _commentRepository = new CommentRepository();



        private static Logger logger = LogManager.GetCurrentClassLogger();

        public ProjectViewModel()
        {

        }

        private bool _isPaneOpen = false;
        public bool IsPaneOpen
        {
            get { return _isPaneOpen; }
            set
            {
                _isPaneOpen = value;
                OnPropertyChanged();
            }
        }

        private List<Task> _listTasks = new List<Task>();
        public List<Task> ListTasks
        {
            get { return _listTasks; }
            set
            {
                _listTasks = value;
                OnPropertyChanged();
            }
        }

        private List<User> _listMembers = new List<User>();
        public List<User> ListMembers
        {
            get { return _listMembers; }
            set
            {
                _listMembers = value;
                OnPropertyChanged();
            }
        }

        private Task _selectedTask;
        public Task SelectedTask
        {
            get { return _selectedTask; }
            set
            {
                _selectedTask = value;
                OnPropertyChanged();
            }
        }

        private int _selectedTaskIndex;
        public int SelectedTaskIndex
        {
            get { return _selectedTaskIndex; }
            set
            {
                _selectedTaskIndex = value;
                OnPropertyChanged();
            }
        }

        private Visibility _changeRoleVisibility = Visibility.Collapsed;
        public Visibility ChangeRoleVisibility
        {
            get { return _changeRoleVisibility; }
            set
            {
                _changeRoleVisibility = value;
                OnPropertyChanged();
            }
        }

        private List<Task> _listUserTask = new List<Task>();
        public List<Task> ListUserTask
        {
            get { return _listUserTask; }
            set
            {
                _listUserTask = value;
                OnPropertyChanged();
            }
        }

        private Role _selectedChangeRole;
        public Role SelectedChangeRole
        {
            get { return _selectedChangeRole; }
            set
            {
                _selectedChangeRole = value;
                OnPropertyChanged();
            }
        }

        private List<Role> _roleSourse = new List<Role>();
        public List<Role> RoleSourse
        {
            get { return _roleSourse; }
            set
            {
                _roleSourse = value;
                OnPropertyChanged();
            }
        }

        private Visibility _deleteMemberButtonVisibility = Visibility.Collapsed;
        public Visibility DeleteMemberButtonVisibility
        {
            get { return _deleteMemberButtonVisibility; }
            set
            {
                _deleteMemberButtonVisibility = value;
                OnPropertyChanged();
            }
        }

        private Visibility _addNewTaskButtonVisibility = Visibility.Collapsed;
        public Visibility AddNewTaskButtonVisibility
        {
            get { return _addNewTaskButtonVisibility; }
            set
            {
                _addNewTaskButtonVisibility = value;
                OnPropertyChanged();
            }
        }

        private Visibility _changeTaskButtonVisibility = Visibility.Collapsed;
        public Visibility ChangeTaskButtonVisibility
        {
            get { return _changeTaskButtonVisibility; }
            set
            {
                _changeTaskButtonVisibility = value;
                OnPropertyChanged();
            }
        }

        private Visibility _deleteTaskButtonVisibility = Visibility.Collapsed;
        public Visibility DeleteTaskButtonVisibility
        {
            get { return _deleteTaskButtonVisibility; }
            set
            {
                _deleteTaskButtonVisibility = value;
                OnPropertyChanged();
            }
        }

        private Visibility _addNewMembersButtonVisibility = Visibility.Collapsed;
        public Visibility AddNewMembersButtonVisibility
        {
            get { return _addNewMembersButtonVisibility; }
            set
            {
                _addNewMembersButtonVisibility = value;
                OnPropertyChanged();
            }
        }

        private Visibility _deleteProjectVisibility = Visibility.Collapsed;
        public Visibility DeleteProjectVisibility
        {
            get { return _deleteProjectVisibility; }
            set
            {
                _deleteProjectVisibility = value;
                OnPropertyChanged();
            }
        }

        private Visibility _visibilityMembers = Visibility.Collapsed;
        public Visibility VisibilityMembers
        {
            get { return _visibilityMembers; }
            set
            {
                _visibilityMembers = value;
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

        private Visibility _notProjectsVisibility = Visibility.Collapsed;
        public Visibility NotProjectsVisibility
        {
            get { return _notProjectsVisibility; }
            set
            {
                _notProjectsVisibility = value;
                OnPropertyChanged();
            }
        }

        private Visibility _projectsVisibility = Visibility.Visible;
        public Visibility ProjectsVisibility
        {
            get { return _projectsVisibility; }
            set
            {
                _projectsVisibility = value;
                OnPropertyChanged();
            }
        }

        private string _projectDescription;
        public string ProjectDescription
        {
            get { return _projectDescription; }
            set
            {
                _projectDescription = value;
                OnPropertyChanged();
            }
        }


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

        private List<User> _newMembersSourse = new List<User>();
        public List<User> NewMembersSourse
        {
            get { return _newMembersSourse; }
            set
            {
                _newMembersSourse = value;
                OnPropertyChanged();
            }
        }

        private double _width;
        public double Width
        {
            get { return _width; }
            set
            {
                _width = value;
                OnPropertyChanged();
            }
        }

        private double _height;
        public double Height
        {
            get { return _height; }
            set
            {
                _height = value;
                OnPropertyChanged();
            }
        }

        private double _heightPane;
        public double HeightPane
        {
            get { return _heightPane; }
            set
            {
                _heightPane = value;
                OnPropertyChanged();
            }
        }

        private string _listTasksText;
        public string ListTasksText
        {
            get { return _listTasksText; }
            set
            {
                _listTasksText = value;
                OnPropertyChanged();
            }
        }

        private User _selectedMember;
        public User SelectedMember
        {
            get { return _selectedMember; }
            set
            {
                _selectedMember = value;
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

        private string _selectedTaskUserName;
        public string SelectedTaskUserName
        {
            get { return _selectedTaskUserName; }
            set
            {
                _selectedTaskUserName = value;
                OnPropertyChanged();
            }
        }

        private string _filter;
        public string Filter
        {
            get { return _filter; }
            set
            {
                _filter = value;
                OnPropertyChanged();
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
                            TaskId = SelectedTask.Id,
                            UserId = (await _userRepository.GetUser(Consts.UserName)).Id,
                            Text = Comment
                        });
                        Comment = "";
                        Comments = (await _commentRepository.GetComment(SelectedTask.Id)).OrderByDescending(c => c.DateTime).ToList();
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex.ToString());
                        //ErrorHandler.Show(Application.Current.Resources["m_error_download"].ToString() + "\n" + ex.Message, _dialogIdentifier);
                    }
                });
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
                        //TaskInfo = Visibility.Collapsed;
                        Role role = await _roleRepository.GetRoleFromUser(Consts.UserName, Consts.ProjectId);
                        await SelectVisibility(role);

                        if (string.Equals((await _roleRepository.GetRoleFromUser(Consts.UserName, Consts.ProjectId)).Name, "Admin"))
                        {
                            VisibilityMembers = Visibility.Visible;
                        }
                        if ((await _projectRepository.GetProject(Consts.ProjectId)).AdminId == (await _userRepository.GetUser(Consts.UserName)).Id)
                        {
                            DeleteProjectVisibility = Visibility.Visible;
                        }

                        Project project = await _projectRepository.GetProject(Consts.ProjectId);
                        ProjectDescription = project.Description;
                        TitleName += project.Name;
                        ListTasks = (await _taskRepository.GetTasksFromProject(Consts.ProjectId));
                        RoleSourse = await _roleRepository.GetRoles();
                        await RefreshUsers();
                        CheckCountTasks(ListTasks);

                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex.ToString());
                        //ErrorHandler.Show(Application.Current.Resources["m_error_download"].ToString() + "\n" + ex.Message, _dialogIdentifier);
                    }
                });
            }
        }

        public async System.Threading.Tasks.Task SelectVisibility(Role role)
        {
            try
            {
                List<Permission> permissions = await _permissionRepository.GetPermissionsFromRole(role.Id);
                foreach (Permission permission in permissions)
                {
                    switch (permission.Name)
                    {
                        case "AddNewTask":
                            {
                                AddNewTaskButtonVisibility = Visibility.Visible;
                                break;
                            }
                        case "DeleteTask":
                            {
                                DeleteTaskButtonVisibility = Visibility.Visible;
                                break;
                            }
                        case "AddNewMembers":
                            {
                                AddNewMembersButtonVisibility = Visibility.Visible;
                                break;
                            }
                        case "DeleteMembers":
                            {
                                DeleteMemberButtonVisibility = Visibility.Visible;
                                break;
                            }
                        case "ChangeRole":
                            {
                                ChangeRoleVisibility = Visibility.Visible;
                                break;
                            }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                //ErrorHandler.Show(Application.Current.Resources["m_error_download"].ToString() + "\n" + ex.Message, _dialogIdentifier);
            }
        }

        public async System.Threading.Tasks.Task RefreshUsers()
        {
            List<User> userInOtherProject = new List<User>();
            List<User> users = (await _userRepository.GetUsers());
            List<User> usersInProject = (await _userRepository.GetUsersFromProject(Consts.ProjectId));
            int number = 0;
            int countUsersInProject = usersInProject.Count;
            foreach (User user in users)
            {
                number = 0;
                foreach (User userProject in usersInProject)
                {
                    if (!string.Equals(user.Login, userProject.Login))
                    {
                        number++;
                    }
                }
                if (number == countUsersInProject)
                {
                    userInOtherProject.Add(user);
                }
            }
            NewMembersSourse = userInOtherProject;
            ListMembers = await _userRepository.GetUsersFromProject(Consts.ProjectId);
        }

        public ICommand HamHamburgerButtonClick
        {
            get
            {
                return new DelegateCommand(async (obj) =>
                {
                    IsPaneOpen = false;
                });
            }
        }

        public ICommand MemberSelectionChanged
        {
            get
            {
                return new DelegateCommand(async (obj) =>
                {
                    try
                    {
                        if (SelectedMember != null)
                        {
                            Project project = await _projectRepository.GetProject(Consts.ProjectId);
                            User user = await _userRepository.GetUser(SelectedMember.Login);
                            int adminId = (await _userRepository.GetUser(SelectedMember.Login)).Id;

                            List<Permission> permissions = await _permissionRepository.GetPermissionsFromRole((await _roleRepository.GetRoleFromUser(Consts.UserName, Consts.ProjectId)).Id);
                            DeleteMemberButtonVisibility = (project.AdminId == adminId ? Visibility.Collapsed :
                                                            permissions.Where(c => string.Equals(c.Name, "DeleteMembers")).ToList().Count == 0 ? Visibility.Collapsed : Visibility.Visible);
                            ChangeRoleVisibility = (project.AdminId == adminId ? Visibility.Collapsed :
                                                            permissions.Where(c => string.Equals(c.Name, "ChangeRole")).ToList().Count == 0 ? Visibility.Collapsed : Visibility.Visible);

                            ListUserTask = await _taskRepository.GetProjectTasksByUser(user.Id, Consts.ProjectId);
                            ListTasksText = ListUserTask.Count == 0 ? Application.Current.Resources["m_member_dont_have_tasks"].ToString() : Application.Current.Resources["mTasks"].ToString();
                            //MemberInfo = Visibility.Visible;
                            Role memberRole = await _roleRepository.GetRoleFromUser(SelectedMember.Login, Consts.ProjectId);
                            SelectedChangeRole = RoleSourse.Find(c => c.Id == memberRole.Id);
                            IsPaneOpen = true;
                        }

                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex.ToString());
                        //ErrorHandler.Show(Application.Current.Resources["m_error_download"].ToString() + "\n" + ex.Message, _dialogIdentifier);
                    }
                });
            }
        }

        public ICommand DeleteMemberClick
        {
            get
            {
                return new DelegateCommand(async (obj) =>
                {
                    try
                    {
                        if ((await _taskRepository.GetProjectTasksByUser(SelectedMember.Id, Consts.ProjectId)).Count != 0)
                        {
                            ContentDialog deleteFileDialog = new ContentDialog()
                            {
                                Title = Application.Current.Resources["MConfirm"].ToString(),
                                Content = Application.Current.Resources["m_delete_all_task"].ToString(),
                                PrimaryButtonText = Application.Current.Resources["MOk"].ToString(),
                                SecondaryButtonText = Application.Current.Resources["MCancel"].ToString()

                            };

                            ContentDialogResult result = await deleteFileDialog.ShowAsync();

                            if (result == ContentDialogResult.Primary)
                            {
                                await _taskRepository.DeleteTasksByUser(SelectedMember.Id, Consts.ProjectId);
                            }
                        }


                        await _userRepository.DeleteUserFromProject(SelectedMember.Id, Consts.ProjectId);

                        List<User> userInOtherProject = new List<User>();
                        List<User> users = (await _userRepository.GetUsers());
                        List<User> usersInProject = (await _userRepository.GetUsersFromProject(Consts.ProjectId));
                        int number = 0;
                        int countUsersInProject = usersInProject.Count;
                        foreach (User user in users)
                        {
                            number = 0;
                            foreach (User userProject in usersInProject)
                            {
                                if (!string.Equals(user.Login, userProject.Login))
                                {
                                    number++;
                                }
                            }
                            if (number == countUsersInProject) userInOtherProject.Add(user);
                        }
                        NewMembersSourse = userInOtherProject;
                        ListMembers = await _userRepository.GetUsersFromProject(Consts.ProjectId);
                        logger.Debug("user " + Consts.UserName + " deleted user " + SelectedMember.Login + " from the project " + (await _projectRepository.GetProject(Consts.ProjectId)).Name);
                        IsPaneOpen = false;
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex.ToString());
                        //ErrorHandler.Show(Application.Current.Resources["m_error_delete_member"].ToString() + "\n" + ex.Message, _dialogIdentifier);
                    }
                });
            }
        }

        public ICommand DeleteTaskClick
        {
            get
            {
                return new DelegateCommand(async (obj) =>
                {
                    try
                    {
                        string taskName = SelectedTask.Name;
                        await _taskRepository.DeleteTask(SelectedTask.Id);

                        ListTasks = (await _taskRepository.GetTasksFromProject(Consts.ProjectId));
                        IsPaneOpen = false;
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

        public ICommand TaskOpenButtonClick
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    try
                    {
                        Consts.TaskId = SelectedTask.Id;
                        NavigationService.Instance.NavigateTo(typeof(Pages.Task));
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex.ToString());
                        //ErrorHandler.Show(Application.Current.Resources["m_error_download"].ToString(), _dialogIdentifier);
                    }
                });
            }
        }

        public ICommand PaneClosing
        {
            get
            {
                return new DelegateCommand(async (obj) =>
                {
                    SelectedMember = null;
                    SelectedTask = null;
                    IsPaneOpen = false;
                });
            }
        }

        public ICommand SelectionChanged
        {
            get
            {
                return new DelegateCommand(async (obj) =>
                {
                    if (SelectedTask != null)
                    {
                        string userRole = (await _roleRepository.GetRoleFromUser(Consts.UserName, Consts.ProjectId)).Name;
                        if (string.Equals(userRole, "Admin") || SelectedTask.UserId == (await _userRepository.GetUser(Consts.UserName)).Id)
                        {
                            CommentsVisibility = Visibility.Visible;
                        }
                        SelectedTaskUserName = (await _userRepository.GetUser(null, SelectedTask.UserId)).Login;
                        Comments = (await _commentRepository.GetComment(SelectedTask.Id)).OrderByDescending(c => c.DateTime).ToList();
                    }
                    IsPaneOpen = true;
                });
            }
        }


        public ICommand AddNewTaskClick => new DelegateCommand(AddNewTask);

        private async void AddNewTask(object o)
        {
            try
            {

                NavigationService.Instance.NavigateTo(typeof(Pages.AddNewTask));
                ListTasks = (await _taskRepository.GetTasksFromProject(Consts.ProjectId));
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
            }
        }

        public ICommand FilterChanged
        {
            get
            {
                return new DelegateCommand(async (obj) =>
                {
                    try
                    {
                        List<Task> filteredTasks = await FilterProjects();
                        CheckCountTasks(filteredTasks);
                        ListTasks = filteredTasks;
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex.ToString());
                        //ErrorHandler.Show(ex.Message, _dialogIdentifier);
                    }
                });
            }
        }

        private async Task<List<Task>> FilterProjects()
        {
            if (Filter != null || Filter != "")
            {
                List<Task> filteredTasks = new List<Task>();
                List<Task> tasks = await _taskRepository.GetTasksFromProject(Consts.ProjectId);
                foreach (Task task in tasks)
                {
                    if (task.Name.Contains(Filter))
                    {
                        filteredTasks.Add(task);
                    }
                }
                CheckCountTasks(filteredTasks);
                return filteredTasks;
            }
            else
            {
                return await _taskRepository.GetTasksFromProject(Consts.ProjectId);
            }
        }

        private void CheckCountTasks(List<Task> tasks)
        {
            try
            {
                if (tasks.Count == 0)
                {
                    ProjectsVisibility = Visibility.Collapsed;
                    NotProjectsVisibility = Visibility.Visible;
                }
                else
                {
                    ProjectsVisibility = Visibility.Visible;
                    NotProjectsVisibility = Visibility.Collapsed;
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
            }
        }

        public ICommand AddNewMemberClick => new DelegateCommand(AddNewMember);

        private async void AddNewMember(object o)
        {
            try
            {
                
                await RefreshUsers();
                NavigationService.Instance.NavigateTo(typeof(Pages.AddNewMember));
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
            }
        }
    }
}
