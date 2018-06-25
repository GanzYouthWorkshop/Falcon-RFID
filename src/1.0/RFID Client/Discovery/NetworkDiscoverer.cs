using RFID_Client;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GEV.Falcon.RFID.Client.Discovery
{
    public class NetworkDiscoverer
    {
        public event EventHandler<string[]> DiscoverySuccess;
        public event EventHandler DiscoveryFailed;

        public string Address { get; set; }
        public int ID { get; set; }

        public void Start()
        {
            new Thread(this.Run)
            {
                IsBackground = true
            }.Start();
        }

        private void Run()
        {
            try
            {
                WebRequest client = WebRequest.Create(this.Address);
                client.Timeout = 200;

                WebResponse response = client.GetResponse();
                string responseString = "";
                using (StreamReader sr = new StreamReader(response.GetResponseStream()))
                {
                    responseString = sr.ReadToEnd();
                }
                string[] values = responseString.Split('-');

                this.DiscoverySuccess?.Invoke(this, values);
            }
            catch (Exception)
            {
                this.DiscoveryFailed?.Invoke(this, new EventArgs());
            }

            Console.WriteLine("#{0} végzett", this.ID);
        }
    }
}
