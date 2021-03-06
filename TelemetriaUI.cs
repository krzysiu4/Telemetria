﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using Newtonsoft.Json;
using GMap.NET;
using GMap.NET.WindowsForms;
using System.Collections;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms.Markers;
using System.Net;
using MissionPlanner;
using System.Threading;
using GMap.NET.WindowsForms.ToolTips;
using MissionPlanner.Utilities;

namespace Telemetria
{
    public partial class TelemetriaUI : Form
    {
        
        private CookieContainer cookies = new CookieContainer();                 // Kontener na ciasteczka
        private string interopURL;                                               // Adres serwera sędziów    
        bool zalogowano = false;                                                 // Przechowuje informacje czy zalogowano
        bool telemetryThreadStop = false;                                        // Sygnalizuje zakończenie wątku od przesyłania Telmetri
        bool ruchomePrzeszkodyThreadStop = false;                                // Sygnalizuje zakończenie wątku od wyświetlania przeszkód
        dynamic activeMission;
        GMapOverlay movingObstaclesOverlay = new GMapOverlay("MovingObstacles"); // Przeszkody ruchome
        GMapOverlay staticObstaclesOverlay = new GMapOverlay("StaticObstacles"); // Przeszkody nieruchome
        GMapOverlay myMarkersOverlay = new GMapOverlay("MyMarkers");             // Markery z wartością wysokości, Off-Axis Target i Last Pos of Emergency Target           
       // private static System.Timers.Timer aTimer;

        private TelemPlugin plugin;                                              // Uchwyt do danych z MissionPlannera
        public TelemetriaUI(TelemPlugin plugin)
        {
            this.plugin = plugin; 
            InitializeComponent();
            this.plugin.Host.FPGMapControl.Overlays.Add(staticObstaclesOverlay);
            this.plugin.Host.FPGMapControl.Overlays.Add(movingObstaclesOverlay);
            this.plugin.Host.FPGMapControl.Overlays.Add(myMarkersOverlay);
        }



        // Logowanie do serwera sędziów
        private void buttonLogin_Click(object sender, EventArgs e)
        {
            string url = textBoxURL.Text;   
            interopURL = url;                                                                    // Tylko ponowne zalogowanie zmienia url serwera
            string uri = "/api/login";
            string username = textBoxUsername.Text;
            string password = textBoxPassword.Text;
            string myParameters = "username=" + username + "&" + "password=" + password;
            using (CookieWebClient wc = new CookieWebClient(cookies))
            {
                wc.BaseAddress = url;
                wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded"; // Potrzebne
                string httpResult = wc.UploadString(uri, myParameters);
                cookies = wc.CookieContainer;                                                   // Zapisuje otrzymane cookies
                textBoxHTTPResult.Text = httpResult;
                zalogowano = true;
              // textBoxHTTPResult.Text += "\nCookies: " + cookies.GetCookieHeader(new Uri(url + uri)); // Wyświetla cookies
            }
        }

