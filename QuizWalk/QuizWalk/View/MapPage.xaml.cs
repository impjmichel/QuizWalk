﻿using Bing.Maps;
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
using Bing.Maps.Directions;
using QuizWalk.View;
using Windows.UI.Popups;


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
        private Location currentLoc { get; set; }
        private Pushpin userPin;
        private Dictionary<Questionpoint,Pushpin> qPoints;
        private WaypointCollection col;
        private bool isFirstTime = true;
        private MapShapeLayer RouteLayer = new MapShapeLayer();
        public QuestionFlyout QFlyaout;
        private int count = 1;

        public MapPage()
        {
            this.InitializeComponent();
            geolocator = new Geolocator();
            userPin = new Pushpin();
            qPoints = new Dictionary<Questionpoint,Pushpin>();
            col = new WaypointCollection();
            QFlyaout = new QuestionFlyout();
            createQuestionPoints();

            //for (int i = 1; i < 10; i++)
            //{
            //    string qp = i + "";
            //    setValues(qPoints, qp, true);
            //}
            geolocator.PositionChanged += new Windows.Foundation.TypedEventHandler<Geolocator, PositionChangedEventArgs>(geolocator_PositionChanged);
            drawQuestionPin(qPoints);
            GeofenceMonitor.Current.GeofenceStateChanged += Current_GeofenceStateChanged;

            zoomToLocation();
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
                    //MessageDialog mes = new MessageDialog("user");
                    //mes.ShowAsync();

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
                Map.SetView(currentLoc, 15.0);                
                geolocator.DesiredAccuracy = PositionAccuracy.Default;  
              
                if(isFirstTime)
                {
                    addWaypoint(currentLoc);
                    isFirstTime = false;
                }

                drawRoute(col);
            }
            catch (Exception d)
            {
                System.Diagnostics.Debug.WriteLine(d);
            }
        }

        /// <summary>
        /// Method to change the color of a pin
        /// <param name="p"></param>
        /// </summary>
        public async void setPinVisited(Pushpin p)
        {
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                p.Background = new SolidColorBrush { Color = new Windows.UI.Color { A = 100, R = 100, B = 100, G = 100 } };
            });
        }

        /// <summary>
        /// Method to set the UserPin
        /// <param name="loc"></param>
        /// </summary>
        public async void setUserPin(Location loc)
        {            
            userPin = new Pushpin();
            userPin.Text = "U";
            userPin.Background = new SolidColorBrush(new Windows.UI.Color { A = 255, B = 255, G = 0, R = 0 });
            MapLayer.SetPosition(userPin, loc);
            Map.Children.Add(userPin);
        }

        /// <summary>
        /// Method to create a list with Questionpoints
        /// </summary>
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

            Questionpoint end = new Questionpoint("Einde", 51.586465, 4.791494);
            qPoints.Add(end, createQuestionPointPins(end));

            addGeofence(qPoints);
        }

        /// <summary>
        /// Method to set the values of a Questionpoint
        /// <param name="dict"></param>
        /// <param name="name"></param>
        ///         the name of the Questionpoint
        /// <param name="isAnswered"></param>
        ///         set isAnswered
        /// </summary>
        public void setValues(string name, bool isAnswered)
        {
            foreach (KeyValuePair<Questionpoint, Pushpin> pair in qPoints)
            {
                if(pair.Key.name == name)
                pair.Key.isAnswered = isAnswered;
            }
            count++;
        }

        public void addGeofence(Dictionary<Questionpoint, Pushpin> dict)
        {
            if (GeofenceMonitor.Current.Geofences.Count > 0)
                GeofenceMonitor.Current.Geofences.Clear();

            foreach (KeyValuePair<Questionpoint, Pushpin> pair in dict)
            {
                Location loc = new Location(pair.Key.latitude, pair.Key.longitude);
                Geofence fence = createGeofence(loc, pair.Key.name);
                GeofenceMonitor.Current.Geofences.Add(fence);                
            }            
        }

        /// <summary>
        /// Method to draw a questionpointpin
        /// <param name="dict"></param>
        /// </summary>
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
                    addWaypoint(loc);
                                        
                    if (pair.Key.isAnswered)
                        setPinVisited(pair.Value);

                    if (pair.Key.isAnswered != true)
                    {
                        isNext = false;
                    }
                }
            }
        }

        /// <summary>
        /// Method to create a questionpointpin
        /// <param name="qp"></param>
        /// </summary>
        public Pushpin createQuestionPointPins(Questionpoint qp)
        {
            Location loc = new Location(qp.latitude, qp.longitude);
            Pushpin pp = new Pushpin();
            pp.Name = qp.name;
            pp.Text = qp.name;
            pp.Background = new SolidColorBrush(new Windows.UI.Color { A = 255, B = 0, G = 100, R = 255 });           
            
            return pp;           
        }

        /// <summary>
        /// Method to create a geofence
        /// <param name="l"></param>
        /// <param name="name"></param>
        ///         name of the geofence
        /// </summary>
        public Geofence createGeofence(Location l, String name)
        {
            MonitoredGeofenceStates mask = 0;

            mask |= MonitoredGeofenceStates.Entered;
            mask |= MonitoredGeofenceStates.Exited;

            Geofence fence = new Geofence(name, new Geocircle(new BasicGeoposition { Altitude = 0.0, Latitude = l.Latitude, Longitude = l.Longitude }, 10),mask,true, new TimeSpan(2));
            
            return fence;
        }

        /// <summary>
        /// Method to draw a route between pins
        /// <param name="col"></param>
        /// </summary>
        public async void drawRoute(WaypointCollection col)
        {
            RouteLayer.Shapes.Clear();

            DirectionsManager manager = Map.DirectionsManager;
            manager.RequestOptions.RouteMode = RouteModeOption.Walking;
            manager.Waypoints = col;
            manager.RenderOptions.WaypointPushpinOptions.Visible = false;

            RouteResponse resp = await manager.CalculateDirectionsAsync();
            resp.Routes[0].RoutePath.LineWidth = 10.0;
            resp.Routes[0].RoutePath.LineColor = new Windows.UI.Color { A = 200, R = 200, B = 0, G = 2 };

            LocationCollection locs = new LocationCollection();
            foreach(Location l in resp.Routes[0].RoutePath.PathPoints)
            {
                locs.Add(l);
            }

            MapPolyline line = new MapPolyline {Locations = locs};
            line.Color = new Windows.UI.Color { A = 100, G = 0, B = 200, R = 0 };
            line.Visible = true;
            line.Width = 10.0;
            RouteLayer.Shapes.Add(line);
            Map.ShapeLayers.Add(RouteLayer);
        }

        /// <summary>
        /// Method to add waypoints to a collection
        /// <param name="loc"></param>
        /// </summary>
        public void addWaypoint(Location loc)
        {
            col.Add(new Waypoint(loc));
        }

        /// <summary>
        /// Geofence handler
        /// <param name="sender"></param>
        /// <param name="args"></param>
        /// </summary>
        void Current_GeofenceStateChanged(GeofenceMonitor sender, object args)
        {
            var reports = sender.ReadReports();
            foreach (GeofenceStateChangeReport report in reports)
            {
                GeofenceState state = report.NewState;

                Geofence geofence = report.Geofence;

                if (state == GeofenceState.Exited)
                {
                    System.Diagnostics.Debug.WriteLine("geofence exited");
                }

                if (state == GeofenceState.Entered)
                {
                    this.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, new DispatchedHandler(
                    () =>
                    {
                        System.Diagnostics.Debug.WriteLine("geofence entered");
                        QFlyaout.loadText(count);
                        QFlyaout.Show();
                    }));
                }
            }
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
