using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarFarm.Core.DTO
{
    public class Panel
    {
        public Section Section { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
        //public DateTime Year { get; set; }
        public int Year { get; set; }
        public Material Material { get; set; }
        public bool isTracking { get; set; }
        public int ID { get; set; }
        public Panel()
        {
            Section = new Section();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Section: {Section.Name}");
            sb.AppendLine($"Row: {Row}F");
            sb.AppendLine($"Column: {Column}F");
            sb.AppendLine($"Year: {Year}%");
            sb.AppendLine($"Material: {Material}");
            sb.AppendLine($"Tracking: {isTracking}");
            sb.AppendLine($"ID: {ID}");
            sb.AppendLine("---------------------");

            return sb.ToString();
        }

    }
}