        // Pobieranie Misji
        private void buttonPobierzMisje_Click(object sender, EventArgs e)
        {
            // Dodać sprawdzenie czy zalogowano
            string url = interopURL;
            string uri = "/api/missions";
            //using (CookieWebClient wc = new CookieWebClient(cookies)) // Korzysta z cookies, zapisanych przy logowaniu
            //{
            //    wc.BaseAddress = url;
            //    string httpResult = wc.DownloadString(uri);
            //    textBoxHTTPResult.Text = httpResult;
            //}
            string missionJson = "[{ 'stationary_obstacles': [{'latitude': 38.14792, 'cylinder_height': 200.0, 'cylinder_radius': 150.0, 'longitude': -76.427995}, {'latitude': 38.145823, 'cylinder_height': 300.0, 'cylinder_radius': 50.0, 'longitude': -76.422396}], 'fly_zones': [{'boundary_pts': [{'latitude': 38.142544, 'order': 1, 'longitude': -76.434088}, {'latitude': 38.141833, 'order': 2, 'longitude': -76.425263}, {'latitude': 38.144678, 'order': 3, 'longitude': -76.427995}], 'altitude_msl_max': 200.0, 'altitude_msl_min': 10.0}], 'off_axis_target_pos': {'latitude': 38.142544, 'longitude': -76.434088}, 'moving_obstacles': [{'sphere_radius': 50.0, 'speed_avg': 30.0}], 'mission_waypoints': [{'latitude': 38.142544, 'altitude_msl': 200.0, 'order': 1, 'longitude': -76.434088}, {'latitude': 38.141833, 'altitude_msl': 300.0, 'order': 2, 'longitude': -76.425263}, {'latitude': 38.144678, 'altitude_msl': 100.0, 'order': 3, 'longitude': -76.427995}], 'emergent_last_known_pos': {'latitude': 38.145823, 'longitude': -76.422396}, 'search_grid_points': [{'latitude': 38.142544, 'altitude_msl': 200.0, 'order': 1, 'longitude': -76.434088}, {'latitude': 38.141833, 'altitude_msl': 300.0, 'order': 2, 'longitude': -76.425263}, {'latitude': 38.144678, 'altitude_msl': 100.0, 'order': 3, 'longitude': -76.427995}], 'active': true, 'id': 1, 'home_pos': {'latitude': 38.14792, 'longitude': -76.427995}, 'air_drop_pos': {'latitude': 38.141833, 'longitude': -76.425263}}]";
            dynamic missionsObject = JsonConvert.DeserializeObject(missionJson);

            foreach (var mission in missionsObject)
            {
                // Tylko jedna missja powinna być aktywna
                if (mission.active == true)
                {
                    activeMission = mission;
                    break; ; // Wychodzi z pętli po znalezieniu pierwszej aktywnej misji
                }
            }
            plugin.Host.FPGMapControl.Position = new PointLatLng( (double) activeMission.home_pos.latitude, (double) activeMission.home_pos.longitude);
        }

        // Wczytuje WayPoint'y
        private void buttonLoadWP_Click(object sender, EventArgs e)
        {
            foreach (var wayPoint in activeMission.mission_waypoints)
            {
                plugin.Host.AddWPtoList(MAVLink.MAV_CMD.WAYPOINT, 0, 0, 0, 0, (double)wayPoint.longitude, (double)wayPoint.latitude, (double)wayPoint.altitude_msl);
            }    
        }

        // Rysuje Off-Axis Target, Emergent Last Known i Air Drop Position
        private void buttonRysujAllPostionMarkers_Click(object sender, EventArgs e)
        {
            // Rysuje Off-Axis Target
            double lat = activeMission.off_axis_target_pos.latitude;
            double lng = activeMission.off_axis_target_pos.longitude;
            GMarkerGoogle marker = new GMarkerGoogle(new PointLatLng(lat, lng), GMarkerGoogleType.green);
            myMarkersOverlay.Markers.Add(marker);
            marker.ToolTipMode = MarkerTooltipMode.Always;
            marker.ToolTip = new GMapRoundedToolTip(marker);
            marker.ToolTipText = "Off-Axis Target";

            // Rysuje Emergent Last Position
            double lat2 = activeMission.emergent_last_known_pos.latitude;
            double lng2 = activeMission.emergent_last_known_pos.longitude;
            GMarkerGoogle marker2 = new GMarkerGoogle(new PointLatLng(lat2, lng2), GMarkerGoogleType.yellow);
            myMarkersOverlay.Markers.Add(marker2);
            marker2.ToolTipMode = MarkerTooltipMode.Always;
            marker2.ToolTip = new GMapRoundedToolTip(marker2);
            marker2.ToolTipText = "Emergent Last Known Pos";

            // Rysuje Air Drop Position
            double lat3 = activeMission.air_drop_pos.latitude;
            double lng3 = activeMission.air_drop_pos.longitude;
            GMarkerGoogle marker3 = new GMarkerGoogle(new PointLatLng(lat3, lng3), GMarkerGoogleType.lightblue);
            myMarkersOverlay.Markers.Add(marker3);
            marker3.ToolTipMode = MarkerTooltipMode.Always;
            marker3.ToolTip = new GMapRoundedToolTip(marker3);
            marker3.ToolTipText = "Air Drop";
        }


