using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RFID_Client;
using GEV.Falcon.RFID.Client;

namespace GEV.Falcon.RFID.Views
{
    public partial class CardHolders : UserControl
    {
        public CardHolders()
        {
            InitializeComponent();
        }

        public void Init()
        {
            foreach (CardHolder item in MainWindow.Config.CardHolders)
            {
                this.gclDataGrid1.Rows.Add(item.CardNumber, item.Worker, item.Department);
            }

            this.gclDataGrid1.CellValueChanged += gclDataGrid1_CellValueChanged;
        }

        private void gclDataGrid1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            MainWindow.Config.CardHolders.Clear();

            foreach (DataGridViewRow row in this.gclDataGrid1.Rows)
            {
                string s1 = row.Cells[1].Value != null ? row.Cells[0].Value.ToString() : "";
                string s2 = row.Cells[1].Value != null ? row.Cells[1].Value.ToString() : "";
                string s3 = row.Cells[1].Value != null ? row.Cells[2].Value as string : "";

                if (s1 == "" || s2 == "")
                {
                    continue;
                }

                MainWindow.Config.CardHolders.Add(new CardHolder()
                {
                    CardNumber = s1,
                    Worker = s2,
                    Department = s3
                });
            }

            MainWindow.Config.Save("config.xml");
        }
    }
}
