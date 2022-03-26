using SolarFarm.Core.DTO;
using SolarFarm.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarFarm.DAL
{
    public class PanelCSVFormatter : IPanelFormatter
    {
        public Panel Deserialize(string data)
        {
            Panel result = new Panel();
            string[] columns = data.Split(',');
            result.Section = columns[0];
            result.Row = int.Parse(columns[1]);
            result.Column = int.Parse(columns[2]);
            result.Year = int.Parse(columns[3]);
            //string holder = Material.Parse(columns[4]);
            
            //result.Material = holder;
            result.isTracking = bool.Parse(columns[5]);
            result.ID = int.Parse(columns[6]);

            return result;
        }

        public bool HasHeaderLine()
        {
            return true;
        }

        public string HeaderLine()
        {
            return "SECTION,ROW,COLUMN,YEAR,MATERIAL,ISTRACKING,ID";
        }

        public string Serialize(Panel panel)
        {
            return $"{panel.Section}, {panel.Row},{panel.Column}, {panel.Year}, {panel.Material}, {panel.isTracking}, {panel.ID}";
        }
    }
}
