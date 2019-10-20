using Newtonsoft.Json;
using SharedServicesModule.Models;
using SharedServicesModule.Services;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using Windows.Foundation;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace UIModule.Pages
{
    public sealed partial class Authorization : Page
    {
        public Authorization()
        {
            this.InitializeComponent();
            
            float x = 350;
            float y = 500;

            ApplicationView.GetForCurrentView().SetPreferredMinSize(new Size(200, 100));
            ApplicationView.PreferredLaunchViewSize = new Size(x, y);
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
        }
    }
}
