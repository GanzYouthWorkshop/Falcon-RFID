using RFID_Client;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GEV.Falcon.RFID.Client
{
    [Serializable]
    public class ReaderStation
    {
        public enum Statuses
        {
            Online,
            NotKnown,
            Offline
        }
        public string IP { get; set; }
        public string StationID { get; set; }
        public string DisplayName { get; set; }

        public Statuses Status { get; set; }

        public static List<CheckinEntry> QueryAll(DateTime from, DateTime to)
        {
            List<CheckinEntry> allCheckins = new List<CheckinEntry>();
            foreach (ReaderStation rs in MainWindow.Config.LastReaderStations)
            {
                List<CheckinEntry> tmp = rs.Query(from, to);
                allCheckins.AddRange(tmp);
            }

            return allCheckins;
        }

        public List<CheckinEntry> Query(DateTime from, DateTime to)
        {
            HttpClient client = new HttpClient();
            client.Timeout = new TimeSpan(0, 0, 0, 0, 1000);

            List<CheckinEntry> checkins = new List<CheckinEntry>();

            try
            {
                string fromString = from.ToString(CheckinEntry.DATETIME_STRING_FORMAT);
                string toString = to.ToString(CheckinEntry.DATETIME_STRING_FORMAT);

                string http = String.Format("http://" + this.IP + ":8080/query.gev?from={0}&to={1}", fromString, toString);

                string responseString = client.GetStringAsync(http).GetAwaiter().GetResult();

                using (TextReader reader = new StringReader(responseString))
                {
                    checkins = (List<CheckinEntry>)new XmlSerializer(typeof(List<CheckinEntry>)).Deserialize(reader);
                }
            }
            catch (Exception ex) { }

            return checkins;
        }
    }
}
