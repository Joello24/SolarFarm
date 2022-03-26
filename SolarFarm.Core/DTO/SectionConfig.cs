using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarFarm.Core.DTO
{
    public class SectionConfig
    {
        public List<Section> Sections { get; set; }

        public SectionConfig()
        {
            Sections = new List<Section>();
        }
        public SectionConfig(List<Section> sections)
        {
            Sections = sections;
        }
    }
}
