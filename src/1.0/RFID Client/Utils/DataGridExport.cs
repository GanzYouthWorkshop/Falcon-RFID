using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GEV.Falcon.RFID.Utils
{
    public class DataGridExport
    {
        public static void ExportDataGridAsCSV(DataGridView dataGrid, string path)
        {
            using (StreamWriter sw = new StreamWriter(path, false, Encoding.ASCII, 4096))
            {
                List<string> headers = new List<string>();
                foreach (DataGridViewColumn col in dataGrid.Columns)
                {
                    headers.Add(col.HeaderText);
                }
                sw.WriteLine(String.Join(";", headers));

                foreach(DataGridViewRow row in dataGrid.Rows)
                {
                    List<string> cells = new List<string>();
                    foreach(DataGridViewCell cell in row.Cells)
                    {
                        if(cell.Value != null)
                        {
                            cells.Add(cell.Value.ToString());
                        }
                        else
                        {
                            cells.Add("");
                        }
                    }
                    sw.WriteLine(String.Join(";", cells));
                }
            }
        }
    }
}
