﻿using BusinessLogicModule.Interfaces;
using GalaSoft.MvvmLight.Command;
using NLog;
using SharedServicesModule.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using UIModule.Utils;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace UIModule.ViewModels
{
    public class ProjectsViewModel : NavigateViewModel
    {
        public ProjectsViewModel()
        {

        }

        IUserRepository _userRepository = new UserRepository();
        IProjectRepository _projectRepository = new ProjectRepository();
        IRoleRepository _roleRepository = new RoleRepository();
        
        private static Logger _logger = LogManager.GetCurrentClassLogger();

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

        private bool _isPaneOpen = true;
        public bool IsPaneOpen
        {
            get { return _isPaneOpen; }
            set
            {
                _isPaneOpen = value;
                OnPropertyChanged();
            }
        }

        private Visibility _paneVisibility = Visibility.Visible;
        public Visibility PaneVisibility
        {
            get { return _paneVisibility; }
            set
            {
                _paneVisibility = value;
                OnPropertyChanged();
            }
        }

        private Visibility _inactiveAreaVisibility = Visibility.Collapsed;
        public Visibility InactiveAreaVisibility
        {
            get { return _inactiveAreaVisibility; }
            set
            {
                _inactiveAreaVisibility = value;
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

        #endregion

        #region Methods

        public ICommand SelectionChanged
        {
            get
            {
                return new DelegateCommand(async (obj) =>
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

                    /*List<Project> projects =  new List<Project>();
                    projects.Add(new Project() { Name = "1" });
                    projects.Add(new Project() { Name = "2" });
                    projects.Add(new Project() { Name = "3" });*/
                    ListProjects = await GetRecordListBoxes();
                });
            }
        }


        private async Task<List<RecordListBox>> GetRecordListBoxes()
        {
            List<RecordListBox> recordListBoxes = new List<RecordListBox>();
            List<Project> projects = await _projectRepository.GetProjectsFromUser(Consts.UserName);
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

        private async void NewProject(object o)
        {
            try
            {
                NavigationService.Instance.NavigateTo(typeof(Pages.AddNewProject));
            }
            catch (Exception ex)
            {
                //logger.Error(ex.ToString());
            }
        }
        #endregion
    }
}
