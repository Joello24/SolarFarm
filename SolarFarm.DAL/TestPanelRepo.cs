using SolarFarm.Core.DTO;
using SolarFarm.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarFarm.DAL
{
    public class TestPanelRepo : IPanelRepository
    {
        private List<Panel> _panels;
        public TestPanelRepo()
        {
            _panels = LoadFakeData().Data;
        }

        private Result<List<Panel>> LoadFakeData()
        {
            Result<List<Panel>> panelLoader = new Result<List<Panel>>();
            panelLoader.Data = new List<Panel>();

            Panel fake = new Panel();
            fake.Section = "Main";
            fake.Row = 1;
            fake.Column = 220;
            fake.Material = (Material)2;
            fake.Year = 2020;
            fake.isTracking = true;

            panelLoader.Data.Add(fake);


            return panelLoader;
        }

        public Result<Panel> Add(Panel panel)
        {
            Result<Panel> result = new Result<Panel>();
            _panels.Add(panel);
            result.Success = true;
            result.Data = panel;
            return result;
        }

        public Result<List<Panel>> GetAll()
        {
            Result<List<Panel>> result = new Result<List<Panel>>();
            result.Data = _panels;
            result.Success = true;
            return result;
        }

        public Result<Panel> Delete(Panel panel)
        {
            Result<Panel> result = new Result<Panel>();
            result.Data = panel;
            if (_panels.Remove(panel))
            {
                result.Success = true;
                result.Message = $"Removed {panel} from list!";
            }
            else
            {
                result.Success = false;
                result.Message = $"Unable to remove {panel} from list!";
            }
            return result;
        }

        public Result<Panel> Update(Panel newPanel, Panel oldPanel)
        {
            Result<Panel> result = new Result<Panel>();

            result.Data = newPanel;
            result.Success = true;
            result.Message = "Updated!";
            return result;
        }

        public Result<string> SaveQuit(List<Panel> panels)
        {
            throw new NotImplementedException();
        }
        public Result<string> SaveQuit()
        {
            throw new NotImplementedException();
        }
        public Result<Panel> FindPanel(string section, int row, int column)
        {
            Result<Panel> result = new Result<Panel>();
            foreach (Panel panel in _panels)
            {

                if (panel.Row == row && panel.Column == column && panel.Section == section)
                {
                    result.Data = panel;
                    result.Success = true;
                    result.Message = "Found it!";
                    return result;

                }
            }
            result.Success = false;
            result.Message = "Panel not found!";
            return result;
        }

        public List<string> FindUniqueSections()
        {
            List<string> sections = new List<string>();
            foreach (Panel panel in _panels)
            {
                if (!sections.Contains(panel.Section))
                {
                    sections.Add(panel.Section);
                }
            }
            return sections;
        }
    }

}