using BusinessLogicModule.Interfaces;
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

namespace UIModule.ViewModels
{
    class AddNewProjectViewModel : NavigateViewModel
    {
        IUserRepository _userRepository;
        IProjectRepository _projectRepository;
        private static Logger logger;

        public AddNewProjectViewModel()
        {
            _userRepository = new UserRepository();
            _projectRepository = new ProjectRepository();
            logger = LogManager.GetCurrentClassLogger();
        }

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

        public ICommand Back
        {
            get
            {
                return new DelegateCommand(async (obj) =>
                {
                    try
                    {
                        NavigationService.Instance.NavigateTo(typeof(Pages.Project));
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
                            //ErrorHandler.Show(Application.Current.Resources["m_correct_entry"].ToString(), _dialogIdentifier);
                        }
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex.ToString());
                        //ErrorHandler.Show(Application.Current.Resources["m_error_create_project"].ToString() + "\n" + ex.Message, _dialogIdentifier);
                    }
                });
            }
        }
    }
}
