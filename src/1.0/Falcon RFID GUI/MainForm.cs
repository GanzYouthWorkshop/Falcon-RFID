using GEV.Falcon.RFID;
using GEV.Falcon.RFID.Common;
using GEV.Layouts;
using GEV.Web.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Falcon_RFID_GUI
{
    public partial class MainForm : GCLForm
    {
        Timer m_TextUpdate;

        private Config config;
        private DataBase DB;


        public MainForm()
        {
            InitializeComponent();

            this.m_TextUpdate = new Timer()
            {
                Interval = 2000,
                Enabled = false
            };
            this.m_TextUpdate.Tick += M_TextUpdate_Tick; ;

            this.gclTextbox1.LostFocus += PerformAddFocus;
            this.gclTextbox1.Focus();

            DB = new DataBase("db.sqlite");
            config = Config.Load("config.xml");

            WebServer ws = new WebServer(8080, ".\\", 20, 5);
            ws.Start();
            ws.CompiledRequestHandlers.Add("/falcon_rfid_disco.gev", this.RespondDiscovery);
            ws.CompiledRequestHandlers.Add("/query.gev", this.GetCheckins);
        }

        private void M_TextUpdate_Tick(object sender, EventArgs e)
        {
            this.label1.Text = "Kártya leolvasható...";
            this.label1.BackColor = GCLColors.PanelBackground;
            this.m_TextUpdate.Enabled = false;
        }

        private void PerformAddFocus(object sender, EventArgs e)
        {
            this.gclTextbox1.Focus();
        }

        private void gclTextbox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                string s = this.gclTextbox1.Text.Replace('ö', '0');
                s = String.Join("", s.Split(new char[]{ '\n', '\r' }));
                this.gclTextbox1.Text = "";
                DB.Checkin(s);

                this.label1.Text = "Kártya leolvasva!";
                this.label1.BackColor = GCLColors.SuccessGreen;
                this.m_TextUpdate.Enabled = true;
            }
        }

        public bool RespondDiscovery(RequestHandler handler)
        {
            handler.WriteSuccess();
            handler.OutputStream.Write(this.config.ID + "-" + config.StationName);
            return true;
        }

        public bool GetCheckins(RequestHandler handler)
        {
            string from = DateTime.Parse(handler.RequestData.Arguments["from"]).ToString(CheckinEntry.DATETIME_STRING_FORMAT);
            string to = DateTime.Parse(handler.RequestData.Arguments["to"]).ToString(CheckinEntry.DATETIME_STRING_FORMAT);

            SQLiteDataReader query = this.DB.Query(String.Format("SELECT * FROM Checkins WHERE Date BETWEEN '{0}' AND '{1}'", from, to));

            List<CheckinEntry> list = new List<CheckinEntry>();
            while (query.Read())
            {
                list.Add(new CheckinEntry()
                {
                    Date = DateTime.Parse(query["Date"].ToString()),
                    Card = query["Card"].ToString(),
                });
            }
            XmlSerializer ser = new XmlSerializer(typeof(List<CheckinEntry>));

            handler.WriteHeaders(HTTPStatus.Code.OK, "text/xml");
            ser.Serialize(handler.OutputStream, list);

            return true;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
        }
    }
}
