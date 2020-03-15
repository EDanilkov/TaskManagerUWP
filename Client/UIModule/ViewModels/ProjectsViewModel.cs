﻿using BusinessLogicModule.Interfaces;
using NLog;
using SharedServicesModule;
using SharedServicesModule.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using UIModule.Utils;
using Windows.UI.Xaml;

namespace UIModule.ViewModels
{
    public class ProjectsViewModel : NavigateViewModel
    {
        IUserRepository _userRepository;
        IProjectRepository _projectRepository;
        IRoleRepository _roleRepository;

        private static Logger _logger;

        public ProjectsViewModel(IUserRepository userRepository, IProjectRepository projectRepository, IRoleRepository roleRepository)
        {
            _userRepository = userRepository;
            _projectRepository = projectRepository;
            _roleRepository = roleRepository;

            _logger = LogManager.GetCurrentClassLogger();
        }

        #region Properties

        private List<RecordListBox> _listProjects = new List<RecordListBox>();
        public List<RecordListBox> ListProjects
        {
            get { return _listProjects; }
            set
            {
                _listProjects = value;
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
        
        private RecordListBox _selectedProject;
        public RecordListBox SelectedProject
        {
            get { return _selectedProject; }
            set
            {
                _selectedProject = value;
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
                    Consts.ProjectId = SelectedProject.Id;
                    NavigationService.Instance.NavigateTo(typeof(Pages.Project));
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
                        ListProjects = await GetRecordListBoxes();
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

        private async Task<List<RecordListBox>> GetRecordListBoxes()
        {
            var recordListBoxes = new List<RecordListBox>();
            var projects = await _projectRepository.GetProjectsFromUser(Consts.UserName);
            foreach (Project project in projects)
            {
                RecordListBox recordListBox = new RecordListBox() { Id = project.Id, ProjectName = project.Name, AdminId = project.AdminId, ChipRole = (await _roleRepository.GetRoleFromUser(Consts.UserName, project.Id)).Name };
                recordListBoxes.Add(recordListBox);
            }
            CheckCountProjects(recordListBoxes);
            return recordListBoxes;
        }
        
        private void CheckCountProjects(List<RecordListBox> recordListBoxes)
        {
            try
            {
                if (recordListBoxes.Count == 0)
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
                _logger.Error(ex.ToString());
            }
        }

        public ICommand NewProjectClick => new DelegateCommand(NewProject);

        private void NewProject(object o)
        {
            try
            {
                NavigationService.Instance.NavigateTo(typeof(Pages.AddNewProject));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.ToString());
            }
        }
        #endregion
    }
}
