namespace GEV.Falcon.RFID.Views
{
    partial class Query
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.gclButton1 = new GEV.Layouts.GCLButton();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.gclComboBox1 = new GEV.Layouts.GCLComboBox();
            this.cairoSpreadsheet1 = new GEV.Layouts.Extended.Cairo.CairoSpreadsheet();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.gclButton1);
            this.panel1.Controls.Add(this.dateTimePicker1);
            this.panel1.Controls.Add(this.dateTimePicker2);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 25);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(812, 32);
            this.panel1.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(2, 7);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Kezdés:";
            // 
            // gclButton1
            // 
            this.gclButton1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(76)))), ((int)(((byte)(79)))));
            this.gclButton1.Checked = false;
            this.gclButton1.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(62)))), ((int)(((byte)(65)))));
            this.gclButton1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(134)))), ((int)(((byte)(171)))));
            this.gclButton1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(158)))), ((int)(((byte)(202)))));
            this.gclButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gclButton1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.gclButton1.Location = new System.Drawing.Point(418, 1);
            this.gclButton1.Margin = new System.Windows.Forms.Padding(2);
            this.gclButton1.Name = "gclButton1";
            this.gclButton1.Size = new System.Drawing.Size(81, 26);
            this.gclButton1.TabIndex = 5;
            this.gclButton1.Text = "Lekérdezés";
            this.gclButton1.UseThemeColors = true;
            this.gclButton1.UseVisualStyleBackColor = false;
            this.gclButton1.Click += new System.EventHandler(this.gclButton1_Click);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CustomFormat = "yyyy. MM. dd. hh:mm:ss";
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker1.Location = new System.Drawing.Point(51, 5);
            this.dateTimePicker1.Margin = new System.Windows.Forms.Padding(2);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(152, 20);
            this.dateTimePicker1.TabIndex = 0;
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.CustomFormat = "yyyy. MM. dd. hh:mm:ss";
            this.dateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker2.Location = new System.Drawing.Point(264, 5);
            this.dateTimePicker2.Margin = new System.Windows.Forms.Padding(2);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(151, 20);
            this.dateTimePicker2.TabIndex = 1;
            this.dateTimePicker2.Value = new System.DateTime(2018, 5, 15, 0, 0, 0, 0);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(226, 7);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Vége:";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.gclComboBox1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(812, 25);
            this.panel2.TabIndex = 9;
            // 
            // gclComboBox1
            // 
            this.gclComboBox1.BackColor = System.Drawing.Color.DarkGray;
            this.gclComboBox1.DrawMode = System.Windows.Forms.DrawMode.Normal;
            this.gclComboBox1.DropDownHeight = 0;
            this.gclComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.gclComboBox1.DropDownWidth = 0;
            this.gclComboBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.gclComboBox1.IsDroppedDown = false;
            this.gclComboBox1.ItemHeight = 13;
            this.gclComboBox1.Location = new System.Drawing.Point(4, 2);
            this.gclComboBox1.Margin = new System.Windows.Forms.Padding(2);
            this.gclComboBox1.MaxDropDownItems = 0;
            this.gclComboBox1.Name = "gclComboBox1";
            this.gclComboBox1.SelectedIndex = -1;
            this.gclComboBox1.SelectedItem = null;
            this.gclComboBox1.SimpleItems = null;
            this.gclComboBox1.Size = new System.Drawing.Size(293, 21);
            this.gclComboBox1.Soreted = false;
            this.gclComboBox1.TabIndex = 0;
            this.gclComboBox1.Text = "gclComboBox1";
            // 
            // cairoSpreadsheet1
            // 
            this.cairoSpreadsheet1.BackColor = System.Drawing.Color.White;
            this.cairoSpreadsheet1.ColumnHeaderContextMenuStrip = null;
            this.cairoSpreadsheet1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cairoSpreadsheet1.LeadHeaderContextMenuStrip = null;
            this.cairoSpreadsheet1.Location = new System.Drawing.Point(0, 57);
            this.cairoSpreadsheet1.Name = "cairoSpreadsheet1";
            this.cairoSpreadsheet1.RowHeaderContextMenuStrip = null;
            this.cairoSpreadsheet1.Script = null;
            this.cairoSpreadsheet1.SheetTabContextMenuStrip = null;
            this.cairoSpreadsheet1.SheetTabNewButtonVisible = true;
            this.cairoSpreadsheet1.SheetTabVisible = false;
            this.cairoSpreadsheet1.SheetTabWidth = 60;
            this.cairoSpreadsheet1.ShowScrollEndSpacing = true;
            this.cairoSpreadsheet1.Size = new System.Drawing.Size(812, 433);
            this.cairoSpreadsheet1.TabIndex = 10;
            this.cairoSpreadsheet1.Text = "cairoSpreadsheet1";
            // 
            // Query
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(84)))), ((int)(((byte)(86)))));
            this.Controls.Add(this.cairoSpreadsheet1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Query";
            this.Size = new System.Drawing.Size(812, 490);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private Layouts.GCLButton gclButton1;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel2;
        private Layouts.GCLComboBox gclComboBox1;
        public Layouts.Extended.Cairo.CairoSpreadsheet cairoSpreadsheet1;
    }
}
