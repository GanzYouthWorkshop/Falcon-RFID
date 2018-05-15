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

namespace GEV.Falcon.RFID.Views
{
    public partial class Query : UserControl
    {
        public Query()
        {
            InitializeComponent();

            this.gclComboBox1.DataSource = new string[]
            {
                "Napi első és utolsó bejelentkezés"
            };

            this.dateTimePicker1.Value = DateTime.Now.AddMonths(-1);
            this.dateTimePicker2.Value = DateTime.Now;
        }

        private void gclButton1_Click(object sender, EventArgs e)
        {
            #region becsekkolások
            this.SetupGataGrid(new List<string>()
            {
                "",
                "Kártyatulajdonos",
                "Munkanap",
                "Megérkezés",
                "Távozás",
                "Munkaidő",
                "Túlóra"
            }, 1);

            List<CheckinEntry> list = ReaderStation.QueryAll(this.dateTimePicker1.Value, this.dateTimePicker2.Value);

            var byDay = list.GroupBy(item => item.Date.DayOfYear);
            foreach(var group in byDay)
            {
                string workDay = group.Key.ToString();

                var byWorker = group.GroupBy(i => i.Card);
                foreach(var worker in byWorker)
                {
                    DateTime min = worker.Min(item => item.Date);
                    DateTime max = worker.Max(item => item.Date);
                    TimeSpan workTime = max.Subtract(min);

                    string maxStr = "---";
                    string workTimeStr = "---";
                    string overTimeStr = "---";

                    if (max != min)
                    {
                        maxStr = max.ToString("HH:mm");
                        TimeSpan overTime = workTime.Subtract(new TimeSpan(0, 8, 0, 0, 0));

                        workTimeStr = String.Format("{0:hh} óra {0:mm} perc", workTime);

                        if (workTime > new TimeSpan(0, 8, 0, 0, 0))
                        {
                            overTimeStr = String.Format("{0:hh} óra {0:mm} perc", overTime);
                        }
                    }


                    CardHolder card = MainWindow.Config.CardHolders[worker.First().Card];
                    string workerStr = "-- ismeretlen --";

                    if (card != null)
                    {
                        workerStr = card.Worker;
                    }

                    int lastRow = this.gclDataGrid1.Rows.Add(
                        "■",
                        workerStr,
                        min.ToString("yyyy. MMMM dd."),
                        min.ToString("HH:mm"),
                        maxStr,
                        workTimeStr,
                        overTimeStr
                      );


                    if(workTime > new TimeSpan(0, 8, 5, 0, 0))
                    {
                        this.gclDataGrid1.Rows[lastRow].Cells[0].Style.ForeColor = GCLColors.AccentColor1Highlight;
                    }
                    else if (workTime > new TimeSpan(0, 7, 55, 0, 0))
                    {
                        this.gclDataGrid1.Rows[lastRow].Cells[0].Style.ForeColor = GCLColors.SuccessGreen;
                    }
                    else if (workTime > new TimeSpan(0, 7, 45, 0, 0))
                    {
                        this.gclDataGrid1.Rows[lastRow].Cells[0].Style.ForeColor = GCLColors.AlertYellow;
                    }
                    else
                    {
                        this.gclDataGrid1.Rows[lastRow].Cells[0].Style.ForeColor = GCLColors.ErrorRed;
                    }
                }
            }
            #endregion
        }

        private void SetupGataGrid(List<string> columns, int rowFilling)
        {
            this.gclDataGrid1.Rows.Clear();
            this.gclDataGrid1.Columns.Clear();

            foreach (string s in columns)
            {
                this.gclDataGrid1.Columns.Add(new DataGridViewTextBoxColumn()
                {
                    HeaderText = s,
                    ReadOnly = true,
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                });
            }

            this.gclDataGrid1.Columns[rowFilling].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }
    }
}
