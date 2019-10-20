using System.Collections.Generic;
using System.Windows.Input;
using UIModule.Utils;

namespace UIModule.ViewModels
{
    class AuthorizationViewModel : NavigateViewModel
    {

        private string _textError;
        public string TextError
        {
            get { return _textError; }
            set
            {
                _textError = value;
                OnPropertyChanged();
            }
        }

        private string _login;
        public string Login
        {
            get { return _login; }
            set
            {
                _login = value;
                OnPropertyChanged();
            }
        }

        public ICommand Enter
        {
            get
            {
                return new DelegateCommand(async (obj) =>
                {
                    NavigationService.Instance.Navigate(typeof(Pages.MainPage));
                });
            }
        }

        public ICommand Registration
        {
            get
            {
                return new DelegateCommand(async (obj) =>
                {
                    NavigationService.Instance.Navigate(typeof(Pages.MainPage));
                });
            }
        }
        
    }
}
