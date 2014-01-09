using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace QuizWalk
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MapPage : Page
    {
        private static MapPage instance;

        public MapPage()
        {
            this.InitializeComponent();
        }

        public static MapPage getInstance()
        {
            return instance != null ? instance : (instance = new MapPage());
        }

        /// <summary>
        /// Method to set the image in the columns
        /// </summary>
        /// <param name="column"></param>
        ///     int must be between 0 (inclusive) and 15 (inclusive).
        /// <param name="letter"></param>
        ///     any 1 char letter or 'empty' is allowed.
        public void changeLetter(int column, string letter)
        {
            column++;
            switch(column)
            {
                case 1:
                    i1st.Source = new BitmapImage(new Uri(this.BaseUri, "ms-appx:/Assets/images/Letters/" + letter + ".png"));
                    break;
                case 2:
                    i2nd.Source = new BitmapImage(new Uri(this.BaseUri, "ms-appx:/Assets/images/Letters/" + letter + ".png"));
                    break;
                case 3:
                    i3rd.Source = new BitmapImage(new Uri(this.BaseUri, "ms-appx:/Assets/images/Letters/" + letter + ".png"));
                    break;
                case 4:
                    i4th.Source = new BitmapImage(new Uri(this.BaseUri, "ms-appx:/Assets/images/Letters/" + letter + ".png"));
                    break;
                case 5:
                    i5th.Source = new BitmapImage(new Uri(this.BaseUri, "ms-appx:/Assets/images/Letters/" + letter + ".png"));
                    break;
                case 6:
                    i6th.Source = new BitmapImage(new Uri(this.BaseUri, "ms-appx:/Assets/images/Letters/" + letter + ".png"));
                    break;
                case 7:
                    i7th.Source = new BitmapImage(new Uri(this.BaseUri, "ms-appx:/Assets/images/Letters/" + letter + ".png"));
                    break;
                case 8:
                    i8th.Source = new BitmapImage(new Uri(this.BaseUri, "ms-appx:/Assets/images/Letters/" + letter + ".png"));
                    break;
                case 9:
                    i9th.Source = new BitmapImage(new Uri(this.BaseUri, "ms-appx:/Assets/images/Letters/" + letter + ".png"));
                    break;
                case 10:
                    i10th.Source = new BitmapImage(new Uri(this.BaseUri, "ms-appx:/Assets/images/Letters/" + letter + ".png"));
                    break;
                case 11:
                    break;
                case 12:
                    i11th.Source = new BitmapImage(new Uri(this.BaseUri, "ms-appx:/Assets/images/Letters/" + letter + ".png"));
                    break;
                case 13:
                    i12th.Source = new BitmapImage(new Uri(this.BaseUri, "ms-appx:/Assets/images/Letters/" + letter + ".png"));
                    break;
                case 14:
                    i13th.Source = new BitmapImage(new Uri(this.BaseUri, "ms-appx:/Assets/images/Letters/" + letter + ".png"));
                    break;
                case 15:
                    i14th.Source = new BitmapImage(new Uri(this.BaseUri, "ms-appx:/Assets/images/Letters/" + letter + ".png"));
                    break;
                case 16:
                    i15th.Source = new BitmapImage(new Uri(this.BaseUri, "ms-appx:/Assets/images/Letters/" + letter + ".png"));
                    break;
            }
        }
    }
}
