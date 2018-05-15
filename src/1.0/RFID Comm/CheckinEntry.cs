using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEV.Falcon.RFID
{
    [Serializable]
    public class CheckinEntry
    {
        public const string DATETIME_STRING_FORMAT = "yyyy-MM-dd HH:mm:ss.fff";

        public DateTime Date { get; set; }
        public string Card { get; set; }
    }
}
