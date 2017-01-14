using System;
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

namespace Telemetria
{
    public partial class TelemetriaUI : Form
    {
        
        private CookieContainer cookies = new CookieContainer();     // 
        private string interopURL;
        bool telemetryThreadStop = false;
        bool ruchomePrzeszkodyThreadStop = false;
        GMapOverlay movingObstaclesOverlay = new GMapOverlay("MovingObstacles"); // Przeszkody ruchome
        GMapOverlay staticObstaclesOverlay = new GMapOverlay("StaticObstacles"); // Przeszkody nieruchome
        GMapOverlay myMarkersOverlay = new GMapOverlay("MyMarkers"); // Markery z wartością wysokości
        public class Obstacles
        {

        }
        // Adres serwera sędziów
               
       // private static System.Timers.Timer aTimer;

        private TelemPlugin plugin;                     // Uchwyt do danych z MissionPlannera
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
            interopURL = url;                                          // Tylko ponowne zalogowanie zmienia url serwera
            string uri = "/api/login";
            string username = textBoxUsername.Text;
            string password = textBoxPassword.Text;
            string myParameters = "username=" + username + "&" + "password=" + password;

            using (CookieWebClient wc = new CookieWebClient(cookies))
            {
                wc.BaseAddress = url;
                wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded"; // Potrzebne
                string httpResult = wc.UploadString(uri, myParameters);
                 cookies = wc.CookieContainer;                                         // Zapisuje otrzymane cookies
                textBoxHTTPResult.Text = httpResult;
              //  textBoxHTTPResult.Text += "\nCookies: " + cookies.GetCookieHeader(new Uri(url + uri)); // Wyświetla cookies
            }
        }

        // Pobieranie Misji
        private void buttonPobierzMisje_Click(object sender, EventArgs e)
        {
            string url = interopURL;
            string uri = "/api/missions";

            using (CookieWebClient wc = new CookieWebClient(cookies)) // Korzysta z cookies, zapisanych przy logowaniu
            {
                wc.BaseAddress = url;
                string httpResult = wc.DownloadString(uri);
                textBoxHTTPResult.Text = httpResult;
            }
        }

        // Pobieranie przeszkod
        private void buttonPobierzPrzeszkody_Click(object sender, EventArgs e)
        {
            string url = interopURL;
            string uri = "/api/obstacles";

            using (CookieWebClient wc = new CookieWebClient(cookies)) // Korzysta z cookies, zapisanych przy logowaniu
            {
                wc.BaseAddress = url;
                string httpResult = wc.DownloadString(uri);
                textBoxHTTPResult.Text = httpResult;
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
                            httpResult = wc.UploadString(uri, myParameters);                          // 
                        }
                        catch (WebException)
                        {
                            telemetryThreadStop = true;
                        }
                    }

                    cnt++;
                    if (cnt >= 10)              // Co dziesiąte wysłanie telemetri wyświetla komunikat
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

            if (buttonWysylajTelemetrie.Text == "START")
            {
                if (myThread.IsAlive == true)
                    return;

                buttonWysylajTelemetrie.Text = "STOP";
                buttonWysylajTelemetrie.BackColor = Color.Red;
                telemetryThreadStop = false;
                myThread.Start();
            }
            else if (buttonWysylajTelemetrie.Text == "STOP")
            {
                buttonWysylajTelemetrie.Text = "START";
                buttonWysylajTelemetrie.BackColor = Color.Green;
                telemetryThreadStop = true;
            }
        }

        private void buttonWyswietlPrzeszkody_Click(object sender, EventArgs e)
        {
            wyswietlPrzeszkodyStacjonarne();
        }

        public void wyswietlPrzeszkodyStacjonarne()
        {
            //string url = interopURL;
            //string uri = "/api/obstacles";
            //string httpResult;

            //using (CookieWebClient wc = new CookieWebClient(cookies)) // Korzysta z cookies, zapisanych przy logowaniu
            //{
            //    wc.BaseAddress = url;
            //    httpResult = wc.DownloadString(uri);
            //}
            //dynamic obstacles = JsonConvert.DeserializeObject(httpResult);

            //GMapOverlay markersOverlay = new GMapOverlay("marker2332s");
            //// GMarkerCross marker = new GMarkerCross(new PointLatLng(51, 19));
            //GMarkerGoogle marker = new GMarkerGoogle(new PointLatLng(51, 19), GMarkerGoogleType.blue_dot);
            //markersOverlay.Markers.Add(marker);
            //plugin.Host.FPGMapControl.Overlays.Add(markersOverlay);
            //plugin.Host.FPGMapControl.UpdateMarkerLocalPosition(marker);

            string jsonDataObstacles = "{'stationary_obstacles': [{'latitude': 38.14792, 'cylinder_height': 200.0, 'cylinder_radius': 150.0, 'longitude': -76.427995}, {'latitude': 38.145823, 'cylinder_height': 300.0, 'cylinder_radius': 50.0, 'longitude': -76.422396}], 'moving_obstacles': [{'latitude': 38.14231360808151, 'sphere_radius': 50.0, 'altitude_msl': 269.53771210358616, 'longitude': -76.42518343430758}]}";
            dynamic obstacles = JsonConvert.DeserializeObject(jsonDataObstacles);
          //  GMapOverlay obstaclesOverlay = new GMapOverlay("Obstacles");
            
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

                GMarkerGoogle marker = new GMarkerGoogle(new PointLatLng(lat,lng), GMarkerGoogleType.blue);
                myMarkersOverlay.Markers.Add(marker);
                marker.ToolTipMode = MarkerTooltipMode.Always;
                marker.ToolTip = new GMapRoundedToolTip(marker);
                marker.ToolTipText = alt.ToString() + "m";
            }
            plugin.Host.FPGMapControl.Position = new PointLatLng(38.144727, -76.428007);

            //foreach (GMapOverlay ov in plugin.Host.FPGMapControl.Overlays)
            //    textBoxHTTPResult.Text += ov.ToString();
            //  obstaclesOverlay.Clear();
            //  obstaclesOverlay.setMap(null);
        }

        private void buttonUsunPrzeszkody_Click(object sender, EventArgs e)
        {
            staticObstaclesOverlay.Clear();
            movingObstaclesOverlay.Clear();
            myMarkersOverlay.Clear();
        }

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
                          //  httpResult = wc.DownloadString(uri);
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
                    }

                    if (ruchomePrzeszkodyThreadStop)
                        break;                                      // Kończy wątek

                    Thread.Sleep(1000);
                }
            });

            if (buttonWyswietlPrzeszkodyRuchome.Text == "START")
            {
                if (myThread.IsAlive == true)
                    return;
             
                buttonWyswietlPrzeszkodyRuchome.Text = "STOP";
                buttonWyswietlPrzeszkodyRuchome.BackColor = Color.Red;
                ruchomePrzeszkodyThreadStop = false;
                myThread.Start();
            }
            else if (buttonWyswietlPrzeszkodyRuchome.Text == "STOP")
            {
                buttonWyswietlPrzeszkodyRuchome.Text = "START";
                buttonWyswietlPrzeszkodyRuchome.BackColor = Color.Green;
                ruchomePrzeszkodyThreadStop = true;
            }
        }
    }
}
