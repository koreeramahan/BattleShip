namespace BattleShipClient
{
    partial class OfferingGame
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OfferingGame));
            this.label2 = new System.Windows.Forms.Label();
            this.BYes = new System.Windows.Forms.Button();
            this.BNo = new System.Windows.Forms.Button();
            this.CBEneNicks = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe Print", 9F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(274, 88);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(260, 31);
            this.label2.TabIndex = 5;
            this.label2.Text = "С тобой хотят сыграть,";
            // 
            // BYes
            // 
            this.BYes.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.BYes.Location = new System.Drawing.Point(284, 332);
            this.BYes.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.BYes.Name = "BYes";
            this.BYes.Size = new System.Drawing.Size(174, 35);
            this.BYes.TabIndex = 7;
            this.BYes.Text = "Начать игру";
            this.BYes.UseVisualStyleBackColor = false;
            this.BYes.Click += new System.EventHandler(this.BYes_Click);
            // 
            // BNo
            // 
            this.BNo.BackColor = System.Drawing.Color.Tomato;
            this.BNo.Location = new System.Drawing.Point(483, 332);
            this.BNo.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.BNo.Name = "BNo";
            this.BNo.Size = new System.Drawing.Size(174, 35);
            this.BNo.TabIndex = 8;
            this.BNo.Text = "Отклонить";
            this.BNo.UseVisualStyleBackColor = false;
            this.BNo.Click += new System.EventHandler(this.BNo_Click);
            // 
            // CBEneNicks
            // 
            this.CBEneNicks.FormattingEnabled = true;
            this.CBEneNicks.Location = new System.Drawing.Point(280, 157);
            this.CBEneNicks.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.CBEneNicks.Name = "CBEneNicks";
            this.CBEneNicks.Size = new System.Drawing.Size(374, 28);
            this.CBEneNicks.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe Print", 9F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(274, 120);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(191, 31);
            this.label1.TabIndex = 10;
            this.label1.Text = "Выбери соперника!";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::BattleShipClient.Properties.Resources.iS31VJS9C;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Location = new System.Drawing.Point(0, -2);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(255, 468);
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // OfferingGame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(688, 462);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CBEneNicks);
            this.Controls.Add(this.BNo);
            this.Controls.Add(this.BYes);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OfferingGame";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Морской бой - Давай сыграем!";
            this.Load += new System.EventHandler(this.OfferingGame_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button BYes;
        private System.Windows.Forms.Button BNo;
        private System.Windows.Forms.ComboBox CBEneNicks;
        private System.Windows.Forms.Label label1;
    }
}