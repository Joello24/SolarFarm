using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarFarm.Core.DTO
{
    public class Panel
    {
        public string Section { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
        //public DateTime Year { get; set; }
        public int Year { get; set; }
        public Material Material { get; set; }
        public bool isTracking { get; set; }
        public int ID { get; set; }
        public Panel()
        {
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine();
            sb.AppendLine($"Section: {Section}");
            sb.AppendLine($"Row: {Row}");
            sb.AppendLine($"Column: {Column}");
            sb.AppendLine($"Year: {Year}");
            sb.AppendLine($"Material: {(Material)Material}");
            sb.AppendLine($"Tracking: {isTracking}");
            sb.AppendLine("---------------------");

            return sb.ToString();
        }

    }
}