        // Rysuje Geofence
        private void buttonRysujGeofence_Click(object sender, EventArgs e)
        {
            // Wypisać pobrane Fly_zones

            List<PointLatLngAlt> geofencePointsList = new List<PointLatLngAlt>();
            foreach (var geofencePoint in activeMission.fly_zones[0].boundary_pts)
            {
                PointLatLngAlt gp = new PointLatLngAlt((double)geofencePoint.latitude, (double)geofencePoint.longitude);
                geofencePointsList.Add(gp);
            }
            plugin.Host.RedrawFPPolygon(geofencePointsList);

            // Ustawia wysokość max i min GeoFenc'u
            try
            {
                if (plugin.Host.comPort.MAV.param.ContainsKey("FENCE_MINALT"))
                    plugin.Host.comPort.setParam("FENCE_MINALT", activeMission.fly_zones[0].altitude_msl_min);
                if (plugin.Host.comPort.MAV.param.ContainsKey("FENCE_MAXALT"))
                    plugin.Host.comPort.setParam("FENCE_MAXALT", activeMission.fly_zones[0].altitude_msl_max);
            }
            catch
            {
                CustomMessageBox.Show("Failed to set min/max fence alt");
                return;
            }
        }

        // Rysuje Search Grid
        private void buttonRysujSearchGrid_Click(object sender, EventArgs e)
        {
            List<PointLatLngAlt> searchGridPointsList = new List<PointLatLngAlt>();
            foreach (var searchGridPoint in activeMission.search_grid_points)
            {
                PointLatLngAlt sgp = new PointLatLngAlt( (double) searchGridPoint.latitude, (double) searchGridPoint.longitude);
                searchGridPointsList.Add(sgp);
            }
            plugin.Host.RedrawFPPolygon(searchGridPointsList);
        }

        // Pobiera i wyświetla Przeszkody Stacjonarne
        private void buttonPobierzWyswietlPrzeszkodyStacjonarne_Click(object sender, EventArgs e)
        {
            string url = interopURL;
            string uri = "/api/obstacles";
            string httpResult;

            //using (CookieWebClient wc = new CookieWebClient(cookies)) // Korzysta z cookies, zapisanych przy logowaniu
            //{
            //    wc.BaseAddress = url;
            //    httpResult = wc.DownloadString(uri);
            //}
            //  dynamic obstacles = JsonConvert.DeserializeObject(httpResult);

            // Dane do testów bez konieczności łączenia z serwerem
            string jsonDataObstacles = "{'stationary_obstacles': [{'latitude': 38.14792, 'cylinder_height': 200.0, 'cylinder_radius': 150.0, 'longitude': -76.427995}, {'latitude': 38.145823, 'cylinder_height': 300.0, 'cylinder_radius': 50.0, 'longitude': -76.422396}], 'moving_obstacles': [{'latitude': 38.14231360808151, 'sphere_radius': 50.0, 'altitude_msl': 269.53771210358616, 'longitude': -76.42518343430758}]}";
            dynamic obstacles = JsonConvert.DeserializeObject(jsonDataObstacles);

            foreach (var stationaryObstacle in obstacles.stationary_obstacles)
            {
                double lat = stationaryObstacle.latitude;
                double lng = stationaryObstacle.longitude;
                double alt = stationaryObstacle.cylinder_height;
                double radius = stationaryObstacle.cylinder_radius / 111200.0; // Przeliczenie na stopnie długości geograficznej (longitude)
                double aspect = 1.271438; // Stosunek 1m szerok. geograf do 1m dł geograficznej

                List<PointLatLng> gpollist = new List<PointLatLng>();
                int segments = 30;
                double seg = Math.PI * 2 / segments;
                for (int i = 0; i < segments; i++)
                {
                    double theta = seg * i;
                    double a = lat + Math.Cos(theta) * radius;
                    double b = lng + Math.Sin(theta) * radius * aspect;
                    PointLatLng gpoi = new PointLatLng(a, b);
                    gpollist.Add(gpoi);
                }
                GMapPolygon gpol = new GMapPolygon(gpollist, "pol");
                gpol.Fill = new SolidBrush(Color.FromArgb(50, Color.Yellow));
                gpol.Stroke = new Pen(Color.Yellow, 1);
                staticObstaclesOverlay.Polygons.Add(gpol);

                GMarkerGoogle marker = new GMarkerGoogle(new PointLatLng(lat, lng), GMarkerGoogleType.blue);
                myMarkersOverlay.Markers.Add(marker);
                marker.ToolTipMode = MarkerTooltipMode.Always;
                marker.ToolTip = new GMapRoundedToolTip(marker);
                marker.ToolTipText = alt.ToString() + "msl";
            }
           // plugin.Host.FPGMapControl.Position = new PointLatLng(38.144727, -76.428007);
        }

