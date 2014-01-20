using Bing.Maps;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Geolocation;
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
using QuizWalk.Model1;
using Windows.Devices.Geolocation.Geofencing;
using Windows.UI.Core;


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace QuizWalk
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MapPage : Page
    {
        private static MapPage instance;
        private Geolocator geolocator;
        public Location currentLoc { get; set; }
        private Pushpin userPin;
        private Dictionary<Questionpoint,Pushpin> qPoints;

        public MapPage()
        {
            this.InitializeComponent();
            geolocator = new Geolocator();
            userPin = new Pushpin();
            qPoints = new Dictionary<Questionpoint,Pushpin>();
            zoomToLocation();
            createQuestionPoints();  
            geolocator.PositionChanged += new Windows.Foundation.TypedEventHandler<Geolocator, PositionChangedEventArgs>(geolocator_PositionChanged);
            drawQuestionPin(qPoints);          
        }

        private void geolocator_PositionChanged(Geolocator sender, PositionChangedEventArgs args)
        {
            // Need to set map view on UI thread.
            this.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, new DispatchedHandler(
                () =>
                {
                    //Get the current location
                    Location location = new Location(args.Position.Coordinate.Latitude, args.Position.Coordinate.Longitude);
                    
                    //Update the position of the GPS pushpin
                    MapLayer.SetPosition(userPin, location);     

                }));
        }

        public static MapPage getInstance()
        {
            return instance != null ? instance : (instance = new MapPage());
        }

        /// <summary>
        /// Method to zoom to my location
        /// </summary>
        private async void zoomToLocation()
        {
            try
            {
                geolocator = new Geolocator();
                Geoposition currentPos = await geolocator.GetGeopositionAsync();
                currentLoc = new Location(currentPos.Coordinate.Latitude, currentPos.Coordinate.Longitude);
                setUserPin(currentLoc);
                //System.Diagnostics.Debug.WriteLine("lat: " + currentPos.Coordinate.Latitude);
                //System.Diagnostics.Debug.WriteLine("lon: " + currentPos.Coordinate.Longitude);
                Map.SetView(currentLoc, 16.0);                
                geolocator.DesiredAccuracy = PositionAccuracy.Default;                
            }
            catch (Exception d)
            {
                System.Diagnostics.Debug.WriteLine(d);
            }
        }

        public async void setUserPin(Location loc)
        {            
            userPin = new Pushpin();
            userPin.Text = "U";
            userPin.Background = new SolidColorBrush(new Windows.UI.Color { A = 255, B = 255, G = 0, R = 0 });
            MapLayer.SetPosition(userPin, loc);
            Map.Children.Add(userPin);
        }

        public void createQuestionPoints()
        {
            Questionpoint qp1 = new Questionpoint("1", 51.585338, 4.791823);
            qPoints.Add(qp1, createQuestionPointPins(qp1));
            Questionpoint qp2 = new Questionpoint("2", 51.586248, 4.788655);
            qPoints.Add(qp2, createQuestionPointPins(qp2));
            Questionpoint qp3 = new Questionpoint("3", 51.585901, 4.787174);
            qPoints.Add(qp3, createQuestionPointPins(qp3));
            Questionpoint qp4 = new Questionpoint("4", 51.587048, 4.786917);
            qPoints.Add(qp4, createQuestionPointPins(qp4));
            Questionpoint qp5 = new Questionpoint("5", 51.587662, 4.784565);
            qPoints.Add(qp5, createQuestionPointPins(qp5));

            Questionpoint qp6 = new Questionpoint("6", 51.587638, 4.783046);
            qPoints.Add(qp6, createQuestionPointPins(qp6));
            Questionpoint qp7 = new Questionpoint("7", 51.588078, 4.781169);
            qPoints.Add(qp7, createQuestionPointPins(qp7));
            Questionpoint qp8 = new Questionpoint("8", 51.587652, 4.780557);
            qPoints.Add(qp8, createQuestionPointPins(qp8));
            Questionpoint qp9 = new Questionpoint("9", 51.587638, 4.779517);
            qPoints.Add(qp9, createQuestionPointPins(qp9));
            Questionpoint qp10 = new Questionpoint("10", 51.588465, 4.778465);
            qPoints.Add(qp10, createQuestionPointPins(qp10));

            Questionpoint qp11 = new Questionpoint("11",51.589443, 4.779755);
            qPoints.Add(qp11, createQuestionPointPins(qp11));
            Questionpoint qp12 = new Questionpoint("12",51.589993, 4.780281);
            qPoints.Add(qp12, createQuestionPointPins(qp12));
            Questionpoint qp13 = new Questionpoint("13",51.589286, 4.782062);
            qPoints.Add(qp13, createQuestionPointPins(qp13));
            Questionpoint qp14 = new Questionpoint("14",51.589669, 4.783575);
            qPoints.Add(qp14, createQuestionPointPins(qp14));
            Questionpoint qp15 = new Questionpoint("15",51.588436, 4.784605);
            qPoints.Add(qp15, createQuestionPointPins(qp15));
        }

        public void drawQuestionPin(Dictionary<Questionpoint,Pushpin> dict)
        {
            
            bool isNext = true;
            foreach(KeyValuePair<Questionpoint, Pushpin> pair in dict)
            {
                Map.Children.Remove(pair.Value);

                if(pair.Key.isAnswered == true || isNext == true)
                {
                    Location loc = new Location(pair.Key.latitude,pair.Key.longitude);
                    MapLayer.SetPosition(pair.Value, loc);
                    Map.Children.Add(pair.Value);
                    if(pair.Key.isAnswered != true)
                    {
                        isNext = false;
                    }
                }
            }
        }

        public Pushpin createQuestionPointPins(Questionpoint qp)
        {
            Location loc = new Location(qp.latitude, qp.longitude);
            Pushpin pp = new Pushpin();
            pp.Name = qp.name;
            pp.Text = qp.name;
            pp.Background = new SolidColorBrush(new Windows.UI.Color { A = 255, B = 0, G = 100, R = 255 });
            
            createGeofence(loc, qp.name);

            return pp;           
        }

        public Geofence createGeofence(Location l, String name)
        {
            Geofence fence = new Geofence(name, new Geocircle(new BasicGeoposition { Altitude = 0.0, Latitude = l.Latitude, Longitude = l.Longitude }, 10));
            return fence;
        }

        public async void drawRoute()
        {

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
