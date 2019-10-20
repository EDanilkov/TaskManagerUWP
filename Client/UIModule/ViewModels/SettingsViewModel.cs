using System.Collections.Generic;
using System.Globalization;
using System.Windows.Input;
using UIModule.Utils;
using Windows.ApplicationModel.Resources.Core;

namespace UIModule.ViewModels
{
    class SettingsViewModel : NavigateViewModel
    {
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

        public ICommand Loaded
        {
            get
            {
                return new DelegateCommand(async (obj) =>
                {

                    foreach (var lang in App.Languages)
                    {
                        Languages.Add(lang);
                    }
                });
            }
        }
        

        public ICommand SelectedChangeLanguage
        {
            get
            {
                return new DelegateCommand(async (obj) =>
                {
                    App.Language = SelectedLanguage;
                    NavigationService.Instance.Navigate(typeof(Pages.MainPage));
                    NavigationService.Instance.NavigateTo(typeof(Pages.Settings));
                });
            }
        }

    }
}
