using BusinessLogicModule.Interfaces;
using BusinessLogicModule.Services;
using NLog;
using SharedServicesModule;
using SharedServicesModule.Models;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using UIModule.Utils;
using Windows.UI.Xaml;

namespace UIModule.ViewModels
{
    public class AddNewTaskViewModel : NavigateViewModel
    {
        private static Logger logger;
        IUserRepository _userRepository;
        ITaskRepository _taskRepository;
        IProjectRepository _projectRepository;
        IStatusRepository _statusRepository;
        
        public AddNewTaskViewModel(IUserRepository UserRepository, ITaskRepository TaskRepository, IProjectRepository ProjectRepository, 
                                        IStatusRepository StatusRepository)
        {
            logger = LogManager.GetCurrentClassLogger();
            _userRepository = UserRepository;
            _taskRepository = TaskRepository;
            _projectRepository = ProjectRepository;
            _statusRepository = StatusRepository;
        }

        #region Properties

        private string _taskName;
        public string TaskName
        {
            get { return _taskName; }
            set
            {
                _taskName = value;
                OnPropertyChanged();
            }
        }

        private string _taskDescription;
        public string TaskDescription
        {
            get { return _taskDescription; }
            set
            {
                _taskDescription = value;
                OnPropertyChanged();
            }
        }

        private DateTimeOffset _taskFinishDate;
        public DateTimeOffset TaskFinishDate
        {
            get { return _taskFinishDate; }
            set
            {
                _taskFinishDate = value;
                OnPropertyChanged();
            }
        }

        private List<User> _users;
        public List<User> Users
        {
            get { return _users; }
            set
            {
                _users = value;
                OnPropertyChanged();
            }
        }

        private User _selectedUser;
        public User SelectedUser
        {
            get { return _selectedUser; }
            set
            {
                _selectedUser = value;
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
                        TaskFinishDate = DateTime.Now;
                        List<User> users = await _userRepository.GetUsersFromProject(Consts.ProjectId);
                        Users = users;
                        PageVisibility = Visibility.Visible;
                        LoadingVisibility = Visibility.Collapsed;
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex.ToString());
                        ErrorHandler.Show(Application.Current.Resources["m_error_download"].ToString() + "\n" + ex.Message);
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
                    try
                    {
                        NavigationService.Instance.NavigateTo(typeof(Pages.Project));
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex.ToString());
                        ErrorHandler.Show(Application.Current.Resources["m_error_download"].ToString() + "\n" + ex.Message);
                    }
                });
            }
        }

        public ICommand Accept
        {
            get
            {
                return new DelegateCommand(async (obj) =>
                {
                    try
                    {
                        if (TaskName != null && TaskDescription != null && SelectedUser != null && TaskFinishDate != null && TaskFinishDate >= DateTime.Today)
                        {
                           var task = new Task()
                            {
                                Name = TaskName,
                                Description = TaskDescription,
                                BeginDate = DateTime.Now,
                                ProjectId = Consts.ProjectId,
                                UserId = SelectedUser.Id,
                                StatusId = (await _statusRepository.GetStatus("Open")).Id,
                                EndDate = TaskFinishDate.UtcDateTime
                            };

                            await _taskRepository.AddTask(task);

                            Notification.ShowToastNotification(Application.Current.Resources["mSuccessAddNewTask"].ToString());
                            logger.Debug("user " + Consts.UserName + " added task " + TaskName + " to the project " + (await _projectRepository.GetProject(Consts.ProjectId)).Name);
                            NavigationService.Instance.NavigateTo(typeof(Pages.Project));
                        }
                        else
                        {
                            ErrorHandler.Show(Application.Current.Resources["m_correct_entry"].ToString());
                        }

                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex.ToString());
                        ErrorHandler.Show(Application.Current.Resources["m_error_create_task"].ToString() + "\n" + ex.Message);
                    }
                });
            }
        }
        #endregion
    }
}