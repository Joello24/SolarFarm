using SolarFarm.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarFarm.Core.Interface
{
    public interface IPanelFormatter
    {
        Panel Deserialize(string data);
        string Serialize(Panel panel);
        bool HasHeaderLine();
        string HeaderLine();
    }
}