        // Usuwa przeszkody i markery
        private void buttonUsunPrzeszkodyMarkery_Click(object sender, EventArgs e)
        {
            staticObstaclesOverlay.Clear();
            movingObstaclesOverlay.Clear();
            myMarkersOverlay.Clear();
        }

        // Wyświetla przeszkody ruchome
        private void buttonWyswietlPrzeszkodyRuchome_Click(object sender, EventArgs e) // Korzysta z cookies, zapisanych przy logowaniu
        {
            Thread myThread = new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                string url = interopURL;
                string uri = "/api/obstacles";
                string httpResult = "Error: Bad URL or not logged in";
              
                while (true)
                {
                    using (CookieWebClient wc = new CookieWebClient(cookies))
                    {
                        try
                        {
                            wc.BaseAddress = url;
                            httpResult = wc.DownloadString(uri);
                            //   dynamic obstacles = JsonConvert.DeserializeObject(httpResult);

                            // Dane do testów bez konieczności łączenia z serwerem
                            string jsonDataObstacles = "{'stationary_obstacles': [{'latitude': 38.14792, 'cylinder_height': 200.0, 'cylinder_radius': 150.0, 'longitude': -76.427995}, {'latitude': 38.145823, 'cylinder_height': 300.0, 'cylinder_radius': 50.0, 'longitude': -76.422396}], 'moving_obstacles': [{'latitude': 38.14231360808151, 'sphere_radius': 50.0, 'altitude_msl': 269.53771210358616, 'longitude': -76.42518343430758}]}";
                            dynamic obstacles = JsonConvert.DeserializeObject(jsonDataObstacles);

                            foreach (var movingObstacle in obstacles.moving_obstacles)
                            {
                                double lat = movingObstacle.latitude;
                                double lng = movingObstacle.longitude;
                                double alt = movingObstacle.altitude_msl;
                                double radiusMeters = movingObstacle.sphere_radius;
                                double radius = movingObstacle.sphere_radius / 111200.0;
                                double aspect = 1.271438; // Stosunek 1m szerok. geograf do 1m dł geograficznej

                                List<PointLatLng> gpollist = new List<PointLatLng>();
                                int segments = 15;
                                double seg = Math.PI * 2 / segments;
                                for (int i = 0; i < segments; i++)
                                {
                                    double theta = seg * i;
                                    double a = lat + Math.Cos(theta) * radius;
                                    double b = lng + Math.Sin(theta) * radius * aspect;
                                    PointLatLng gpoi = new PointLatLng(a, b);
                                    gpollist.Add(gpoi);
                                }
                                GMapPolygon gpol = new GMapPolygon(gpollist, "pol");
                                gpol.Fill = new SolidBrush(Color.FromArgb(50, Color.Yellow));
                                gpol.Stroke = new Pen(Color.Yellow, 1);
                                movingObstaclesOverlay.Polygons.Add(gpol);

                                GMarkerGoogle marker = new GMarkerGoogle(new PointLatLng(lat, lng), GMarkerGoogleType.blue);
                                myMarkersOverlay.Markers.Add(marker);
                                marker.ToolTipMode = MarkerTooltipMode.Always;
                                marker.ToolTip = new GMapRoundedToolTip(marker);
                                marker.ToolTipText = ((int) alt-radiusMeters/2).ToString() + "-" + ((int) alt+radiusMeters/2).ToString() + "msl";
                            }
     

                        }
                        catch (WebException)
                        {
                            ruchomePrzeszkodyThreadStop = true;
                        }
                        catch (Exception ex)
                        {
                            ruchomePrzeszkodyThreadStop = true;
                        }
                    }

                    if (ruchomePrzeszkodyThreadStop)
                        break;                                      // Kończy wątek

                    Thread.Sleep(1000);                             // Czeka
                }
            });

