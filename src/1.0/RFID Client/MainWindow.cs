using GEV.Falcon.RFID;
using GEV.Falcon.RFID.Client;
using GEV.Falcon.RFID.Utils;
using GEV.Layouts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RFID_Client
{
    public partial class MainWindow : GCLForm
    {
        public static Config Config { get; set; } = new Config();

        public MainWindow()
        {
            Config = Config.Load("config.xml");

            InitializeComponent();

            this.mainMenu1.btnSettings.Click += GclButton2_Click;
            this.mainMenu1.btnLog.Click += GclButton4_Click;
            this.mainMenu1.btnCards.Click += GclButton3_Click;
            this.mainMenu1.btnAbout.Click += GclButton5_Click;
            this.mainMenu1.btnStatistics.Click += BtnStatistics_Click;

            this.cardHolders1.Init();
        }

        private void BtnStatistics_Click(object sender, EventArgs e)
        {
            this.gclTabControl1.SelectedTab = this.tabQuery;
        }

        private void GclButton5_Click(object sender, EventArgs e)
        {
            this.gclTabControl1.SelectedTab = this.tabAbout;
        }

        private void GclButton3_Click(object sender, EventArgs e)
        {
            this.gclTabControl1.SelectedTab = this.tabCards;
        }

        private void GclButton4_Click(object sender, EventArgs e)
        {
            this.gclTabControl1.SelectedTab = this.tabLog;
        }

        private void GclButton2_Click(object sender, EventArgs e)
        {
            this.gclTabControl1.SelectedTab = this.tabSettings;
        }

        private void OnBackToMainMenu(object sender, EventArgs e)
        {
            this.gclTabControl1.SelectedTab = this.tabMain;
        }

        private void gclButton1_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.FileName = "kimutatás.csv";
            sfd.Filter = ".csv|CSV fájlok";
            if(sfd.ShowDialog() == DialogResult.OK)
            {
                DataGridExport.ExportDataGridAsCSV(this.query1.gclDataGrid1, sfd.FileName);
            }
        }

        private void gclButton2_Click_1(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.FileName = "log.csv";
            sfd.Filter = ".csv|CSV fájlok";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                DataGridExport.ExportDataGridAsCSV(this.log1.gclDataGrid1, sfd.FileName);
            }
        }
    }
}
