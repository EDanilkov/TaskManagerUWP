using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using SharedServicesModule.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace UIModule.ViewModels
{
    public class NavigateViewModel : ViewModelBase, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public NavigateViewModel()
        {

        }

        public string Title { get; set; }
        public void Navigate(string url)
        {
            Messenger.Default.Send<NavigateArgs>(new NavigateArgs(url));
        }
    }
}
