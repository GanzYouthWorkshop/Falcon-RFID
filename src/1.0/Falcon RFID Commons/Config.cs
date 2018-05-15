using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GEV.Falcon.RFID.Common
{
    [Serializable]
    public class Config
    {
        public string ID { get; set; } = "test";
        public string StationName { get; set; } = "Test";

        public static Config Load(string file)
        {
            if(!File.Exists(file))
            {
                Config c = new Config();
                c.Save(file);
                return c;
            }
            else
            {
                using (StreamReader sr = new StreamReader(file))
                {
                    XmlSerializer ser = new XmlSerializer(typeof(Config));
                    return (Config)ser.Deserialize(sr);
                }
            }
        }

        public void Save(string file)
        {
            using (StreamWriter sw = new StreamWriter(file))
            {
                XmlSerializer ser = new XmlSerializer(typeof(Config));
                ser.Serialize(sw, this);
            }
        }
    }
}
