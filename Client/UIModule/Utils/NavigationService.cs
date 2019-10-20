using System;
using UIModule.Pages;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace UIModule.Utils
{
    sealed class NavigationService
    {
        public void Navigate(Type sourcePage)
        {
            var frame = (Frame)Window.Current.Content;
            frame.Navigate(sourcePage);
        }

        public void NavigateTo(Type viewType)
        {
            var rootFrame = Window.Current.Content as Frame;
            var homePage = rootFrame.Content as MainPage;
            homePage.NavigationFrame.Navigate(viewType);
        }
        
        public void NavigateBack()
        {
            var rootFrame = Window.Current.Content as Frame;
            var homePage = rootFrame.Content as MainPage;
            homePage.NavigationFrame.GoBack();
        }

        public void Navigate(Type sourcePage, object parameter)
        {
            var frame = (Frame)Window.Current.Content;
            frame.Navigate(sourcePage, parameter);
        }

        public void GoBack()
        {
            var frame = (Frame)Window.Current.Content;
            frame.GoBack();
        }

        private NavigationService() { }

        private static readonly Lazy<NavigationService> instance =
            new Lazy<NavigationService>(() => new NavigationService());

        public static NavigationService Instance => instance.Value;
    }
}
