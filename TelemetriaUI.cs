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

        private CookieContainer cookies = new CookieContainer();     // 
        private string interopURL;
        bool telemetryThreadStop = false;
        // Adres serwera sędziów
               
       // private static System.Timers.Timer aTimer;

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
