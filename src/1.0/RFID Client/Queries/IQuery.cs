using GEV.Layouts.Extended.Cairo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEV.Falcon.RFID.Client.Queries
{
    public interface IQuery
    {
        CairoSpreadsheet Sheet { get; set; }
        List<CheckinEntry> Data { get; set; }
        DateTime Start { get; set; }
        DateTime End { get; set; }

        void Run();
    }
}
