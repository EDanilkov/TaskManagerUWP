using BusinessLogicModule.Interfaces;
using NLog;
using SharedServicesModule.Models;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using UIModule.Utils;

namespace UIModule.ViewModels
{
    class ChangeTaskViewModel : NavigateViewModel
    {
        string _dialogIdentifier = "AddTaskDialog";
        private static Logger logger = LogManager.GetCurrentClassLogger();
        IUserRepository _userRepository = new UserRepository();
        ITaskRepository _taskRepository = new TaskRepository();
        IProjectRepository _projectRepository = new ProjectRepository();

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
                        List<User> users = await _userRepository.GetUsersFromProject(Consts.ProjectId);
                        Users = users;
                        Task task = (await _taskRepository.GetTask(Consts.TaskId));
                        TaskName = task.Name;
                        TaskDescription = task.Description;
                        TaskFinishDate = task.EndDate;
                        SelectedUser = (await _userRepository.GetUser(Consts.UserName));
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
                return new DelegateCommand(async (obj) =>
                {
                    try
                    {
                        NavigationService.Instance.NavigateTo(typeof(Pages.Task));
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex.ToString());
                        //ErrorHandler.Show(Application.Current.Resources["m_error_download"].ToString() + "\n" + ex.Message, _dialogIdentifier);
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
                            await _taskRepository.ChangeTask((await _taskRepository.GetTask(Consts.TaskId)), TaskName, TaskDescription, SelectedUser.Id, TaskFinishDate.UtcDateTime);
                            logger.Debug("user " + Consts.UserName + " added task " + TaskName + " to the project " + (await _projectRepository.GetProject(Consts.ProjectId)).Name);

                            NavigationService.Instance.NavigateTo(typeof(Pages.Project));
                        }
                        else
                        {
                            //ErrorHandler.Show(Application.Current.Resources["m_correct_entry"].ToString(), _dialogIdentifier);
                        }

                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex.ToString());
                        //ErrorHandler.Show(Application.Current.Resources["m_error_create_task"].ToString() + "\n" + ex.Message, _dialogIdentifier);
                    }
                });
            }
        }
        #endregion
    }
}
