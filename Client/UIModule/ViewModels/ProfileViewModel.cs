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
using Windows.UI.Xaml.Media;

namespace UIModule.ViewModels
{
    class ProfileViewModel : NavigateViewModel
    {
        public ProfileViewModel()
        {

        }

        IUserRepository _userRepository = new UserRepository();
        IProjectRepository _projectRepository = new ProjectRepository();
        IRoleRepository _roleRepository = new RoleRepository();
        ITaskRepository _taskRepository = new TaskRepository();
        IStatusRepository _statusRepository = new StatusRepository();

        private static Logger _logger = LogManager.GetCurrentClassLogger();

        #region Properties

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

        private Visibility _notTasksVisibility;
        public Visibility NotTasksVisibility
        {
            get { return _notTasksVisibility; }
            set
            {
                _notTasksVisibility = value;
                OnPropertyChanged();
            }
        }
        
        private Visibility _tasksVisibility;
        public Visibility TasksVisibility
        {
            get { return _tasksVisibility; }
            set
            {
                _tasksVisibility = value;
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

        private Brush _brush;
        public Brush Brush
        {
            get { return _brush; }
            set
            {
                _brush = value;
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

        #endregion

        #region Methods

        public ICommand SelectionChanged
        {
            get
            {
                return new DelegateCommand(async (obj) =>
                {
                    Consts.TaskId = SelectedTask.TaskId;
                    Consts.ProjectId = SelectedTask.ProjectId;
                    NavigationService.Instance.NavigateTo(typeof(Pages.Task));
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
                        Brush = new SolidColorBrush(Colors.Red);
                        UserName = Consts.UserName;
                        ListTasks = await GetRecordListBoxes();
                        PageVisibility = Visibility.Visible;
                        LoadingVisibility = Visibility.Collapsed;
                    }
                    catch (Exception ex)
                    {
                        _logger.Error(ex.ToString());
                    }
            });
            }
        }

        private async Task<List<RecordListBoxTasks>> GetRecordListBoxes()
        {
            List<Status> statuses = await _statusRepository.GetStatuses();
            List<SharedServicesModule.Models.Task> tasks = await _projectRepository.GetTasksFromUser((await _userRepository.GetUser(Consts.UserName)).Id);
            List<RecordListBoxTasks> records = new List<RecordListBoxTasks>();
            foreach (SharedServicesModule.Models.Task task in tasks)
            {
                Project project = await _projectRepository.GetProject(task.ProjectId);

                RecordListBoxTasks record = new RecordListBoxTasks()
                {
                    TaskId = task.Id,
                    ProjectId = project.Id,
                    ProjectName = project.Name,
                    TaskName = task.Name,
                    FinishDate = task.EndDate.ToString(),
                    Status = statuses.First(c => c.Id == task.StatusId).Name,
                    Foreground = CheckTime(task.EndDate)
                };
                records.Add(record);
            }
            CheckCountProjects(records);
            return records.OrderByDescending(c => c.Status).ToList();
        }

        Brush CheckTime(DateTime dateTime)
        {
            return dateTime >= DateTime.Now ? new SolidColorBrush(Colors.DimGray) : new SolidColorBrush(Colors.DarkRed);
        }

        private void CheckCountProjects(List<RecordListBoxTasks> recordListBoxes)
        {
            try
            {
                if (recordListBoxes.Count == 0)
                {
                    NotTasksVisibility = Visibility.Visible;
                    TasksVisibility = Visibility.Collapsed;
                }
                else
                {
                    NotTasksVisibility = Visibility.Collapsed;
                    TasksVisibility = Visibility.Visible;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.ToString());
            }
        }


        #endregion
    }
}

