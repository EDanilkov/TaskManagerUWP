using BusinessLogicModule.Interfaces;
using BusinessLogicModule.Repositories;
using BusinessLogicModule.Services;
using NLog;
using SharedServicesModule;
using SharedServicesModule.Models;
using SharedServicesModule.Services;
using System;
using System.Linq;
using System.Windows.Input;
using UIModule.Pages;
using UIModule.Utils;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace UIModule.ViewModels
{
    public class AuthorizationViewModel : NavigateViewModel
    {
        IUserRepository _userRepository;
        private static Logger _logger;

        public AuthorizationViewModel(IUserRepository UserRepository)
        {
            _userRepository = new UserRepository();
            _logger = LogManager.GetCurrentClassLogger();
        }

        #region Properties

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

        private Brush _colorError;
        public Brush ColorError
        {
            get { return _colorError; }
            set
            {
                _colorError = value;
                OnPropertyChanged();
            }
        }


        private Visibility _visibilityError = Visibility.Collapsed;
        public Visibility VisibilityError
        {
            get { return _visibilityError; }
            set
            {
                _visibilityError = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Methods

        private void ShowError(string textError)
        {
            TextError = textError;
            VisibilityError = Visibility.Visible;
        }

        public ICommand Enter
        {
            get
            {
                return new DelegateCommand(async (obj) =>
                {
                    try
                    {
                        var passwordBox = obj as PasswordBox;
                        var password = passwordBox.Password;
                        if (!string.Equals(password, "") && !string.Equals(Login, ""))
                        {
                            var user = new User()
                            {
                                Login = Login.ToString(),
                                Password = password
                            };
                            await TokenService.GetToken(user);

                            Consts.UserName = Login;
                            Consts.UserId = (await _userRepository.GetUser(Login)).Id;

                            Notification.ShowToastNotification(Application.Current.Resources["mWelcome"].ToString() + ", " + Login);
                            _logger.Debug("The user " + user.Login + " is logged in to the app");
                            NavigationService.Instance.Navigate(typeof(MainPage));
                        }
                        else
                        {
                            ShowError(Application.Current.Resources["m_error_enter_all_fields"].ToString());
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.Error(ex.ToString());
                        ShowError(Application.Current.Resources["m_error_enter"].ToString() + "\n" + ex.Message);
                    }
                });
            }
        }

        public ICommand Registration
        {
            get
            {
                return new DelegateCommand(async (obj) =>
                {
                    try
                    {
                        var passwordBox = obj as PasswordBox;
                        var password = passwordBox.Password;

                        if (!string.Equals(password, "") && !string.Equals(Login, ""))
                        {
                            if ((await _userRepository.GetUsers()).Where(c => string.Equals(c.Login, Login)).ToList().Count == 0)
                            {
                                User user = new User() { Login = Login, Password = password, RegistrationDate = DateTime.Now };
                                await _userRepository.AddUser(user);
                                ShowError(Application.Current.Resources["m_success_registered"].ToString());
                                _logger.Debug("The user " + user.Login + " is registered");
                            }
                            else
                            {
                                ShowError(Application.Current.Resources["m_error_bad_login"].ToString());
                            }
                        }
                        else
                        {
                            ShowError(Application.Current.Resources["m_error_enter_all_fields"].ToString());
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.Error(ex.ToString());
                        ShowError(Application.Current.Resources["m_error_add_user"].ToString());
                    }
                });
            }
        }
        #endregion

    }
}
