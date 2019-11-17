using BusinessLogicModule.Interfaces;
using BusinessLogicModule.Repositories;
using NLog;
using SharedServicesModule.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using UIModule.Utils;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Task = SharedServicesModule.Models.Task;

namespace UIModule.ViewModels
{
    class ProjectViewModel : NavigateViewModel
    {
        IUserRepository _userRepository;
        IRoleRepository _roleRepository;
        IProjectRepository _projectRepository;
        ITaskRepository _taskRepository;
        IPermissionRepository _permissionRepository;
        ICommentRepository _commentRepository;
        IStatusRepository _statusRepository;


        private static Logger logger;

        public ProjectViewModel()
        {
            _userRepository = new UserRepository();
            _roleRepository = new RoleRepository();
            _projectRepository = new ProjectRepository();
            _taskRepository = new TaskRepository();
            _permissionRepository = new PermissionRepository();
            _commentRepository = new CommentRepository();
            _statusRepository = new StatusRepository();
            
            logger = LogManager.GetCurrentClassLogger();
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

        private List<RecordListBoxTasks> _listTasks = new List<RecordListBoxTasks>();
        public List<RecordListBoxTasks> ListTasks
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

        private RecordListBoxTasks _selectedTask;
        public RecordListBoxTasks SelectedTask
        {
            get { return _selectedTask; }
            set
            {
                _selectedTask = value;
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

        private Visibility _notTasksVisibility = Visibility.Collapsed;
        public Visibility NotTasksVisibility
        {
            get { return _notTasksVisibility; }
            set
            {
                _notTasksVisibility = value;
                OnPropertyChanged();
            }
        }

        private Visibility _tasksVisibility = Visibility.Visible;
        public Visibility TasksVisibility
        {
            get { return _tasksVisibility; }
            set
            {
                _tasksVisibility = value;
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

        private Visibility _pageVisibility = Visibility.Collapsed;
        public Visibility PageVisibility
        {
            get { return _pageVisibility; }
            set
            {
                _pageVisibility = value;
                OnPropertyChanged();
            }
        }

        private Visibility _loadingVisibility = Visibility.Collapsed;
        public Visibility LoadingVisibility
        {
            get { return _loadingVisibility; }
            set
            {
                _loadingVisibility = value;
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
                            TaskId = SelectedTask.TaskId,
                            UserId = Consts.UserId,
                            Text = Comment
                        });
                        Comment = "";
                        Comments = (await _commentRepository.GetComment(SelectedTask.TaskId)).OrderByDescending(c => c.DateTime).ToList();
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
                        PageVisibility = Visibility.Collapsed;
                        LoadingVisibility = Visibility.Visible;
                        Role role = await _roleRepository.GetRoleFromUser(Consts.UserName, Consts.ProjectId);
                        Project project = await _projectRepository.GetProject(Consts.ProjectId);
                        await SelectVisibility(role);

                        if (string.Equals(role.Name, "Admin"))
                        {
                            VisibilityMembers = Visibility.Visible;
                        }
                        if (project.AdminId == Consts.UserId)
                        {
                            DeleteProjectVisibility = Visibility.Visible;
                        }

                        ProjectDescription = project.Description;
                        TitleName = project.Name;
                        ListTasks = await GetRecordListBoxes();
                        RoleSourse = await _roleRepository.GetRoles();
                        await RefreshUsers();
                        CheckCountTasks(ListTasks);
                        PageVisibility = Visibility.Visible;
                        LoadingVisibility = Visibility.Collapsed;

                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex.ToString());
                        //ErrorHandler.Show(Application.Current.Resources["m_error_download"].ToString() + "\n" + ex.Message, _dialogIdentifier);
                    }
                });
            }
        }
        
        private async Task<List<RecordListBoxTasks>> GetRecordListBoxes()
        {
            List<Status> statuses = await _statusRepository.GetStatuses();
            List<Task> tasks = await _taskRepository.GetTasksFromProject(Consts.ProjectId);
            List<RecordListBoxTasks> records = new List<RecordListBoxTasks>();
            foreach (Task task in tasks)
            {
                Project project = await _projectRepository.GetProject(task.ProjectId);
                RecordListBoxTasks record = new RecordListBoxTasks()
                {
                    TaskId = task.Id,
                    UserId = task.UserId,
                    ProjectId = project.Id,
                    UserName = (await _userRepository.GetUser(null,task.UserId)).Login,
                    ProjectName = project.Name,
                    TaskName = task.Name,
                    FinishDate = task.EndDate.ToString(),
                    StartDate = task.BeginDate.ToString(),
                    Status = statuses.First(c => c.Id == task.StatusId).Name,
                    Foreground = CheckTime(task.EndDate)
                };
                records.Add(record);
            }
            CheckCountTasks(records);
            return records.OrderByDescending(c => c.Status).ThenByDescending(c => c.StartDate).ToList();
        }

        Brush CheckTime(DateTime dateTime)
        {
            return dateTime >= DateTime.Now ? new SolidColorBrush(Colors.DimGray) : new SolidColorBrush(Colors.DarkRed);
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
                        string taskName = SelectedTask.TaskName;
                        await _taskRepository.DeleteTask(SelectedTask.TaskId);

                        ListTasks = await FilterTasks();
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
                        Consts.TaskId = SelectedTask.TaskId;
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
                        Comments = (await _commentRepository.GetComment(SelectedTask.TaskId)).OrderByDescending(c => c.DateTime).ToList();
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
                ListTasks = await FilterTasks();
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
                        List<RecordListBoxTasks> filteredTasks = await FilterTasks();
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

        private async Task<List<RecordListBoxTasks>> FilterTasks()
        {
            if (Filter != null || Filter != "")
            {
                List<Status> statuses = await _statusRepository.GetStatuses();
                List<RecordListBoxTasks> filteredTasks = new List<RecordListBoxTasks>();
                List<Task> tasks = await _taskRepository.GetTasksFromProject(Consts.ProjectId);
                foreach (Task task in tasks)
                {
                    if (task.Name.Contains(Filter))
                    {
                        filteredTasks.Add(new RecordListBoxTasks()
                        {
                            TaskId = task.Id,
                            UserId = task.UserId,
                            ProjectId = task.ProjectId,
                            ProjectName = (await _projectRepository.GetProject(task.ProjectId)).Name,
                            UserName = (await _userRepository.GetUser(null, task.UserId)).Login,
                            TaskName = task.Name,
                            StartDate = task.BeginDate.ToString(),
                            FinishDate = task.EndDate.ToString(),
                            Status = statuses.First(c => c.Id == task.StatusId).Name,
                            Foreground = CheckTime(task.EndDate)
                        });
                    }
                }
                CheckCountTasks(filteredTasks);
                return filteredTasks.OrderByDescending(c => c.Status).ThenByDescending(c => c.StartDate).ToList();
            }
            else
            {
                return await GetRecordListBoxes();
            }
        }

        private void CheckCountTasks(List<RecordListBoxTasks> tasks)
        {
            try
            {
                if (tasks.Count == 0)
                {
                    TasksVisibility = Visibility.Collapsed;
                    NotTasksVisibility = Visibility.Visible;
                }
                else
                {
                    TasksVisibility = Visibility.Visible;
                    NotTasksVisibility = Visibility.Collapsed;
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
