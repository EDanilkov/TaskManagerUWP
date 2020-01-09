using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace UIModule.Utils
{
    public static class ErrorHandler
    {
        public static async void Show(string error)
        {
            ContentDialog deleteFileDialog = new ContentDialog()
            {
                Title = Application.Current.Resources["MError"].ToString(),
                Content = error,
                PrimaryButtonText = "ОK"
            };
            ContentDialogResult result = await deleteFileDialog.ShowAsync();
        }
    }
}
