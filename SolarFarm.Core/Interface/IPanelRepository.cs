using SolarFarm.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarFarm.Core.Interface
{
    public interface IPanelRepository
    {
        Result<List<Panel>> GetAll();       
        Result<Panel> Add(Panel panel);  
        Result<Panel> Delete(Panel panel);  
        Result<Panel> Update(Panel newPanel, Panel oldPanel); 
        Result<string> SaveQuit();
        public Result<Panel> FindPanel(string section, int row, int column);
        List<string> FindUniqueSections();
    }
}
