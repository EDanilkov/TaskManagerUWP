using SharedServicesModule;
using System.Windows.Input;
using UIModule.Pages;
using UIModule.Utils;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace UIModule.ViewModels
{
    public class MainPageViewModel : NavigateViewModel
    {
        public MainPageViewModel()
        {

        }

        #region Properties

        private int _selectedPage;
        public int SelectedPage
        {
            get { return _selectedPage; }
            set
            {
                _selectedPage = value;
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

        private Visibility _frameVisibility;
        public Visibility FrameVisibility
        {
            get { return _frameVisibility; }
            set
            {
                _frameVisibility = value;
                OnPropertyChanged();
            }
        }

        private double _frameOpacity;
        public double FrameOpacity
        {
            get { return _frameOpacity; }
            set
            {
                _frameOpacity = value;
                OnPropertyChanged();
            }
        }

        private SplitViewDisplayMode _displayMode;
        public SplitViewDisplayMode DisplayMode
        {
            get { return _displayMode; }
            set
            {
                _displayMode = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Methods

        public ICommand PageChanged
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    switch (SelectedPage)
                    {
                        case 0:
                            NavigationService.Instance.NavigateTo(typeof(Profile));
                            break;
                        
                        case 1:
                            NavigationService.Instance.NavigateTo(typeof(Projects));
                            break;
                        
                        case 2:
                            NavigationService.Instance.NavigateTo(typeof(Settings));
                            break;

                        case 3:
                            NavigationService.Instance.Navigate(typeof(Authorization));
                            break;
                    }
                    SelectedPage = -1;
                    IsPaneOpen = Consts.Width > 720 ? true : false;
                });
            }
        }
        
        public ICommand Loaded
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    FrameVisibility = Visibility.Visible;
                    SelectedPage = -1;
                    FrameOpacity = 1;
                    DisplayMode = Consts.Width > 720 ? SplitViewDisplayMode.CompactInline : SplitViewDisplayMode.CompactOverlay;
                    PaneVisibility = Consts.Width > 720 ? Visibility.Visible : Visibility.Collapsed;
                    IsPaneOpen = Consts.Width > 720 ? true : false;
                    NavigationService.Instance.NavigateTo(typeof(Projects));
                });
            }
        }

        public ICommand HamburgerButtonClick
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    IsPaneOpen = !IsPaneOpen;
                    PaneVisibility = Visibility.Visible;
                    InactiveAreaVisibility = Visibility.Visible;
                    FrameOpacity = 0.1;
                });
            }
        }
        
        public ICommand PaneClosing
        {
            get
            {
                return new DelegateCommand((obj) =>
                {
                    PaneVisibility = Visibility.Collapsed;
                    IsPaneOpen = false;
                    InactiveAreaVisibility = Visibility.Collapsed;
                    FrameOpacity = 1;
                });
            }
        }
        #endregion
    }
}
