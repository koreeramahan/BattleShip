namespace BattleShipClient
{
    partial class ServerConnectionPanel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ServerConnectionPanel));
            this.LNick = new System.Windows.Forms.Label();
            this.LServerIP = new System.Windows.Forms.Label();
            this.TBNick = new System.Windows.Forms.TextBox();
            this.TBServerIP = new System.Windows.Forms.TextBox();
            this.BConnect = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.RulesButton = new System.Windows.Forms.Button();
            this.ExitButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // LNick
            // 
            this.LNick.AutoSize = true;
            this.LNick.Font = new System.Drawing.Font("Segoe Print", 9F, System.Drawing.FontStyle.Bold);
            this.LNick.Location = new System.Drawing.Point(261, 98);
            this.LNick.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LNick.Name = "LNick";
            this.LNick.Size = new System.Drawing.Size(185, 31);
            this.LNick.TabIndex = 1;
            this.LNick.Text = "Придумайте имя:";
            // 
            // LServerIP
            // 
            this.LServerIP.AutoSize = true;
            this.LServerIP.Font = new System.Drawing.Font("Segoe Print", 9F, System.Drawing.FontStyle.Bold);
            this.LServerIP.Location = new System.Drawing.Point(330, 150);
            this.LServerIP.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LServerIP.Name = "LServerIP";
            this.LServerIP.Size = new System.Drawing.Size(116, 31);
            this.LServerIP.TabIndex = 2;
            this.LServerIP.Text = "IP сервера:";
            // 
            // TBNick
            // 
            this.TBNick.Location = new System.Drawing.Point(454, 103);
            this.TBNick.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TBNick.MaxLength = 16;
            this.TBNick.Name = "TBNick";
            this.TBNick.Size = new System.Drawing.Size(176, 26);
            this.TBNick.TabIndex = 3;
            this.TBNick.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.EnterClicked);
            // 
            // TBServerIP
            // 
            this.TBServerIP.Location = new System.Drawing.Point(454, 153);
            this.TBServerIP.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TBServerIP.Name = "TBServerIP";
            this.TBServerIP.Size = new System.Drawing.Size(176, 26);
            this.TBServerIP.TabIndex = 4;
            this.TBServerIP.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.EnterClicked);
            // 
            // BConnect
            // 
            this.BConnect.BackColor = System.Drawing.Color.Maroon;
            this.BConnect.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.BConnect.ForeColor = System.Drawing.Color.White;
            this.BConnect.Location = new System.Drawing.Point(361, 198);
            this.BConnect.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.BConnect.Name = "BConnect";
            this.BConnect.Size = new System.Drawing.Size(178, 35);
            this.BConnect.TabIndex = 5;
            this.BConnect.Text = "Присоединиться";
            this.BConnect.UseVisualStyleBackColor = false;
            this.BConnect.Click += new System.EventHandler(this.BConnect_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::BattleShipClient.Properties.Resources.iS31VJS9C;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Location = new System.Drawing.Point(-2, 0);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(255, 468);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // RulesButton
            // 
            this.RulesButton.BackColor = System.Drawing.Color.Maroon;
            this.RulesButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.RulesButton.ForeColor = System.Drawing.Color.White;
            this.RulesButton.Location = new System.Drawing.Point(361, 334);
            this.RulesButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.RulesButton.Name = "RulesButton";
            this.RulesButton.Size = new System.Drawing.Size(178, 35);
            this.RulesButton.TabIndex = 6;
            this.RulesButton.Text = "Правила игры";
            this.RulesButton.UseVisualStyleBackColor = false;
            this.RulesButton.Click += new System.EventHandler(this.RulesButton_Click);
            // 
            // ExitButton
            // 
            this.ExitButton.BackColor = System.Drawing.Color.Maroon;
            this.ExitButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.ExitButton.ForeColor = System.Drawing.Color.White;
            this.ExitButton.Location = new System.Drawing.Point(361, 379);
            this.ExitButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ExitButton.Name = "ExitButton";
            this.ExitButton.Size = new System.Drawing.Size(178, 35);
            this.ExitButton.TabIndex = 7;
            this.ExitButton.Text = "Выйти из игры";
            this.ExitButton.UseVisualStyleBackColor = false;
            this.ExitButton.Click += new System.EventHandler(this.ExitButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe Print", 9F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(370, 38);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(197, 31);
            this.label1.TabIndex = 8;
            this.label1.Text = "Добро пожаловать";
            // 
            // ServerConnectionPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(669, 468);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ExitButton);
            this.Controls.Add(this.RulesButton);
            this.Controls.Add(this.BConnect);
            this.Controls.Add(this.TBServerIP);
            this.Controls.Add(this.TBNick);
            this.Controls.Add(this.LServerIP);
            this.Controls.Add(this.LNick);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.Name = "ServerConnectionPanel";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Морской бой - соединение";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label LNick;
        private System.Windows.Forms.Label LServerIP;
        private System.Windows.Forms.TextBox TBNick;
        private System.Windows.Forms.TextBox TBServerIP;
        private System.Windows.Forms.Button BConnect;
        private System.Windows.Forms.Button RulesButton;
        private System.Windows.Forms.Button ExitButton;
        private System.Windows.Forms.Label label1;
    }
}