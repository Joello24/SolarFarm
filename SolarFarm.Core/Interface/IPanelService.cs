using SolarFarm.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarFarm.Core.Interface
{
    public interface  IPanelService
    {
        Result<List<Panel>> LoadSection(string section);     
        Result<Panel> Get(string section, int row, int column);                              
        Result<Panel> Add(Panel panel);                          
        Result<Panel> Remove(string section, int row, int column);                           
        Result<Panel> Update(Panel panel);                         
        Result<string> SaveQuit();
        
        Result<List<string>> GetSections();
        public Result<Panel> PanelExists(Panel panel);
        List<string> FindUniqueSections();
    }
}
