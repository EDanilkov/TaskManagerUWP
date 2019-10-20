using SharedServicesModule.Models;
using System;
using UIModule.ViewModels;
using Windows.ApplicationModel.Core;
using Windows.ApplicationModel.Resources;
using Windows.ApplicationModel.Resources.Core;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace UIModule.Pages
{
    public sealed partial class MainPage : Page
    {
        public Frame NavigationFrame => myFrame;

        public MainPage()
        {
            float x = 2160;
            float y = 600;
            ApplicationView.GetForCurrentView().SetPreferredMinSize(new Size(360, 470));
            ApplicationView.PreferredLaunchViewSize = new Size(x, y);
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;

            InitializeComponent();
            CoreApplication.GetCurrentView().TitleBar.ExtendViewIntoTitleBar = false;
            // по умолчанию открываем страницу home.xaml
            
            myFrame.Navigate(typeof(Home));
        }
    }

}
