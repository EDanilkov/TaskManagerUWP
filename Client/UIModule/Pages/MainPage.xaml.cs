using SharedServicesModule.Models;
using SharedServicesModule.Services;
using System;
using Windows.ApplicationModel.Resources;
using Windows.ApplicationModel.Resources.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;


namespace UIModule.Pages
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();

            // по умолчанию открываем страницу home.xaml
            myFrame.Navigate(typeof(Home));
            TitleTextBlock.Text = "Главная";
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (home.IsSelected)
            {
                myFrame.Navigate(typeof(Home));
                TitleTextBlock.Text = "Главная";
            }
            else if (share.IsSelected)
            {
                myFrame.Navigate(typeof(Home));
                TitleTextBlock.Text = "Поделиться";
            }
            else if (settings.IsSelected)
            {
                myFrame.Navigate(typeof(Settings));
                TitleTextBlock.Text = "Настройки";
            }
        }

        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            mySplitView.IsPaneOpen = !mySplitView.IsPaneOpen;
        }
    }

}
