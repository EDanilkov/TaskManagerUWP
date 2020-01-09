using Windows.Foundation;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml.Controls;

namespace UIModule.Pages
{
    public sealed partial class Authorization : Page
    {
        public Authorization()
        {
            this.InitializeComponent();

            ApplicationView.GetForCurrentView().SetPreferredMinSize(new Size(200, 100));
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
        }
    }
}
