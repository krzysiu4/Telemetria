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
            this.buttonPobierzPrzeszkody = new System.Windows.Forms.Button();
            this.buttonWysylajTelemetrie = new System.Windows.Forms.Button();
            this.textBoxTelem = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonWyswietlPrzeszkody = new System.Windows.Forms.Button();
            this.buttonUsunPrzeszkody = new System.Windows.Forms.Button();
            this.buttonWyswietlPrzeszkodyRuchome = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
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
            // buttonWysylajTelemetrie
            // 
            this.buttonWysylajTelemetrie.BackColor = System.Drawing.Color.Green;
            this.buttonWysylajTelemetrie.Location = new System.Drawing.Point(411, 174);
            this.buttonWysylajTelemetrie.Name = "buttonWysylajTelemetrie";
            this.buttonWysylajTelemetrie.Size = new System.Drawing.Size(76, 23);
            this.buttonWysylajTelemetrie.TabIndex = 23;
            this.buttonWysylajTelemetrie.Text = "START";
            this.buttonWysylajTelemetrie.UseVisualStyleBackColor = false;
            this.buttonWysylajTelemetrie.Click += new System.EventHandler(this.buttonWysylajTelemetrie_Click);
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(421, 146);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 24;
            this.label1.Text = "Telemetria";
            // 
            // buttonWyswietlPrzeszkody
            // 
            this.buttonWyswietlPrzeszkody.Location = new System.Drawing.Point(233, 122);
            this.buttonWyswietlPrzeszkody.Name = "buttonWyswietlPrzeszkody";
            this.buttonWyswietlPrzeszkody.Size = new System.Drawing.Size(128, 23);
            this.buttonWyswietlPrzeszkody.TabIndex = 25;
            this.buttonWyswietlPrzeszkody.Text = "Wyswietl Przeszkody";
            this.buttonWyswietlPrzeszkody.UseVisualStyleBackColor = true;
            this.buttonWyswietlPrzeszkody.Click += new System.EventHandler(this.buttonWyswietlPrzeszkody_Click);
            // 
            // buttonUsunPrzeszkody
            // 
            this.buttonUsunPrzeszkody.Location = new System.Drawing.Point(233, 78);
            this.buttonUsunPrzeszkody.Name = "buttonUsunPrzeszkody";
            this.buttonUsunPrzeszkody.Size = new System.Drawing.Size(128, 23);
            this.buttonUsunPrzeszkody.TabIndex = 26;
            this.buttonUsunPrzeszkody.Text = "Usun Przeszkody";
            this.buttonUsunPrzeszkody.UseVisualStyleBackColor = true;
            this.buttonUsunPrzeszkody.Click += new System.EventHandler(this.buttonUsunPrzeszkody_Click);
            // 
            // buttonWyswietlPrzeszkodyRuchome
            // 
            this.buttonWyswietlPrzeszkodyRuchome.Location = new System.Drawing.Point(411, 55);
            this.buttonWyswietlPrzeszkodyRuchome.Name = "buttonWyswietlPrzeszkodyRuchome";
            this.buttonWyswietlPrzeszkodyRuchome.Size = new System.Drawing.Size(134, 23);
            this.buttonWyswietlPrzeszkodyRuchome.TabIndex = 27;
            this.buttonWyswietlPrzeszkodyRuchome.Text = "START";
            this.buttonWyswietlPrzeszkodyRuchome.UseVisualStyleBackColor = true;
            this.buttonWyswietlPrzeszkodyRuchome.Click += new System.EventHandler(this.buttonWyswietlPrzeszkodyRuchome_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(431, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(105, 13);
            this.label2.TabIndex = 28;
            this.label2.Text = "Przeszkody ruchome";
            // 
            // TelemetriaUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(604, 367);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.buttonWyswietlPrzeszkodyRuchome);
            this.Controls.Add(this.buttonUsunPrzeszkody);
            this.Controls.Add(this.buttonWyswietlPrzeszkody);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonWysylajTelemetrie);
            this.Controls.Add(this.buttonPobierzPrzeszkody);
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
        private System.Windows.Forms.Button buttonPobierzPrzeszkody;
        private System.Windows.Forms.Button buttonWysylajTelemetrie;
        private System.Windows.Forms.TextBox textBoxTelem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonWyswietlPrzeszkody;
        private System.Windows.Forms.Button buttonUsunPrzeszkody;
        private System.Windows.Forms.Button buttonWyswietlPrzeszkodyRuchome;
        private System.Windows.Forms.Label label2;
    }
}

