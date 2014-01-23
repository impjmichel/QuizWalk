using QuizWalk.Model1;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Serialization;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace QuizWalk.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoginPage : Page
    {
        private UserData data;

        public LoginPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            RestoreAsync();
        }

        private async void RestoreAsync()
        {
            try
            {
                StorageFile file = await ApplicationData.Current.LocalFolder.GetFileAsync("UserDetails.xml");
                if (file == null)
                    return;
                if (this.Frame != null)
                {
                    this.Frame.Navigate(typeof(MainPage));
                }
            }
            catch (FileNotFoundException) { }
        }

        private void Login_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (!NameTextBox.Text.Equals(""))
            {
                data = new UserData(NameTextBox.Text);
                SaveAsync();
                if (this.Frame != null)
                {
                    this.Frame.Navigate(typeof(MainPage));
                }
            }
            else
            {
                noNameError.Text = "Please enter a name :)";
            }
        }

        private async void SaveAsync()
        {
            StorageFile userdetailsfile = await ApplicationData.Current.LocalFolder.CreateFileAsync("UserDetails.xml", CreationCollisionOption.ReplaceExisting);
            IRandomAccessStream raStream = await userdetailsfile.OpenAsync(FileAccessMode.ReadWrite);
            using (IOutputStream outStream = raStream.GetOutputStreamAt(0))
            {
                DataContractSerializer serializer = new DataContractSerializer(typeof(UserData));
                serializer.WriteObject(outStream.AsStreamForWrite(), data);
                await outStream.FlushAsync();
                outStream.Dispose();
            }
            raStream.Dispose();
        }
    }
}
