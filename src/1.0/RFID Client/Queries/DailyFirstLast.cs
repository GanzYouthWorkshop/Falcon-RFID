using GEV.Layouts;
using GEV.Layouts.Extended.Cairo;
using RFID_Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEV.Falcon.RFID.Client.Queries
{
    public class DailyFirstLast : IQuery
    {
        public CairoSpreadsheet Sheet { get; set; }
        public List<CheckinEntry> Data { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public void Run()
        {
            int[] start = SetupTable();

            int row = start[0];
            int col = start[1];

            var byDay = this.Data.GroupBy(item => item.Date.DayOfYear);
            foreach (var group in byDay)
            {
                string workDay = group.Key.ToString();

                var byWorker = group.GroupBy(i => i.Card);
                foreach (var worker in byWorker)
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
                    string workerStr = "Kártya #" + worker.First().Card;

                    if (card != null)
                    {
                        workerStr = card.Worker;
                    }

                    this.Sheet.CurrentWorksheet.Cells[row, col++].Data = "■";
                    this.Sheet.CurrentWorksheet.Cells[row, col++].Data = workerStr;
                    this.Sheet.CurrentWorksheet.Cells[row, col++].Data = min.ToString("yyyy. MMMM dd.");
                    this.Sheet.CurrentWorksheet.Cells[row, col].DataFormat = Layouts.Extended.Cairo.DataFormat.CellDataFormatFlag.Text;
                    this.Sheet.CurrentWorksheet.Cells[row, col++].Data = min.ToString("HH:mm");
                    this.Sheet.CurrentWorksheet.Cells[row, col].DataFormat = Layouts.Extended.Cairo.DataFormat.CellDataFormatFlag.Text;
                    this.Sheet.CurrentWorksheet.Cells[row, col++].Data = max.ToString("HH:mm");
                    this.Sheet.CurrentWorksheet.Cells[row, col++].Data = workTimeStr;
                    //this.Sheet.CurrentWorksheet.Cells[row, col++].Data = overTimeStr;


                    this.Sheet.CurrentWorksheet.Cells[row, 0].Style.TextColor = QueryUtils.ColorOfWorkTime(workTime);

                    this.Sheet.CurrentWorksheet.AutoFitColumnWidth(col, false);
                    this.Sheet.CurrentWorksheet.AutoFitRowHeight(row, false);
                    row++;
                    col = 0;

                }
                RangePosition range = new RangePosition(start[1], 0, row - start[1], 6);
                this.Sheet.CurrentWorksheet.SetRangeStyles(range, new WorksheetRangeStyle
                {
                    Flag = PlainStyleFlag.HorizontalAlign,
                    HAlign = ReoGridHorAlign.Center,
                });
            }

        }

        private int[] SetupTable()
        {

            List<string> items = new List<string>()
            {
                "",
                "Kártyatulajdonos",
                "Munkanap",
                "Megérkezés",
                "Távozás",
                "Munkaidő"
                //"Túlóra"
            };

            Worksheet ws = this.Sheet.CurrentWorksheet;
            QueryUtils.SetupMainHeader(this.Sheet.CurrentWorksheet, "Napi első és utolsó bejelentkezés", this.Start, this.End);
            QueryUtils.SetupHeader(this.Sheet.CurrentWorksheet, items, true, 6, 0);


            this.Sheet.CurrentWorksheet.SetColumnsWidth(1, 5, 30);
            this.Sheet.CurrentWorksheet.SetColumnsWidth(1, 5, 130);
            return new int[] { 7, 0 };
        }

    }
}
