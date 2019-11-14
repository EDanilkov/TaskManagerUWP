using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using UIModule.Pages;
using UIModule.Utils;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Resources.Core;
using Windows.Globalization;
using Windows.System.UserProfile;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace UIModule
{
    sealed partial class App : Application
    {

        private static CultureInfo selectedCultureInfo = new CultureInfo("en-US");

        private static List<CultureInfo> m_Languages = new List<CultureInfo>();

        public static List<CultureInfo> Languages
        {
            get
            {
                return m_Languages;
            }
        }

        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;


            App.LanguageChanged += App_LanguageChanged;

            m_Languages.Clear();
            m_Languages.Add(new CultureInfo("en-US")); //Нейтральная культура для этого проекта
            m_Languages.Add(new CultureInfo("ru-RU"));
        }
        
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            SystemNavigationManager.GetForCurrentView().BackRequested += App_BackRequested; //Button Back

            ApplicationLanguages.PrimaryLanguageOverride = GlobalizationPreferences.Languages[0];

            Frame rootFrame = Window.Current.Content as Frame;

            if (rootFrame == null)
            {
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Load state from previously suspended application
                }
                
                Window.Current.Content = rootFrame;
            }

            if (e.PrelaunchActivated == false)
            {
                if (rootFrame.Content == null)
                {
                    rootFrame.Navigate(typeof(Authorization), e.Arguments);
                }
                Window.Current.Activate();
            }
        }

        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }
        
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            deferral.Complete();
        }
        
        //Евент для оповещения всех окон приложения
        public static event EventHandler LanguageChanged;

        public static CultureInfo Language
        {
            get
            {
                return selectedCultureInfo;
            }
            set
            {
                if (value == null) throw new ArgumentNullException("value");
                if (value == selectedCultureInfo) return;

                //Меняем язык приложения:
                selectedCultureInfo = value;

                //Создаём ResourceDictionary для новой культуры
                ResourceDictionary dict = new ResourceDictionary();
                switch (value.Name)
                {
                    case "ru-RU":
                        dict.Source = new Uri("ms-appx:Resources/lang.ru-RU.xaml", UriKind.Absolute);
                        break;
                    default:
                        dict.Source = new Uri("ms-appx:Resources/lang.xaml", UriKind.Absolute);
                        break;
                }
                Application.Current.Resources.MergedDictionaries.Add(dict);

                //Вызываем евент для оповещения всех окон.
                LanguageChanged(Application.Current, new EventArgs());
            }
        }

        private void App_LanguageChanged(Object sender, EventArgs e)
        {
            selectedCultureInfo = Language;
            
        }

        private void App_BackRequested(object sender, BackRequestedEventArgs e)
        {
            if (!e.Handled)
            {
                NavigationService.Instance.NavigateBack();
                e.Handled = true;
            }
        }
    }
}
