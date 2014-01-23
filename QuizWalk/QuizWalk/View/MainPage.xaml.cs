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

namespace QuizWalk
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private UserData data;

        public MainPage()
        {
            this.InitializeComponent();
        }

        private void Quiz1_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if(this.Frame != null)
            {
                this.Frame.Navigate(typeof(MapPage));
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            RestoreAsync();
        }

        private async void RestoreAsync()
        {
            StorageFile file = await ApplicationData.Current.LocalFolder.GetFileAsync("UserDetails.xml");
            if (file == null)
                return;

            using(var fs = await file.OpenAsync(FileAccessMode.ReadWrite))
            {
                var inStream = fs.GetInputStreamAt(0);
                DataContractSerializer serializer = new DataContractSerializer(typeof(UserData));
                data = (UserData)serializer.ReadObject(inStream.AsStreamForRead());
                inStream.Dispose();
                fs.Dispose();
            }
            welcomeText.Text = "Choose your quiz, " + data.Name;
        }
    }
}
