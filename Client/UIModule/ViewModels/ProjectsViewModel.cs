using BusinessLogicModule.Interfaces;
using GalaSoft.MvvmLight.Command;
using SharedServicesModule.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml;

namespace UIModule.ViewModels
{
    public class ProjectsViewModel : NavigateViewModel
    {
        public ProjectsViewModel()
        {

        }

        private int _height;
        public int Height
        {
            get { return _height; }
            set
            {
                _height = value;
                OnPropertyChanged();
            }
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

        private bool _personal;
        public bool Personal
        {
            get { return _personal; }
            set
            {
                _personal = value;
                OnPropertyChanged();
            }
        }

        private string _chipRole;
        public string ChipRole
        {
            get { return _chipRole; }
            set
            {
                _chipRole = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Methods

        private ICommand _selectionChanged;
        public ICommand SelectionChanged
        {
            get
            {
                if (_selectionChanged == null)
                {
                    _selectionChanged = new RelayCommand(() =>
                    {
                        //System.Windows.Application.Current.Properties["ProjectId"] = SelectedProject.Id;
                        Navigate("Pages/Project.xaml");
                    });
                }
                return _selectionChanged;
            }
            set { _selectionChanged = value; }
        }

        public ICommand Loaded
        {
            get
            {
                return new DelegateCommand(async (obj) =>
                {
                    ProjectRepository _projectRepository = new ProjectRepository();
                    RoleRepository _roleRepository = new RoleRepository();
                    string userName = "2";
                    List<RecordListBox> recordListBoxes = new List<RecordListBox>();
                    List<Project> projects = await _projectRepository.GetProjectsFromUser(userName);
                    foreach (Project project in projects)
                    {
                        RecordListBox recordListBox = new RecordListBox() { Id = project.Id, ProjectName = project.Name, AdminId = project.AdminId, ChipRole = (await _roleRepository.GetRoleFromUser(userName, project.Id)).Name };
                        recordListBoxes.Add(recordListBox);
                    }
                    CheckCountProjects(recordListBoxes);
                    ListProjects = recordListBoxes;
                });
            }
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
            }
        }
        
        #endregion
    }
}
