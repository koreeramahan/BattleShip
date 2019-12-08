namespace BattleShipClient
{
    partial class EnemySelectionPanel
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// очистить использованные ресуры
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EnemySelectionPanel));
            this.LNick = new System.Windows.Forms.Label();
            this.BConnect = new System.Windows.Forms.Button();
            this.TBSearch = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.ExitButton = new System.Windows.Forms.Button();
            this.DGVAvailableEnemies = new System.Windows.Forms.DataGridView();
            this.IPandPort = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ENick = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DGVAvailableEnemies)).BeginInit();
            this.SuspendLayout();
            // 
            // LNick
            // 
            this.LNick.AutoSize = true;
            this.LNick.Font = new System.Drawing.Font("Segoe Print", 9F, System.Drawing.FontStyle.Bold);
            this.LNick.Location = new System.Drawing.Point(272, 8);
            this.LNick.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LNick.Name = "LNick";
            this.LNick.Size = new System.Drawing.Size(140, 31);
            this.LNick.TabIndex = 2;
            this.LNick.Text = "Поиск игрока";
            // 
            // BConnect
            // 
            this.BConnect.BackColor = System.Drawing.Color.Maroon;
            this.BConnect.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.BConnect.ForeColor = System.Drawing.Color.White;
            this.BConnect.Location = new System.Drawing.Point(273, 408);
            this.BConnect.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.BConnect.Name = "BConnect";
            this.BConnect.Size = new System.Drawing.Size(399, 35);
            this.BConnect.TabIndex = 6;
            this.BConnect.Text = "Выбрать игрока и играть!";
            this.BConnect.UseVisualStyleBackColor = false;
            this.BConnect.Click += new System.EventHandler(this.BConnect_Click);
            // 
            // TBSearch
            // 
            this.TBSearch.BackColor = System.Drawing.Color.White;
            this.TBSearch.Location = new System.Drawing.Point(423, 11);
            this.TBSearch.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TBSearch.MaxLength = 16;
            this.TBSearch.Name = "TBSearch";
            this.TBSearch.Size = new System.Drawing.Size(246, 26);
            this.TBSearch.TabIndex = 8;
            this.TBSearch.TextChanged += new System.EventHandler(this.SearchEnemy);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::BattleShipClient.Properties.Resources.iS31VJS9C;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Location = new System.Drawing.Point(0, -2);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(255, 495);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // ExitButton
            // 
            this.ExitButton.BackColor = System.Drawing.Color.Maroon;
            this.ExitButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.ExitButton.ForeColor = System.Drawing.Color.White;
            this.ExitButton.Location = new System.Drawing.Point(273, 453);
            this.ExitButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ExitButton.Name = "ExitButton";
            this.ExitButton.Size = new System.Drawing.Size(399, 35);
            this.ExitButton.TabIndex = 9;
            this.ExitButton.Text = "Выйти из игры";
            this.ExitButton.UseVisualStyleBackColor = false;
            this.ExitButton.Click += new System.EventHandler(this.ExitButton_Click);
            // 
            // DGVAvailableEnemies
            // 
            this.DGVAvailableEnemies.AllowUserToAddRows = false;
            this.DGVAvailableEnemies.AllowUserToDeleteRows = false;
            this.DGVAvailableEnemies.AllowUserToResizeRows = false;
            this.DGVAvailableEnemies.BackgroundColor = System.Drawing.SystemColors.ActiveCaption;
            this.DGVAvailableEnemies.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.DGVAvailableEnemies.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGVAvailableEnemies.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IPandPort,
            this.ENick});
            this.DGVAvailableEnemies.Location = new System.Drawing.Point(273, 57);
            this.DGVAvailableEnemies.MultiSelect = false;
            this.DGVAvailableEnemies.Name = "DGVAvailableEnemies";
            this.DGVAvailableEnemies.ReadOnly = true;
            this.DGVAvailableEnemies.RowHeadersVisible = false;
            this.DGVAvailableEnemies.RowTemplate.Height = 28;
            this.DGVAvailableEnemies.Size = new System.Drawing.Size(396, 343);
            this.DGVAvailableEnemies.TabIndex = 10;
            this.DGVAvailableEnemies.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGVAvailableEnemies_CellClick);
            // 
            // IPandPort
            // 
            this.IPandPort.HeaderText = "IPandPort";
            this.IPandPort.Name = "IPandPort";
            this.IPandPort.ReadOnly = true;
            this.IPandPort.Visible = false;
            // 
            // ENick
            // 
            this.ENick.HeaderText = "Онлайн игроки";
            this.ENick.Name = "ENick";
            this.ENick.ReadOnly = true;
            this.ENick.Width = 249;
            // 
            // EnemySelectionPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(685, 493);
            this.Controls.Add(this.DGVAvailableEnemies);
            this.Controls.Add(this.ExitButton);
            this.Controls.Add(this.TBSearch);
            this.Controls.Add(this.BConnect);
            this.Controls.Add(this.LNick);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.Name = "EnemySelectionPanel";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Морской бой - выбор игрока";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CloseApp);
            this.Load += new System.EventHandler(this.EnemySelectionPanel_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DGVAvailableEnemies)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label LNick;
        private System.Windows.Forms.Button BConnect;
        private System.Windows.Forms.TextBox TBSearch;
        private System.Windows.Forms.Button ExitButton;
        public System.Windows.Forms.DataGridView DGVAvailableEnemies;
        private System.Windows.Forms.DataGridViewTextBoxColumn IPandPort;
        private System.Windows.Forms.DataGridViewTextBoxColumn ENick;
    }
}