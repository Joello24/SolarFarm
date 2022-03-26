using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarFarm.Core.DTO
{
    public class SectionConfig
    {
        public List<@string> Sections { get; set; }

        public SectionConfig()
        {
            Sections = new List<@string>();
        }
        public SectionConfig(List<@string> sections)
        {
            Sections = sections;
        }
    }
}
