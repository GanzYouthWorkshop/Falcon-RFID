using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GEV.Layouts;
using RFID_Client;
using GEV.Falcon.RFID.Client;
using GEV.Layouts.Extended.Cairo;
using GEV.Falcon.RFID.Client.Queries;

namespace GEV.Falcon.RFID.Views
{
    public partial class Query : UserControl
    {
        public Query()
        {
            InitializeComponent();

            this.gclComboBox1.DataSource = new string[]
            {
                "Napi első és utolsó bejelentkezés",
                "Összesítés"
            };

            this.dateTimePicker1.Value = DateTime.Now.AddMonths(-1);
            this.dateTimePicker2.Value = DateTime.Now;
        }

        private void gclButton1_Click(object sender, EventArgs e)
        {
            List<CheckinEntry> data = ReaderStation.QueryAll(this.dateTimePicker1.Value, this.dateTimePicker2.Value);

            IQuery query = null;
            switch (gclComboBox1.SelectedItem.ToString())
            {
                case "Napi első és utolsó bejelentkezés":
                    query = new DailyFirstLast();
                    break;
                case "Összesítés":
                    query = new MonthlySummary();
                    break;
            }

            if (query != null)
            {
                query.Sheet = this.cairoSpreadsheet1;
                query.Start = this.dateTimePicker1.Value;
                query.End = this.dateTimePicker2.Value;
                query.Data = data;

                query.Run();
            }
        }

    }
}
