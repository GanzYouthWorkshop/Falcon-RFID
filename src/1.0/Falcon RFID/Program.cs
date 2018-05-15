using GEV.Falcon.RFID;
using GEV.Falcon.RFID.Common;
using GEV.Web.Server;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SQLite;
using System.Linq;
using System.Media;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace GEV.Falcon.RFID
{
    class Program
    {
        [DllImport("kernel32.dll")]
        public static extern bool Beep(int freq, int duration);

        private static Config config;
        private static DataBase DB;

        static void Main(string[] args)
        {
            bool onlyInstance = false;
            Mutex m = new Mutex(false, "Global\\FalconRFID", out onlyInstance);
            if (!onlyInstance)
            {
                MessageBox.Show("Program már fut!");
                return;
            }

            Console.Title = "Falcon RFID";
            Log.Alert("Falcon RFID 1.0");

            Log.Info("Adatbázis indítása...");
            DB = new DataBase("db.sqlite");
            Log.Success("Adatbázis elindult!");

            Log.Info("Konfig betöltése...");
            config = Config.Load("config.xml");
            Log.Success("Konfig betöltve!");

            Log.Info("A jelenlegi állomás: {0} ({1})", config.ID, config.StationName);


            Log.Info("Webszerver indítása...");
            WebServer ws = new WebServer(8080, ".\\", 20, 5);
            ws.Start();
            ws.CompiledRequestHandlers.Add("/falcon_rfid_disco.gev", RespondDiscovery);
            ws.CompiledRequestHandlers.Add("/query.gev", GetCheckins);
            Log.Success("Webszerver elindult!");

            Log.Success("====== Falcon RFID állomásszoftver használatra kész! ======");

            SystemSounds.Beep.Play();

            while (true)
            {
                string s = Console.ReadLine().Replace('ö', '0');
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                Log.Info("Egy kártyát leolvastak: {0}", s);
                DB.Checkin(s);
            }
        }

        public static bool RespondDiscovery(RequestHandler handler)
        {
            Log.Info("Az állomást felderítették...");

            handler.WriteSuccess();
            handler.OutputStream.Write(config.ID + "-" + config.StationName);
            return true;
        }

        public static bool GetCheckins(RequestHandler handler)
        {
            Log.Info("Lekérdezés kiszolgálása...");

            string from = DateTime.Parse(handler.RequestData.Arguments["from"]).ToString(CheckinEntry.DATETIME_STRING_FORMAT);
            string to = DateTime.Parse(handler.RequestData.Arguments["to"]).ToString(CheckinEntry.DATETIME_STRING_FORMAT);

            SQLiteDataReader query = DB.Query(String.Format("SELECT * FROM Checkins WHERE Date BETWEEN '{0}' AND '{1}'", from, to));

            List<CheckinEntry> list = new List<CheckinEntry>();
            while(query.Read())
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
    }
}