            if (buttonWyswietlPrzeszkodyRuchome.Text == "START Ruchome Przeszkody")
            {
                if (myThread.IsAlive == true)
                    return;
             
                buttonWyswietlPrzeszkodyRuchome.Text = "STOP Ruchome Przeszkody";
                buttonWyswietlPrzeszkodyRuchome.BackColor = Color.Red;
                ruchomePrzeszkodyThreadStop = false;
                myThread.Start();
            }
            else if (buttonWyswietlPrzeszkodyRuchome.Text == "STOP Ruchome Przeszkody")
            {
                buttonWyswietlPrzeszkodyRuchome.Text = "START Ruchome Przeszkody";
                buttonWyswietlPrzeszkodyRuchome.BackColor = Color.Green;
                ruchomePrzeszkodyThreadStop = true;
            }
        }

        public void updateTextBoxTelem(string result)
        {
            textBoxTelem.Text = result;
        }

        // Wyslanie telemetrii
        private void buttonWysylajTelemetrie_Click(object sender, EventArgs e)
        {
            Thread myThread = new Thread(() =>
            {
                int cnt = 10;
                Thread.CurrentThread.IsBackground = true;
                string url = interopURL;
                string uri = "/api/telemetry";
                string httpResult = "Error: Bad URL or not logged in";
                double latitude, longitude, altitude, course;

                while (true)
                {
                    latitude = plugin.Host.cs.lat;
                    longitude = plugin.Host.cs.lng;
                    altitude = plugin.Host.cs.alt;
                    course = plugin.Host.cs.groundcourse;


                    string myParameters = "latitude=" + latitude.ToString(System.Globalization.CultureInfo.InvariantCulture) + "&" +          // Kropki zamiast przecinków
                                          "longitude=" + longitude.ToString(System.Globalization.CultureInfo.InvariantCulture) + "&" +        // Kropki zamiast przecinków
                                          "altitude_msl=" + altitude.ToString(System.Globalization.CultureInfo.InvariantCulture) + "&" +      // Kropki zamiast przecinków
                                          "uas_heading=" + course.ToString(System.Globalization.CultureInfo.InvariantCulture);                // Kropki zamiast przecinków


                    using (CookieWebClient wc = new CookieWebClient(cookies))
                    {
                        try
                        {
                            wc.BaseAddress = url;
                            wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded"; //  Za każdym razem trzeba dodać
                            httpResult = wc.UploadString(uri, myParameters);
                        }
                        catch (WebException)
                        {
                            telemetryThreadStop = true;
                        }
                        catch (Exception ex)
                        {
                            telemetryThreadStop = true;
                        }
                    }

                    cnt++;
                    if (cnt >= 10)               // Co dziesiąte wysłanie telemetri wyświetla komunikat
                    {
                        cnt = 0;
                        this.Invoke(new MethodInvoker(delegate ()
                        {
                            updateTextBoxTelem(httpResult);
                        }));
                    }

                    if (telemetryThreadStop)
                        break;                                      // Kończy wątek

                }
            });

            if (buttonWysylajTelemetrie.Text == "START Telemetria")
            {
                if (myThread.IsAlive == true)
                    return;

                buttonWysylajTelemetrie.Text = "STOP Telemetria";
                buttonWysylajTelemetrie.BackColor = Color.Red;
                telemetryThreadStop = false;
                myThread.Start();
            }
            else if (buttonWysylajTelemetrie.Text == "STOP Telemetria")
            {
                buttonWysylajTelemetrie.Text = "START Telemetria";
                buttonWysylajTelemetrie.BackColor = Color.Green;
                telemetryThreadStop = true;
            }
        }

        // Zamykanie okna pluginu
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            if (e.CloseReason == CloseReason.WindowsShutDown) return;

            // Confirm user wants to close
            switch (MessageBox.Show(this, "Are you sure you want to close?", "Closing", MessageBoxButtons.YesNo))
            {
                case DialogResult.No:
                    e.Cancel = true;
                    break;
                default:
                    telemetryThreadStop = true;
                    break;
            }
        }

      
    }
}
