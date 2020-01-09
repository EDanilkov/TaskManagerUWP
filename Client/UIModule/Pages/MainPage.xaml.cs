using SharedServicesModule;
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
            InitializeComponent();
        }

        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Consts.Width = ActualWidth;
            Consts.Height = ActualHeight;
        }
    }
}
