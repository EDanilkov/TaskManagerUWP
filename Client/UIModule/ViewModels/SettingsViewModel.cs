using NLog;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Input;
using UIModule.Utils;
using Windows.UI.Xaml;

namespace UIModule.ViewModels
{
    public class SettingsViewModel : NavigateViewModel
    {
        private static Logger logger;

        public SettingsViewModel()
        {
            logger = LogManager.GetCurrentClassLogger();
        }

        #region Properties

        private List<CultureInfo> _languages = new List<CultureInfo>();
        public List<CultureInfo> Languages
        {
            get { return _languages; }
            set
            {
                _languages = value;
                OnPropertyChanged();
            }
        }

        private CultureInfo _selectedLanguage;
        public CultureInfo SelectedLanguage
        {
            get { return _selectedLanguage; }
            set
            {
                _selectedLanguage = value;
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
                return new DelegateCommand((obj) =>
                {
                    try
                    {
                        PageVisibility = Visibility.Collapsed;
                        LoadingVisibility = Visibility.Visible;
                        foreach (var lang in App.Languages)
                        {
                            Languages.Add(lang);
                        }
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

        public ICommand SelectedChangeLanguage
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    App.Language = SelectedLanguage;
                    NavigationService.Instance.Navigate(typeof(Pages.MainPage));
                    NavigationService.Instance.NavigateTo(typeof(Pages.Settings));
                });
            }
        }

        #endregion
    }
}
