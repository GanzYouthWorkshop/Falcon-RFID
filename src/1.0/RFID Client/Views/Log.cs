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
using GEV.Falcon.RFID;
using System.Xml.Serialization;
using System.IO;
using RFID_Client;
using GEV.Falcon.RFID.Client;

namespace GEV.Falcon.RFID.Views
{
    public partial class Log : UserControl
    {
        public Log()
        {
            InitializeComponent();

            this.dateTimePicker1.Value = DateTime.Now.AddMonths(-1);
            this.dateTimePicker2.Value = DateTime.Now;
        }

        private async void gclButton1_Click(object sender, EventArgs e)
        {
            List<CheckinEntry> allCheckins = ReaderStation.QueryAll(this.dateTimePicker1.Value, this.dateTimePicker2.Value);

            this.gclDataGrid1.Rows.Clear();
            foreach (CheckinEntry entry in allCheckins)
            {
                CardHolder card = MainWindow.Config.CardHolders[entry.Card];
                string worker = "-- ismeretlen --";

                if (card != null)
                {
                    worker = card.Worker;
                }

                this.gclDataGrid1.Rows.Add(entry.Date.ToString("yyyy. MM. dd. hh:mm:ss"), entry.Card, worker);
            }
        }
    }
}
