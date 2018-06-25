namespace Falcon_RFID_GUI
{
    partial class MainForm
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
            this.gclWindowHeader1 = new GEV.Layouts.GCLWindowHeader();
            this.gclTextbox1 = new GEV.Layouts.GCLTextbox();
            this.label1 = new System.Windows.Forms.Label();
            this.gclPanel1 = new GEV.Layouts.GCLPanel();
            this.gclPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gclWindowHeader1
            // 
            this.gclWindowHeader1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.gclWindowHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            this.gclWindowHeader1.Location = new System.Drawing.Point(1, 1);
            this.gclWindowHeader1.Margin = new System.Windows.Forms.Padding(2);
            this.gclWindowHeader1.Name = "gclWindowHeader1";
            this.gclWindowHeader1.ShowCloseButton = true;
            this.gclWindowHeader1.ShowWindowButtons = false;
            this.gclWindowHeader1.Size = new System.Drawing.Size(649, 32);
            this.gclWindowHeader1.TabIndex = 999;
            this.gclWindowHeader1.TabStop = false;
            this.gclWindowHeader1.Title = "Falcon RFID";
            // 
            // gclTextbox1
            // 
            this.gclTextbox1.ActiveBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(134)))), ((int)(((byte)(171)))));
            this.gclTextbox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(71)))), ((int)(((byte)(74)))));
            this.gclTextbox1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(62)))), ((int)(((byte)(65)))));
            this.gclTextbox1.Font = new System.Drawing.Font("Segoe UI Light", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.gclTextbox1.Location = new System.Drawing.Point(105, 147);
            this.gclTextbox1.Margin = new System.Windows.Forms.Padding(2);
            this.gclTextbox1.Name = "gclTextbox1";
            this.gclTextbox1.Padding = new System.Windows.Forms.Padding(2);
            this.gclTextbox1.Size = new System.Drawing.Size(411, 41);
            this.gclTextbox1.TabIndex = 0;
            this.gclTextbox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.gclTextbox1.UseSystemPasswordChar = false;
            this.gclTextbox1.UseThemeColors = true;
            this.gclTextbox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.gclTextbox1_KeyPress);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(84)))), ((int)(((byte)(86)))));
            this.label1.Font = new System.Drawing.Font("Segoe UI Light", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(13, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(603, 98);
            this.label1.TabIndex = 2;
            this.label1.Text = "Kártya leolvasható...";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // gclPanel1
            // 
            this.gclPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(84)))), ((int)(((byte)(86)))));
            this.gclPanel1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(55)))), ((int)(((byte)(55)))));
            this.gclPanel1.Controls.Add(this.label1);
            this.gclPanel1.Controls.Add(this.gclTextbox1);
            this.gclPanel1.Location = new System.Drawing.Point(11, 45);
            this.gclPanel1.Margin = new System.Windows.Forms.Padding(10);
            this.gclPanel1.Name = "gclPanel1";
            this.gclPanel1.Padding = new System.Windows.Forms.Padding(10);
            this.gclPanel1.Size = new System.Drawing.Size(629, 233);
            this.gclPanel1.TabIndex = 1000;
            this.gclPanel1.UseThemeColors = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(651, 290);
            this.Controls.Add(this.gclPanel1);
            this.Controls.Add(this.gclWindowHeader1);
            this.Name = "MainForm";
            this.Text = "Falcon RFID";
            this.gclPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private GEV.Layouts.GCLWindowHeader gclWindowHeader1;
        private GEV.Layouts.GCLTextbox gclTextbox1;
        private System.Windows.Forms.Label label1;
        private GEV.Layouts.GCLPanel gclPanel1;
    }
}

