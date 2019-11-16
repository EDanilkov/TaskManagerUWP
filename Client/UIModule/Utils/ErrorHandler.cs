using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace UIModule.Utils
{
    public static class ErrorHandler
    {
        public static async void Show(string error)
        {
            ContentDialog deleteFileDialog = new ContentDialog()
            {
                Title = "Ошибка",
                Content = error,
                PrimaryButtonText = "ОК"
            };

            ContentDialogResult result = await deleteFileDialog.ShowAsync();
            
        }
    }
}
