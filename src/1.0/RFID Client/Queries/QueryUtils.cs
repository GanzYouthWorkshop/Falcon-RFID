using GEV.Layouts;
using GEV.Layouts.Extended.Cairo;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEV.Falcon.RFID.Client.Queries
{
    public class QueryUtils
    {
        public static int[] SetupMainHeader(Worksheet ws, string name, DateTime start, DateTime end)
        {
            ws.Reset();

            int row = 0;
            int col = 0;

            ws.Cells[0, 0].Style.FontSize = 16;
            ws.MergeRange("A1:E1");
            ws.Cells[0, 0].Data = name;

            ws.Cells[1, 0].Style.FontSize = 8;
            ws.MergeRange("A2:B2");
            ws.Cells[1, 0].Data = "Falcon RFID 1.1";

            ws.Cells[2, 0].Data = "Kezdés: ";

            ws.MergeRange("B3:E3");
            ws.Cells[2, 1].DataFormat = Layouts.Extended.Cairo.DataFormat.CellDataFormatFlag.DateTime;
            ws.Cells[2, 1].Data = start;

            ws.Cells[3, 0].Data = "Vége:";

            ws.MergeRange("B4:E4");
            ws.Cells[3, 1].DataFormat = Layouts.Extended.Cairo.DataFormat.CellDataFormatFlag.DateTime;
            ws.Cells[3, 1].Data = end;

            return new int[] { 6, 0 };
        }

        public static void SetupHeader(Worksheet ws, List<string> items, bool horizontal, int startRow, int startColumn, bool rotate = false)
        {
            int row = startRow;
            int col = startColumn;

            foreach(string item in items)
            {
                RangeBorderStyle border = new RangeBorderStyle(Color.Black, BorderLineStyle.BoldSolid);
                if (horizontal)
                {
                    ws.Cells[row, col].Border.Bottom = border;
                }
                else
                {
                    ws.Cells[row, col].Border.Right = border;
                }
                ws.Cells[row, col].Style.VAlign = ReoGridVerAlign.Middle;
                ws.Cells[row, col].Style.HAlign = ReoGridHorAlign.Center;
                ws.Cells[row, col].Style.Bold = true;
                ws.Cells[row, col].Data = item;

                if(rotate)
                {
                    ws.Cells[row, col].Style.RotationAngle = 90;
                }

                ws.Cells[row, col].ExpandColumnWidth();
                ws.Cells[row, col].ExpandRowHeight();

                if (horizontal) col++; else row++;
            }
        }

        public static Color ColorOfWorkTime(TimeSpan workTime)
        {
            if (workTime > new TimeSpan(0, 9, 0, 0, 0))
            {
                return GCLColors.AccentColor1Highlight;
            }
            else if (workTime > new TimeSpan(0, 8, 25, 0, 0))
            {
                return GCLColors.SuccessGreen;
            }
            else if (workTime > new TimeSpan(0, 8, 15, 0, 0))
            {
                return GCLColors.AlertYellow;
            }
            else
            {
                return GCLColors.ErrorRed;
            }
        }

        public static Color BackColorOfWorkTime(TimeSpan workTime)
        {
            if (workTime > new TimeSpan(0, 9, 0, 0, 0))
            {
                return GCLColors.SuccessGreen;
            }
            else if (workTime > new TimeSpan(0, 8, 25, 0, 0))
            {
                return Color.Transparent;
            }
            else if (workTime > new TimeSpan(0, 8, 15, 0, 0))
            {
                return GCLColors.AlertYellow;
            }
            else
            {
                return GCLColors.ErrorRed;
            }
        }

        public static string ExcelColumnName(int columnNumber)
        {
            int dividend = columnNumber;
            string columnName = String.Empty;
            int modulo;

            while (dividend > 0)
            {
                modulo = (dividend - 1) % 26;
                columnName = Convert.ToChar(65 + modulo).ToString() + columnName;
                dividend = (int)((dividend - modulo) / 26);
            }

            return columnName;
        }
    }
}
