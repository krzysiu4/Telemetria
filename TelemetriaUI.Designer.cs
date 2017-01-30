namespace Telemetria
{
    partial class TelemetriaUI
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBoxURL = new System.Windows.Forms.TextBox();
            this.buttonLogin = new System.Windows.Forms.Button();
            this.textBoxHTTPResult = new System.Windows.Forms.TextBox();
            this.textBoxUsername = new System.Windows.Forms.TextBox();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.buttonPobierzMisje = new System.Windows.Forms.Button();
            this.buttonWysylajTelemetrie = new System.Windows.Forms.Button();
            this.textBoxTelem = new System.Windows.Forms.TextBox();
            this.buttonPobierzWyswietlPrzeszkody = new System.Windows.Forms.Button();
            this.buttonUsunPrzeszkody = new System.Windows.Forms.Button();
            this.buttonWyswietlPrzeszkodyRuchome = new System.Windows.Forms.Button();
            this.buttonLoadWP = new System.Windows.Forms.Button();
            this.buttonRysujGeofence = new System.Windows.Forms.Button();
            this.buttonRysujAllPositionMarkers = new System.Windows.Forms.Button();
            this.buttonRysujSearchGrid = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // textBoxURL
            // 
            this.textBoxURL.Location = new System.Drawing.Point(12, 10);
            this.textBoxURL.Name = "textBoxURL";
            this.textBoxURL.Size = new System.Drawing.Size(169, 20);
            this.textBoxURL.TabIndex = 5;
            this.textBoxURL.Text = "http://";
            // 
            // buttonLogin
            // 
            this.buttonLogin.Location = new System.Drawing.Point(12, 62);
            this.buttonLogin.Name = "buttonLogin";
            this.buttonLogin.Size = new System.Drawing.Size(80, 28);
            this.buttonLogin.TabIndex = 10;
            this.buttonLogin.Text = "Login";
            this.buttonLogin.UseVisualStyleBackColor = true;
            this.buttonLogin.Click += new System.EventHandler(this.buttonLogin_Click);
            // 
            // textBoxHTTPResult
            // 
            this.textBoxHTTPResult.Location = new System.Drawing.Point(187, 10);
            this.textBoxHTTPResult.Multiline = true;
            this.textBoxHTTPResult.Name = "textBoxHTTPResult";
            this.textBoxHTTPResult.Size = new System.Drawing.Size(352, 80);
            this.textBoxHTTPResult.TabIndex = 17;
            // 
            // textBoxUsername
            // 
            this.textBoxUsername.Location = new System.Drawing.Point(12, 36);
            this.textBoxUsername.Name = "textBoxUsername";
            this.textBoxUsername.Size = new System.Drawing.Size(80, 20);
            this.textBoxUsername.TabIndex = 19;
            this.textBoxUsername.Text = "testuser";
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Location = new System.Drawing.Point(98, 36);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.Size = new System.Drawing.Size(83, 20);
            this.textBoxPassword.TabIndex = 20;
            this.textBoxPassword.Text = "testpass";
            // 
            // buttonPobierzMisje
            // 
            this.buttonPobierzMisje.Location = new System.Drawing.Point(98, 62);
            this.buttonPobierzMisje.Name = "buttonPobierzMisje";
            this.buttonPobierzMisje.Size = new System.Drawing.Size(83, 28);
            this.buttonPobierzMisje.TabIndex = 21;
            this.buttonPobierzMisje.Text = "Pobierz Misje";
            this.buttonPobierzMisje.UseVisualStyleBackColor = true;
            this.buttonPobierzMisje.Click += new System.EventHandler(this.buttonPobierzMisje_Click);
            // 
            // buttonWysylajTelemetrie
            // 
            this.buttonWysylajTelemetrie.BackColor = System.Drawing.Color.Transparent;
            this.buttonWysylajTelemetrie.Location = new System.Drawing.Point(187, 175);
            this.buttonWysylajTelemetrie.Name = "buttonWysylajTelemetrie";
            this.buttonWysylajTelemetrie.Size = new System.Drawing.Size(177, 28);
            this.buttonWysylajTelemetrie.TabIndex = 23;
            this.buttonWysylajTelemetrie.Text = "START Telemetria";
            this.buttonWysylajTelemetrie.UseVisualStyleBackColor = false;
            this.buttonWysylajTelemetrie.Click += new System.EventHandler(this.buttonWysylajTelemetrie_Click);
            // 
            // textBoxTelem
            // 
            this.textBoxTelem.Location = new System.Drawing.Point(187, 209);
            this.textBoxTelem.Multiline = true;
            this.textBoxTelem.Name = "textBoxTelem";
            this.textBoxTelem.Size = new System.Drawing.Size(177, 28);
            this.textBoxTelem.TabIndex = 14;
            // 
            // buttonPobierzWyswietlPrzeszkody
            // 
            this.buttonPobierzWyswietlPrzeszkody.Location = new System.Drawing.Point(187, 96);
            this.buttonPobierzWyswietlPrzeszkody.Name = "buttonPobierzWyswietlPrzeszkody";
            this.buttonPobierzWyswietlPrzeszkody.Size = new System.Drawing.Size(177, 28);
            this.buttonPobierzWyswietlPrzeszkody.TabIndex = 25;
            this.buttonPobierzWyswietlPrzeszkody.Text = "Wyświetl Przeszkody Stacjonarne";
            this.buttonPobierzWyswietlPrzeszkody.UseVisualStyleBackColor = true;
            this.buttonPobierzWyswietlPrzeszkody.Click += new System.EventHandler(this.buttonPobierzWyswietlPrzeszkodyStacjonarne_Click);
            // 
            // buttonUsunPrzeszkody
            // 
            this.buttonUsunPrzeszkody.Location = new System.Drawing.Point(370, 130);
            this.buttonUsunPrzeszkody.Name = "buttonUsunPrzeszkody";
            this.buttonUsunPrzeszkody.Size = new System.Drawing.Size(169, 28);
            this.buttonUsunPrzeszkody.TabIndex = 26;
            this.buttonUsunPrzeszkody.Text = "Usun Przeszkody i Markery";
            this.buttonUsunPrzeszkody.UseVisualStyleBackColor = true;
            this.buttonUsunPrzeszkody.Click += new System.EventHandler(this.buttonUsunPrzeszkodyMarkery_Click);
            // 
            // buttonWyswietlPrzeszkodyRuchome
            // 
            this.buttonWyswietlPrzeszkodyRuchome.Location = new System.Drawing.Point(12, 175);
            this.buttonWyswietlPrzeszkodyRuchome.Name = "buttonWyswietlPrzeszkodyRuchome";
            this.buttonWyswietlPrzeszkodyRuchome.Size = new System.Drawing.Size(169, 28);
            this.buttonWyswietlPrzeszkodyRuchome.TabIndex = 27;
            this.buttonWyswietlPrzeszkodyRuchome.Text = "START Ruchome Przeszkody";
            this.buttonWyswietlPrzeszkodyRuchome.UseVisualStyleBackColor = true;
            this.buttonWyswietlPrzeszkodyRuchome.Click += new System.EventHandler(this.buttonWyswietlPrzeszkodyRuchome_Click);
            // 
            // buttonLoadWP
            // 
            this.buttonLoadWP.Location = new System.Drawing.Point(12, 96);
            this.buttonLoadWP.Name = "buttonLoadWP";
            this.buttonLoadWP.Size = new System.Drawing.Size(169, 28);
            this.buttonLoadWP.TabIndex = 29;
            this.buttonLoadWP.Text = "Wczytaj WayPoint\'y";
            this.buttonLoadWP.UseVisualStyleBackColor = true;
            this.buttonLoadWP.Click += new System.EventHandler(this.buttonLoadWP_Click);
            // 
            // buttonRysujGeofence
            // 
            this.buttonRysujGeofence.Location = new System.Drawing.Point(370, 96);
            this.buttonRysujGeofence.Name = "buttonRysujGeofence";
            this.buttonRysujGeofence.Size = new System.Drawing.Size(169, 28);
            this.buttonRysujGeofence.TabIndex = 30;
            this.buttonRysujGeofence.Text = "Rysuj GeoFence Polygon";
            this.buttonRysujGeofence.UseVisualStyleBackColor = true;
            this.buttonRysujGeofence.Click += new System.EventHandler(this.buttonRysujGeofence_Click);
            // 
            // buttonRysujAllPositionMarkers
            // 
            this.buttonRysujAllPositionMarkers.Location = new System.Drawing.Point(187, 130);
            this.buttonRysujAllPositionMarkers.Name = "buttonRysujAllPositionMarkers";
            this.buttonRysujAllPositionMarkers.Size = new System.Drawing.Size(177, 28);
            this.buttonRysujAllPositionMarkers.TabIndex = 31;
            this.buttonRysujAllPositionMarkers.Text = "Rysuj Off-Axis Target, Emergent, Air Drop Position";
            this.buttonRysujAllPositionMarkers.UseVisualStyleBackColor = true;
            this.buttonRysujAllPositionMarkers.Click += new System.EventHandler(this.buttonRysujAllPostionMarkers_Click);
            // 
            // buttonRysujSearchGrid
            // 
            this.buttonRysujSearchGrid.Location = new System.Drawing.Point(12, 130);
            this.buttonRysujSearchGrid.Name = "buttonRysujSearchGrid";
            this.buttonRysujSearchGrid.Size = new System.Drawing.Size(169, 28);
            this.buttonRysujSearchGrid.TabIndex = 34;
            this.buttonRysujSearchGrid.Text = "Rysuj Search Grid Polygon";
            this.buttonRysujSearchGrid.UseVisualStyleBackColor = true;
            this.buttonRysujSearchGrid.Click += new System.EventHandler(this.buttonRysujSearchGrid_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 209);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(169, 28);
            this.textBox1.TabIndex = 35;
            // 
            // TelemetriaUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(552, 249);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.buttonRysujSearchGrid);
            this.Controls.Add(this.buttonRysujAllPositionMarkers);
            this.Controls.Add(this.buttonRysujGeofence);
            this.Controls.Add(this.buttonLoadWP);
            this.Controls.Add(this.buttonWyswietlPrzeszkodyRuchome);
            this.Controls.Add(this.buttonUsunPrzeszkody);
            this.Controls.Add(this.buttonPobierzWyswietlPrzeszkody);
            this.Controls.Add(this.buttonWysylajTelemetrie);
            this.Controls.Add(this.buttonPobierzMisje);
            this.Controls.Add(this.textBoxPassword);
            this.Controls.Add(this.textBoxUsername);
            this.Controls.Add(this.textBoxHTTPResult);
            this.Controls.Add(this.textBoxTelem);
            this.Controls.Add(this.buttonLogin);
            this.Controls.Add(this.textBoxURL);
            this.Name = "TelemetriaUI";
            this.Text = "Telemetria";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox textBoxURL;
        private System.Windows.Forms.Button buttonLogin;
        private System.Windows.Forms.TextBox textBoxHTTPResult;
        private System.Windows.Forms.TextBox textBoxUsername;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.Button buttonPobierzMisje;
        private System.Windows.Forms.Button buttonWysylajTelemetrie;
        private System.Windows.Forms.TextBox textBoxTelem;
        private System.Windows.Forms.Button buttonPobierzWyswietlPrzeszkody;
        private System.Windows.Forms.Button buttonUsunPrzeszkody;
        private System.Windows.Forms.Button buttonWyswietlPrzeszkodyRuchome;
        private System.Windows.Forms.Button buttonLoadWP;
        private System.Windows.Forms.Button buttonRysujGeofence;
        private System.Windows.Forms.Button buttonRysujAllPositionMarkers;
        private System.Windows.Forms.Button buttonRysujSearchGrid;
        private System.Windows.Forms.TextBox textBox1;
    }
}

