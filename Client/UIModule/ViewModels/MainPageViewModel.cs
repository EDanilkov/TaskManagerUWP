using System;
using System.Windows.Input;
using UIModule.Pages;
using UIModule.Utils;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace UIModule.ViewModels
{
    class MainPageViewModel : NavigateViewModel
    {
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

        public ICommand PageChanged
        {
            get
            {
                return new DelegateCommand(async (obj) =>
                {
                    switch (SelectedPage)
                    {
                        case 0:
                            NavigationService.Instance.NavigateTo(typeof(Authorization));
                            SelectedPage = -1;
                            IsPaneOpen = Consts.Width > 720 ? true : false;
                            break;
                        
                        case 1:
                            NavigationService.Instance.NavigateTo(typeof(Projects));
                            SelectedPage = -1;
                            IsPaneOpen = Consts.Width > 720 ? true : false;
                            break;
                        
                        case 2:
                            NavigationService.Instance.NavigateTo(typeof(Settings));
                            SelectedPage = -1;
                            IsPaneOpen = Consts.Width > 720 ? true : false;
                            break;

                        case 3:
                            NavigationService.Instance.Navigate(typeof(Authorization));
                            SelectedPage = -1;
                            break;

                    }
                });
            }
        }


        //В ЭТОМ МЕТОДЕ ПОЛНАЯ ЖЕСТЬ, РЕШИТЬ ВОПРОС С МОБИЛЬНОЙ И ДЕСКТОПНОЙ ВЕРСИЕЙ
        public ICommand Loaded
        {
            get
            {
                return new DelegateCommand(async (obj) =>
                {
                    /*DisplayMode = SplitViewDisplayMode.CompactOverlay;
                    PaneVisibility = Visibility.Visible;
                    IsPaneOpen = true ;*/
                    SelectedPage = -1;
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
                return new DelegateCommand(async (obj) =>
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
                return new DelegateCommand(async (obj) =>
                {
                    PaneVisibility = Visibility.Collapsed;
                    IsPaneOpen = false;
                    InactiveAreaVisibility = Visibility.Collapsed;
                    FrameOpacity = 1;
                });
            }
        }
        
    }
}
