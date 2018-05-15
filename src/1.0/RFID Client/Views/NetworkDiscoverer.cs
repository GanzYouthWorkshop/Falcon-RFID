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

namespace GEV.Falcon.RFID.Views
{
    public partial class NetworkDiscoverer : UserControl
    {
        public NetworkDiscoverer()
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
            HttpClient client = new HttpClient();
            client.Timeout = new TimeSpan(0, 0, 0, 0, 75);

            this.lmProgress.Value = 0;
            this.lmProgress.Title = "Hálózati felderítés folyamatban...";

            foreach (DataGridViewRow row in this.dgNetwork.Rows)
            {
                row.Cells[0].Style.ForeColor = GCLColors.AlertYellow;
            }

            for (int i = 1; i < 255; i++)
            {
                try
                {
                    string responseString = await client.GetStringAsync(String.Format("http://" + MainWindow.Config.Network + ":8080/falcon_rfid_disco.gev", i));
                    string[] values = responseString.Split('-');


                    ReaderStation station = MainWindow.Config.LastReaderStations.FirstOrDefault(s => s.StationID == values[0]);
                    if(station != null)
                    {
                        foreach(DataGridViewRow row in this.dgNetwork.Rows)
                        {
                            if(row.Cells[2].Value.ToString() == station.StationID)
                            {
                                row.Cells[0].Style.ForeColor = GCLColors.SuccessGreen;
                                row.Cells[1].Value = String.Format(MainWindow.Config.Network, i);
                                station.IP = String.Format(MainWindow.Config.Network, i);
                                station.Status = ReaderStation.Statuses.Online;
                            }
                        }
                    }
                    else
                    {
                        MainWindow.Config.LastReaderStations.Add(new ReaderStation()
                        {
                            IP = String.Format(MainWindow.Config.Network, i),
                            StationID = values[0],
                            DisplayName = values[1],
                            Status = ReaderStation.Statuses.Online
                        });
                        int row = this.dgNetwork.Rows.Add("■", String.Format(MainWindow.Config.Network, i), values[0], values[1]);
                        this.dgNetwork.Rows[row].Cells[0].Style.ForeColor = GCLColors.SuccessGreen;
                    }
                }
                catch (Exception) { }
                this.lmProgress.Value++;
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

            this.lmProgress.Title = "Hálózati felderítés kész.";
            MainWindow.Config.Save("config.xml");
        }
    }
}
