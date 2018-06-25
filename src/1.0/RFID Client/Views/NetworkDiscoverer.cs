using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using RFID_Client;
using GEV.Layouts;
using GEV.Falcon.RFID.Client;
using System.Net;
using System.IO;
using GEV.Falcon.RFID.Client.Discovery;

namespace GEV.Falcon.RFID.Views
{
    public partial class NetworkDiscovery : UserControl
    {
        public NetworkDiscovery()
        {
            InitializeComponent();

            if (!this.DesignMode)
            {
                foreach (ReaderStation r in MainWindow.Config.LastReaderStations)
                {
                    int i = this.dgNetwork.Rows.Add("■", r.IP, r.StationID, r.DisplayName);
                    this.dgNetwork.Rows[i].Cells[0].Style.ForeColor = GCLColors.AlertYellow;
                }
            }
        }

        private async void gclButton1_Click(object sender, EventArgs e)
        {

            this.lmProgress.Value = 0;
            this.lmProgress.Title = "Hálózati felderítés folyamatban...";

            foreach (DataGridViewRow row in this.dgNetwork.Rows)
            {
                row.Cells[0].Style.ForeColor = GCLColors.AlertYellow;
            }

            for (int i = 1; i < 255; i++)
            {
                NetworkDiscoverer disco = new NetworkDiscoverer()
                {
                    Address = String.Format("http://" + MainWindow.Config.Network + ":8080/falcon_rfid_disco.gev", i),
                    ID = i
                };
                disco.DiscoverySuccess += OnSuccess;
                disco.DiscoveryFailed += OnFail;

                disco.Start();
            }


            foreach (DataGridViewRow row in this.dgNetwork.Rows)
            {
                if (row.Cells[0].Style.ForeColor != GCLColors.SuccessGreen)
                {
                    row.Cells[0].Style.ForeColor = GCLColors.ErrorRed;
                }
            }

            foreach(ReaderStation r in MainWindow.Config.LastReaderStations)
            {
                r.Status = ReaderStation.Statuses.NotKnown;
            }

        }

        private void OnFail(object sender, EventArgs e)
        {
            this.lmProgress.Value++;

            this.CheckComplete();
        }

        private void OnSuccess(object sender, string[] e)
        {
            this.BeginInvoke(new Action(() =>
            {
                this.lmProgress.Value++;
                int ID = (sender as NetworkDiscoverer).ID;

                ReaderStation station = MainWindow.Config.LastReaderStations.FirstOrDefault(s => s.StationID == e[0]);
                if (station != null)
                {
                    foreach (DataGridViewRow row in this.dgNetwork.Rows)
                    {
                        if (row.Cells[2].Value != null && row.Cells[2].Value.ToString() == station.StationID)
                        {
                            row.Cells[0].Style.ForeColor = GCLColors.SuccessGreen;
                            row.Cells[1].Value = String.Format(MainWindow.Config.Network, ID);
                            station.IP = String.Format(MainWindow.Config.Network, ID);
                            station.Status = ReaderStation.Statuses.Online;
                        }
                    }
                }
                else
                {
                    MainWindow.Config.LastReaderStations.Add(new ReaderStation()
                    {
                        IP = String.Format(MainWindow.Config.Network, ID),
                        StationID = e[0],
                        DisplayName = e[1],
                        Status = ReaderStation.Statuses.Online
                    });
                    int row = this.dgNetwork.Rows.Add("■", String.Format(MainWindow.Config.Network, ID), e[0], e[1]);
                    this.dgNetwork.Rows[row].Cells[0].Style.ForeColor = GCLColors.SuccessGreen;
                }
            }));

            this.CheckComplete();
        }

        private void CheckComplete()
        {
            if(this.lmProgress.Value >= 253)
            {
                this.BeginInvoke(new Action(() =>
                {
                    this.lmProgress.Title = "Hálózati felderítés kész.";
                    MainWindow.Config.Save("config.xml");
                }));
            }
        }
    }
}
