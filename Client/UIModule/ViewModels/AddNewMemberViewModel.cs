using BusinessLogicModule.Interfaces;
using BusinessLogicModule.Services;
using NLog;
using SharedServicesModule;
using SharedServicesModule.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using UIModule.Utils;
using Windows.UI.Xaml;

namespace UIModule.ViewModels
{
    public class AddNewMemberViewModel : NavigateViewModel
    {
        IUserRepository _userRepository;
        IProjectRepository _projectRepository;
        IUserProjectRepository _userProjectRepository;
        IRoleRepository _roleRepository;

        private static Logger logger;

        public AddNewMemberViewModel(IUserRepository UserRepository, IProjectRepository ProjectRepository,
                                        IUserProjectRepository UserProjectRepository, IRoleRepository RoleRepository)
        {
            _userRepository = UserRepository;
            _projectRepository = ProjectRepository;
            _userProjectRepository = UserProjectRepository;
            _roleRepository = RoleRepository;

            logger = LogManager.GetCurrentClassLogger();
        }

        #region Properties

        private Role _selectedRole;
        public Role SelectedRole
        {
            get { return _selectedRole; }
            set
            {
                _selectedRole = value;
                OnPropertyChanged();
            }
        }

        private string _comboBoxNewMembersText;
        public string ComboBoxNewMembersText
        {
            get { return _comboBoxNewMembersText; }
            set
            {
                _comboBoxNewMembersText = value;
                OnPropertyChanged();
            }
        }

        private bool _comboBoxNewMemberIsDropDownOpen;
        public bool ComboBoxNewMemberIsDropDownOpen
        {
            get { return _comboBoxNewMemberIsDropDownOpen; }
            set
            {
                _comboBoxNewMemberIsDropDownOpen = value;
                OnPropertyChanged();
            }
        }

        private User _selectedNewMember;
        public User SelectedNewMember
        {
            get { return _selectedNewMember; }
            set
            {
                _selectedNewMember = value;
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

        private List<User> _listNewDevelopers = new List<User>();
        public List<User> ListNewDevelopers
        {
            get { return _listNewDevelopers; }
            set
            {
                _listNewDevelopers = value;
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

        public ICommand ComboBoxNewMembersChanged
        {
            get
            {
                return new DelegateCommand(async (obj) =>
                {
                    try
                    {
                        var users = (await _userRepository.GetUsers());
                        var usersInProject = (await _userRepository.GetUsersFromProject(Consts.ProjectId));
                        var userInOtherProject = new List<User>();
                        int number = 0;
                        int countUsersInProject = usersInProject.Count;
                        foreach (User user in users)
                        {
                            number = 0;
                            foreach (User userProject1 in usersInProject)
                            {
                                if (!string.Equals(user.Login, userProject1.Login))
                                {
                                    number++;
                                }
                            }
                            if (number == countUsersInProject)
                            {
                                userInOtherProject.Add(user);
                            }
                        }
                        ComboBoxNewMemberIsDropDownOpen = true;
                        NewMembersSourse = userInOtherProject.Where(p => p.Login.Contains(ComboBoxNewMembersText)).ToList();
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
                        if (SelectedNewMember != null && SelectedRole != null)
                        {
                            Project project = await _projectRepository.GetProject(Consts.ProjectId);
                            var userProject = new UserProject() { UserId = SelectedNewMember.Id, ProjectId = project.Id, RoleId = SelectedRole.Id };
                            await _userProjectRepository.AddUserProject(userProject);

                            Notification.ShowToastNotification(Application.Current.Resources["mSuccessAddUserToProject"].ToString());
                            logger.Debug("user " + Consts.UserName + " added user " + SelectedNewMember.Login + " to the project " + project.Name + " with the " + SelectedRole.Name + " role");
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
                        ErrorHandler.Show(Application.Current.Resources["m_error_add_user"].ToString() + "\n" + ex.Message);
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
                        RoleSourse = await _roleRepository.GetRoles();
                        await RefreshUsers();
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

        public async System.Threading.Tasks.Task RefreshUsers()
        {
            var users = (await _userRepository.GetUsers());
            var usersInProject = (await _userRepository.GetUsersFromProject(Consts.ProjectId));
            var userInOtherProject = new List<User>();
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
            ListNewDevelopers = userInOtherProject;
            NewMembersSourse = userInOtherProject;
            ListMembers = await _userRepository.GetUsersFromProject(Consts.ProjectId);
        }
        #endregion
    }
}
