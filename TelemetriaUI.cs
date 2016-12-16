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

namespace Telemetria
{
    public partial class TelemetriaUI : Form
    {

        private CookieContainer cookies;                 // 
        private string interopURL;                      // Adres serwera sędziów

        public class Account
        {
            public string imie { get; set; }
            public bool id { get; set; }
        }
        private static System.Timers.Timer aTimer;

        private TelemPlugin plugin;                     // Uchwyt do danych z MissionPlannera

        public TelemetriaUI(TelemPlugin plugin)
        {
            this.plugin = plugin; 
            InitializeComponent();

        }

        private void TelemetriaUI_Load(object sender, EventArgs e)
        {

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

            using (CookieWebClient wc = new CookieWebClient(new CookieContainer()))
            {
                wc.BaseAddress = url;
                wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded"; // Potrzebne
                string httpResult = wc.UploadString(uri, myParameters);
                cookies = wc.CookieContainer;                                         // Zapisuje otrzymane cookies
                textBoxHTTPResult.Text = httpResult;
                textBoxHTTPResult.Text += "\nCookies: " + cookies.GetCookieHeader(new Uri(url + uri)); // Wyświetla cookies
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

        // Wyslanie telemetrii
        private void buttonWyslijTelemetrie_Click(object sender, EventArgs e)
        {
            //
            string username = textBoxUsername.Text;
            string password = textBoxPassword.Text;
            string url = textBoxURL.Text;
            string myParameters1 = "username=" + username + "&" + "password=" + password;
            //

            //  string url = interopURL;
            string uri1 = "/api/login";
            string uri = "/api/telemetry";

            double latitude = plugin.Host.cs.lat;
            double longitude = plugin.Host.cs.lng;
            double altitude = plugin.Host.cs.alt;
            double course = plugin.Host.cs.groundcourse;


            string myParameters = "latitude=" + latitude.ToString() + "&" +
                                  "longitude=" + longitude.ToString() + "&" +
                                  "altitude_msl=" + altitude.ToString() + "&" +
                                  "uas_heading=" + course.ToString();

            using (CookieWebClient wc = new CookieWebClient(new CookieContainer()))
            {
                wc.BaseAddress = url;            
                wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                string httpResult1 = wc.UploadString(uri1, myParameters1);                       // Ponowne logowanie
                wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded"; //  Za każdym razem trzeba dodać
                string httpResult = wc.UploadString(uri, myParameters);                          // Zmienić na zapytania async
                textBoxHTTPResult.Text = httpResult1 + "\n" + httpResult ;
               
            }
        }

        private void buttonStartTelem_Click(object sender, EventArgs e)
        {
            double period = 1 / double.Parse(textBoxTelemHz.Text);
            aTimer = new System.Timers.Timer(period);
            aTimer.Elapsed += OnTimedEvent;
            aTimer.AutoReset = false;                 // Do testow zatrzymany
            aTimer.Enabled = true;
        }

        private void updateTelemBox(string text)
        {
            textBoxTelem.Text = text;
        }

        private void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
        {
            // Działa jako osobny wątek

            double latitude = plugin.Host.cs.lat;
            double longitude = plugin.Host.cs.lng;
            double altitude = plugin.Host.cs.alt;
            double course = plugin.Host.cs.groundcourse;
            string arg = latitude.ToString() + " " + longitude.ToString() + " " + altitude.ToString() + " " + course.ToString();
            ProcessStartInfo start = new ProcessStartInfo();
            start.FileName = @"C:\Users\Krzysiu\AppData\Local\Programs\Python\Python35-32\python.exe";
            start.Arguments = string.Format("{0} {1}", @"Skrypty\auvsi_mp.py", arg);
            start.UseShellExecute = false;
            start.RedirectStandardOutput = true;
            using (Process process = Process.Start(start))
            {
                using (StreamReader reader = process.StandardOutput)
                {
                    string result = reader.ReadToEnd();
                    if (this.IsDisposed) return;
                    this.Invoke(new MethodInvoker(delegate ()
                    {
                        updateTelemBox(result);

                    }));
               
                }
            }
        }



        private void buttonAuvsi_Click(object sender, EventArgs e)
        {
            timerTelem.Start();
        }

     
    }
}
