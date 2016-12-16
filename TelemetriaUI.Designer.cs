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
            this.components = new System.ComponentModel.Container();
            this.textBoxURL = new System.Windows.Forms.TextBox();
            this.buttonLogin = new System.Windows.Forms.Button();
            this.buttonStartTelem = new System.Windows.Forms.Button();
            this.textBoxTelem = new System.Windows.Forms.TextBox();
            this.textBoxTelemHz = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.timerTelem = new System.Windows.Forms.Timer(this.components);
            this.textBoxHTTPResult = new System.Windows.Forms.TextBox();
            this.textBoxUsername = new System.Windows.Forms.TextBox();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.buttonPobierzMisje = new System.Windows.Forms.Button();
            this.buttonPobierzPrzeszkody = new System.Windows.Forms.Button();
            this.buttonWyslijTelemetrie = new System.Windows.Forms.Button();
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
            this.buttonLogin.Location = new System.Drawing.Point(17, 78);
            this.buttonLogin.Name = "buttonLogin";
            this.buttonLogin.Size = new System.Drawing.Size(75, 23);
            this.buttonLogin.TabIndex = 10;
            this.buttonLogin.Text = "Login";
            this.buttonLogin.UseVisualStyleBackColor = true;
            this.buttonLogin.Click += new System.EventHandler(this.buttonLogin_Click);
            // 
            // buttonStartTelem
            // 
            this.buttonStartTelem.Location = new System.Drawing.Point(392, 143);
            this.buttonStartTelem.Name = "buttonStartTelem";
            this.buttonStartTelem.Size = new System.Drawing.Size(112, 27);
            this.buttonStartTelem.TabIndex = 13;
            this.buttonStartTelem.Text = "Start auvsi_mp.py";
            this.buttonStartTelem.UseVisualStyleBackColor = true;
            this.buttonStartTelem.Click += new System.EventHandler(this.buttonStartTelem_Click);
            // 
            // textBoxTelem
            // 
            this.textBoxTelem.Location = new System.Drawing.Point(391, 216);
            this.textBoxTelem.Multiline = true;
            this.textBoxTelem.Name = "textBoxTelem";
            this.textBoxTelem.Size = new System.Drawing.Size(130, 113);
            this.textBoxTelem.TabIndex = 14;
            this.textBoxTelem.Text = "Nietesttowane: alternatywnie można wywoływać skrypty w pythonie do przesylania te" +
    "lemetriii- wymaga podania odpowiednich sciezek w kodzie.";
            // 
            // textBoxTelemHz
            // 
            this.textBoxTelemHz.Location = new System.Drawing.Point(430, 186);
            this.textBoxTelemHz.Name = "textBoxTelemHz";
            this.textBoxTelemHz.Size = new System.Drawing.Size(36, 20);
            this.textBoxTelemHz.TabIndex = 15;
            this.textBoxTelemHz.Text = "10";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(472, 189);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(20, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "Hz";
            // 
            // textBoxHTTPResult
            // 
            this.textBoxHTTPResult.Location = new System.Drawing.Point(12, 161);
            this.textBoxHTTPResult.Multiline = true;
            this.textBoxHTTPResult.Name = "textBoxHTTPResult";
            this.textBoxHTTPResult.Size = new System.Drawing.Size(318, 184);
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
            this.buttonPobierzMisje.Location = new System.Drawing.Point(12, 122);
            this.buttonPobierzMisje.Name = "buttonPobierzMisje";
            this.buttonPobierzMisje.Size = new System.Drawing.Size(88, 23);
            this.buttonPobierzMisje.TabIndex = 21;
            this.buttonPobierzMisje.Text = "Pobierz Misje";
            this.buttonPobierzMisje.UseVisualStyleBackColor = true;
            this.buttonPobierzMisje.Click += new System.EventHandler(this.buttonPobierzMisje_Click);
            // 
            // buttonPobierzPrzeszkody
            // 
            this.buttonPobierzPrzeszkody.Location = new System.Drawing.Point(106, 122);
            this.buttonPobierzPrzeszkody.Name = "buttonPobierzPrzeszkody";
            this.buttonPobierzPrzeszkody.Size = new System.Drawing.Size(121, 23);
            this.buttonPobierzPrzeszkody.TabIndex = 22;
            this.buttonPobierzPrzeszkody.Text = "Pobierz Przeszkody";
            this.buttonPobierzPrzeszkody.UseVisualStyleBackColor = true;
            this.buttonPobierzPrzeszkody.Click += new System.EventHandler(this.buttonPobierzPrzeszkody_Click);
            // 
            // buttonWyslijTelemetrie
            // 
            this.buttonWyslijTelemetrie.Location = new System.Drawing.Point(233, 122);
            this.buttonWyslijTelemetrie.Name = "buttonWyslijTelemetrie";
            this.buttonWyslijTelemetrie.Size = new System.Drawing.Size(97, 23);
            this.buttonWyslijTelemetrie.TabIndex = 23;
            this.buttonWyslijTelemetrie.Text = "Wyslij Telemetrie";
            this.buttonWyslijTelemetrie.UseVisualStyleBackColor = true;
            this.buttonWyslijTelemetrie.Click += new System.EventHandler(this.buttonWyslijTelemetrie_Click);
            // 
            // TelemetriaUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(604, 367);
            this.Controls.Add(this.buttonWyslijTelemetrie);
            this.Controls.Add(this.buttonPobierzPrzeszkody);
            this.Controls.Add(this.buttonPobierzMisje);
            this.Controls.Add(this.textBoxPassword);
            this.Controls.Add(this.textBoxUsername);
            this.Controls.Add(this.textBoxHTTPResult);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxTelemHz);
            this.Controls.Add(this.textBoxTelem);
            this.Controls.Add(this.buttonStartTelem);
            this.Controls.Add(this.buttonLogin);
            this.Controls.Add(this.textBoxURL);
            this.Name = "TelemetriaUI";
            this.Text = "Telemetria";
            this.Load += new System.EventHandler(this.TelemetriaUI_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox textBoxURL;
        private System.Windows.Forms.Button buttonLogin;
        private System.Windows.Forms.Button buttonStartTelem;
        private System.Windows.Forms.TextBox textBoxTelem;
        private System.Windows.Forms.TextBox textBoxTelemHz;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Timer timerTelem;
        private System.Windows.Forms.TextBox textBoxHTTPResult;
        private System.Windows.Forms.TextBox textBoxUsername;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.Button buttonPobierzMisje;
        private System.Windows.Forms.Button buttonPobierzPrzeszkody;
        private System.Windows.Forms.Button buttonWyslijTelemetrie;
    }
}

