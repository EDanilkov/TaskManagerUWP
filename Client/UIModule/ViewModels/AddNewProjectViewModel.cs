using BusinessLogicModule.Interfaces;
using NLog;
using SharedServicesModule;
using SharedServicesModule.Models;
using System;
using System.Windows.Input;
using UIModule.Utils;
using Windows.UI.Xaml;

namespace UIModule.ViewModels
{
    public class AddNewProjectViewModel : NavigateViewModel
    {
        IUserRepository _userRepository;
        IProjectRepository _projectRepository;
        private static Logger logger;

        public AddNewProjectViewModel(IUserRepository UserRepository, IProjectRepository ProjectRepository)
        {
            _userRepository = UserRepository;
            _projectRepository = ProjectRepository;
            logger = LogManager.GetCurrentClassLogger();
        }

        #region Properties

        private string _projectName;
        public string ProjectName
        {
            get { return _projectName; }
            set
            {
                _projectName = value;
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

        #endregion

        #region Methods

        public ICommand Back
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    try
                    {
                        NavigationService.Instance.NavigateTo(typeof(Pages.Projects));
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
                        if (ProjectName != null && ProjectDescription != null)
                        {
                            Project project = new Project()
                            {
                                Name = ProjectName,
                                Description = ProjectDescription,
                                AdminId = (await _userRepository.GetUser(Consts.UserName)).Id,
                                RegistrationDate = DateTime.Now
                            };
                            await _projectRepository.AddProject(project);
                            logger.Debug("user " + Consts.UserName + " added project " + ProjectName);

                            NavigationService.Instance.NavigateTo(typeof(Pages.Projects));
                        }
                        else
                        {
                            ErrorHandler.Show(Application.Current.Resources["m_correct_entry"].ToString());
                        }
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex.ToString());
                        ErrorHandler.Show(Application.Current.Resources["m_error_create_project"].ToString() + "\n" + ex.Message);
                    }
                });
            }
        }

        #endregion
    }
}
