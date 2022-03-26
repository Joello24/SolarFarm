using SolarFarm.Core.DTO;
using SolarFarm.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarFarm.DAL
{
    public class PanelRepo : IPanelRepository
    {
        private List<Panel> _panels;

        private string PATH = Directory.GetCurrentDirectory() + @"Panels.csv";
        private PanelCSVFormatter formatter = new PanelCSVFormatter();
        public PanelRepo()
        {
            Result<List<Panel>> loader = LoadFromFile();
            if (loader.Success)
            {
                _panels = loader.Data;
            }
            else
            {
                _panels = new List<Panel>();
            }
        }

        private Result<List<Panel>> LoadFromFile()
        {
            Result<List<Panel>> panelLoader = new Result<List<Panel>>();
            panelLoader.Data = new List<Panel>();
            if (File.Exists(PATH))
            {
                panelLoader.Success = true;
                using (StreamReader sr = new StreamReader(PATH))
                {
                    string line = sr.ReadLine();
                    if (line!= null && formatter.HasHeaderLine())
                    {
                        line = sr.ReadLine();
                    }

                    while (line != null)
                    {
                        panelLoader.Data.Add(formatter.Deserialize(line));
                        line = sr.ReadLine();
                    }
                }
            }
            else
            {
                panelLoader.Success = false;
                Console.WriteLine($"Can't find {PATH}");
                File.Create(PATH);
            }
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
        
        public Result<Panel> Update(Panel panel)
        {
            Result<Panel> result = new Result<Panel>();
            result = FindPanel(panel.Section, panel.Row, panel.Column);
            Delete(result.Data);
            _panels.Add(panel);
            result.Data = panel;
            result.Success = true;
            result.Message = "Updated!";
            result.Data = panel;
            return result;
        }
        
        public Result<string> SaveQuit(List<Panel> panels)
        {
            Result<string> result = new Result<string>();
            using (StreamWriter sw = new StreamWriter(PATH))
            {
                if (formatter.HasHeaderLine())
                {
                    sw.WriteLine(formatter.HeaderLine);
                }

                foreach (Panel panel in panels)
                {
                    sw.WriteLine(formatter.Serialize(panel));
                    result.Data += panel.ToString();
                }
            }
            result.Success = true;
            return result;
        }
        public Result<string> SaveQuit()
        {
            Result<string> result = new Result<string>();
            using (StreamWriter sw = new StreamWriter(PATH))
            {
                if (formatter.HasHeaderLine())
                {
                    sw.WriteLine(formatter.HeaderLine);
                }

                foreach (Panel panel in _panels)
                {
                    sw.WriteLine(formatter.Serialize(panel));
                    result.Data += panel.ToString();
                }
            }

            result.Success = true;
            return result;

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
