﻿using BusinessLogicModule.Interfaces;
using NLog;
using SharedServicesModule;
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
    public class ProfileViewModel : NavigateViewModel
    {
        IUserRepository _userRepository;
        IProjectRepository _projectRepository;
        IRoleRepository _roleRepository;
        ITaskRepository _taskRepository;
        IStatusRepository _statusRepository;

        private static Logger _logger;

        public ProfileViewModel(IUserRepository UserRepository, IProjectRepository ProjectRepository, IRoleRepository RoleRepository,
                                    ITaskRepository TaskRepository, IStatusRepository StatusRepository)
        {
            _userRepository = UserRepository;
            _projectRepository = ProjectRepository;
            _roleRepository = RoleRepository;
            _taskRepository = TaskRepository;
            _statusRepository = StatusRepository;
            _logger = LogManager.GetCurrentClassLogger();
        }

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

        private List<RecordListBoxTasks> _listOpenTasks = new List<RecordListBoxTasks>();
        public List<RecordListBoxTasks> ListOpenTasks
        {
            get { return _listOpenTasks; }
            set
            {
                _listOpenTasks = value;
                OnPropertyChanged();
            }
        }

        private List<RecordListBoxTasks> _listClosedTasks = new List<RecordListBoxTasks>();
        public List<RecordListBoxTasks> ListClosedTasks
        {
            get { return _listClosedTasks; }
            set
            {
                _listClosedTasks = value;
                OnPropertyChanged();
            }
        }

        private string _openTasks;
        public string OpenTasks
        {
            get { return _openTasks; }
            set
            {
                _openTasks = value;
                OnPropertyChanged();
            }
        }

        private string _closedTasks;
        public string ClosedTasks
        {
            get { return _closedTasks; }
            set
            {
                _closedTasks = value;
                OnPropertyChanged();
            }
        }

        private string _allTasks;
        public string AllTasks
        {
            get { return _allTasks; }
            set
            {
                _allTasks = value;
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
                return new DelegateCommand((obj) =>
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
                        ListOpenTasks = ListTasks.Where(c => string.Equals(c.Status, "Open")).ToList();
                        ListClosedTasks = ListTasks.Where(c => string.Equals(c.Status, "Closed")).ToList();
                        AllTasks = Application.Current.Resources["mAll"].ToString() + "(" + ListTasks.Count + ")";
                        OpenTasks = Application.Current.Resources["mOpen"].ToString() + "(" + ListOpenTasks.Count + ")";
                        ClosedTasks = Application.Current.Resources["mClosed"].ToString() + "(" + ListClosedTasks.Count + ")";
                        PageVisibility = Visibility.Visible;
                        LoadingVisibility = Visibility.Collapsed;
                    }
                    catch (Exception ex)
                    {
                        _logger.Error(ex.ToString());
                        ErrorHandler.Show(Application.Current.Resources["m_error_download"].ToString() + "\n" + ex.Message);
                    }
            });
            }
        }

        private async Task<List<RecordListBoxTasks>> GetRecordListBoxes()
        {
            var statuses = await _statusRepository.GetStatuses();
            var tasks = await _projectRepository.GetTasksFromUser((await _userRepository.GetUser(Consts.UserName)).Id);
            var records = new List<RecordListBoxTasks>();
            foreach (SharedServicesModule.Models.Task task in tasks)
            {
                Project project = await _projectRepository.GetProject(task.ProjectId);

                var record = new RecordListBoxTasks()
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

