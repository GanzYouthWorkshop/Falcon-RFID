using GEV.Layouts;
using GEV.Layouts.Extended.Cairo;
using GEV.Layouts.Extended.Cairo.DataFormat;
using RFID_Client;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEV.Falcon.RFID.Client.Queries
{
    public class MonthlySummary : IQuery
    {
        public CairoSpreadsheet Sheet { get; set; }
        public List<CheckinEntry> Data { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public void Run()
        {
            this.SetupTable();

            List<string> cards = this.Data.GroupBy(entry => entry.Card).Select(card => card.First()).Select(card => card.Card).ToList();

            Dictionary<string, TimeSpan> cardsAndTimes = new Dictionary<string, TimeSpan>();
            foreach (string card in cards)
            {
                cardsAndTimes.Add(card, new TimeSpan());
            }


            var byDay = this.Data.GroupBy(item => item.Date.DayOfYear);
            foreach (var group in byDay)
            {
                var byWorker = group.GroupBy(i => i.Card);
                foreach (var worker in byWorker)
                {
                    DateTime min = worker.Min(item => item.Date);
                    DateTime max = worker.Max(item => item.Date);
                    TimeSpan workTime = max.Subtract(min);

                    int col = (group.Key - this.Start.DayOfYear) + 2;

                    CardHolder tmp = this.Workers.FirstOrDefault(w => w.CardNumber == worker.First().Card);
                    if(tmp != null)
                    {
                        int row = this.Workers.IndexOf(tmp) + 7;

                        this.Sheet.CurrentWorksheet.Cells[row, col].DataFormat = CellDataFormatFlag.Text;
                        this.Sheet.CurrentWorksheet.Cells[row, col].Data = workTime.ToString(@"hh\:mm");

                        this.Sheet.CurrentWorksheet.Cells[row, col].Style.BackColor = QueryUtils.BackColorOfWorkTime(workTime);
                    }
                }
            }


        }

        private List<CardHolder> Workers = new List<CardHolder>();

        private int[] SetupTable()
        {
            List<string> cards = this.Data.GroupBy(entry => entry.Card).Select(card => card.First()).Select(card => card.Card).ToList();

            this.Workers = MainWindow.Config.CardHolders.OrderBy(s => s.Department).ThenBy(s => s.Worker).ToList();

            this.Start = new DateTime(this.Start.Year, this.Start.Month, this.Start.Day);
            this.End = new DateTime(this.End.Year, this.End.Month, this.End.Day);

            List<string> days = new List<string>();
            DateTime i = this.Start;
            DateTime to = this.End.AddDays(1);

            while (i != to)
            {
                days.Add(i.ToString("yyyy. MM. dd."));
                i = i.AddDays(1);
            }
            days.Add("Átlag");
            days.Add("Szadadság");
            days.Add("Csúsztatás");

            Worksheet ws = this.Sheet.CurrentWorksheet;
            QueryUtils.SetupMainHeader(this.Sheet.CurrentWorksheet, "Összesítés", this.Start, this.End);
            QueryUtils.SetupHeader(this.Sheet.CurrentWorksheet, this.Workers.Select(w => w.Worker).ToList(), false, 7, 1);
            QueryUtils.SetupHeader(this.Sheet.CurrentWorksheet, days, true, 6, 2, true);

            int dayCount;
            for ( dayCount = 0; dayCount < days.Count; dayCount++)
            {
                DateTime dt;
                if(DateTime.TryParse(days[dayCount], out dt))
                {
                    if (dt.DayOfWeek == DayOfWeek.Sunday || dt.DayOfWeek == DayOfWeek.Saturday)
                    {
                        RangePosition rp = new RangePosition(7, dayCount + 2, Workers.Count, 1);
                        this.Sheet.CurrentWorksheet.SetRangeStyles(rp, new WorksheetRangeStyle()
                        {
                            Flag = PlainStyleFlag.BackColor | PlainStyleFlag.VerticalAlign,
                            BackColor = Color.LightGray,
                        });
                    }
                }
            }

            for (int c = 0; c < Workers.Count; c++)
            {
                string range = String.Format("C{0}:{1}{2}", 8 + c, QueryUtils.ExcelColumnName(dayCount - 1), 8 + c);
                this.Sheet.CurrentWorksheet.Cells[7 + c, dayCount - 1].Formula = "AVERAGE(" + range + ")";
                this.Sheet.CurrentWorksheet.Cells[7 + c, dayCount].Formula = "COUNTIF(" + range + "; \"sz\")";
            }


            List<Color> colors = new List<Color>()
            {
                Color.LightGreen,
                Color.Orange,
                Color.LightCoral
            };

            int count = 7;
            int lastCount = 7;
            int colorIndex = 0;
            var departments = this.Workers.GroupBy(w => w.Department);
            foreach(var department in departments)
            {
                this.Sheet.CurrentWorksheet.Cells[count, 0].Style.VAlign = ReoGridVerAlign.Middle;
                this.Sheet.CurrentWorksheet.Cells[count, 0].Style.HAlign = ReoGridHorAlign.Center;
                this.Sheet.CurrentWorksheet.Cells[count, 0].Data = department.Key;
                count += department.Count();

                this.Sheet.CurrentWorksheet.MergeRange(lastCount, 0, count - lastCount, 1);
                RangePosition rp = new RangePosition(lastCount, 0, count - lastCount, 2);
                this.Sheet.CurrentWorksheet.SetRangeStyles(rp, new WorksheetRangeStyle()
                {
                    Flag = PlainStyleFlag.BackColor,
                    BackColor = colors[colorIndex++],
                });

                lastCount = count;
            }

            RangePosition yellow = new RangePosition(7, dayCount - 1, lastCount - 7, 3);
            this.Sheet.CurrentWorksheet.SetRangeStyles(yellow, new WorksheetRangeStyle()
            {
                Flag = PlainStyleFlag.BackColor,
                BackColor = Color.LightYellow,
            });



            this.Sheet.CurrentWorksheet.Cells[6, 0].Style.VAlign = ReoGridVerAlign.Middle;
            this.Sheet.CurrentWorksheet.Cells[6, 0].Style.HAlign = ReoGridHorAlign.Center;
            this.Sheet.CurrentWorksheet.Cells[6, 0].Style.Bold = true;
            this.Sheet.CurrentWorksheet.Cells[6, 0].Data = "Csoport";

            this.Sheet.CurrentWorksheet.Cells[6, 1].Style.VAlign = ReoGridVerAlign.Middle;
            this.Sheet.CurrentWorksheet.Cells[6, 1].Style.HAlign = ReoGridHorAlign.Center;
            this.Sheet.CurrentWorksheet.Cells[6, 1].Style.Bold = true;
            this.Sheet.CurrentWorksheet.Cells[6, 1].Data = "Név";

            this.Sheet.CurrentWorksheet.SetColumnsWidth(2, days.Count, 40);

            this.Sheet.CurrentWorksheet.FreezeToCell(0, 2);
            return new int[] { 7, 0 };
        }
    }
}
