using SolarFarm.Core.DTO;
using SolarFarm.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarFarm.BLL
{
    public class PanelService : IPanelService
    {
        private IPanelRepository _repo;
        public PanelService(IPanelRepository repo)
        {
            _repo = repo;
        }

        public List<string> FindUniqueSections()
        {
            return _repo.FindUniqueSections();
        }
        public Result<List<string>> GetSections()
        {
            Result<List<string>> result = new Result<List<string>>();
            result.Data = new List<string>();
            foreach (Panel s in _repo.GetAll().Data)
            {
                if (!result.Data.Contains(s.Section))
                {
                    result.Data.Add(s.Section);
                    result.Success = true;
                    result.Message = "Added sections!";
                }
            }
            return result;
        }
        public Result<Panel> Add(Panel panel)
        {
            Result<List<Panel>> panels = new Result<List<Panel>>();
            Result<List<string>> sections = new Result<List<string>>();
            sections = GetSections();
            panels = _repo.GetAll();
            Result<Panel> result = new Result<Panel>();

            result.Success = true;
            result.Message = "Added panel!";
            if (panels.Data == null)
            {
                result =_repo.Add(panel);
                return result;
            }
            if (panels.Data.Contains(panel) || PanelExists(panel).Success)
            {
                result.Success = false;
                result.Message = "Duplicate panel";
            }
            if (panel.Row > 250 || panel.Row < 1)
            {
                result.Success = false;
                result.Message = "Invalid Row";
            }
            if (panel.Column > 250 || panel.Column < 1)
            {
                result.Success = false;
                result.Message = "Invalid Row";
            }
            if (panel.Year > DateTime.Now.Year)
            {
                result.Success = false;
                result.Message = "Year is in future";
            }
            if (!panel.Material.Equals(Material.cadmiumTelluride) && !panel.Material.Equals(Material.copperIndiumGalliumSelenide)
                 && !panel.Material.Equals(Material.multicrystallineSilicon) && !panel.Material.Equals(Material.monocrystallineSilicon)
                 && !panel.Material.Equals(Material.amorphousSilicon))
            {
                result.Success = false;
                result.Message = "Invalid Material";
            }
            if (!panel.isTracking)
            {
                result.Success = false;
                result.Message = "Not tracking";
            }
            if(result.Success)
            {
                result =_repo.Add(panel);
            }
            return result;
        }

        public Result<Panel> Get(string section, int row, int column)
        {
            Result<List<Panel>> panels = new Result<List<Panel>>();
            panels = _repo.GetAll();

            Result<Panel> result = new Result<Panel>();
            if (panels.Success)
            {
                foreach (Panel panel in panels.Data)
                {

                    if (panel.Row == row && panel.Column == column && panel.Section == section)
                    {
                        result.Data = panel;
                        result.Success = true;
                        result.Message = "Found it!";
                        return result;
                    }
                }
            }
            result.Success = false;
            result.Message = "Panel not found!";
            return result;
        }

        public Result<List<Panel>> LoadSection(string section)
        {
            Result<List<Panel>> result = new Result<List<Panel>>();
            Result<List<Panel>> panels = new Result<List<Panel>>();
            result.Data = new List<Panel>();
            panels = _repo.GetAll();

            if (panels.Data == null)
            {
                result.Success = false;
                result.Message = "No panels exist yet";
                return result;
            }

            result.Success = false;
            result.Message = "No Panels in that section";
            foreach (Panel panel in panels.Data)
            {
                if (panel.Section == section)
                {
                    result.Data.Add(panel);
                    result.Success=true;
                }
                
            }
            return result;
        }

        public Result<Panel> Remove(string section, int row, int column)
        {
            Result<List<Panel>> panels = new Result<List<Panel>>();
            panels = _repo.GetAll();
            Result<Panel> panel = Get(section, row, column);
            Result<Panel> result = new Result<Panel>();
            if (!panel.Success)
            {
                result.Success = false;
                result.Message = "Can't find panel!";
                return result;
            }
            if (panels.Data.Contains(panel.Data) || PanelExists(panel.Data).Success)
            {
               
                result = _repo.Delete(panel.Data);
            }
            else
            {
                result.Success=false;
                result.Message = "Can't find panel";
            }
            return result;
        }

        public Result<string> SaveQuit()
        {
            Result<string> result = new Result<string>();
            result = _repo.SaveQuit();
            return result;
        }
        public Result<Panel> Update(Panel panel)
        {
            Result<Panel> result = new Result<Panel>();

            if (ValidatePanel(panel).Success)
            {
                result = _repo.Update(panel);
            }
            else
            {
                result = ValidatePanel(panel);
            }
            return result;
        }
        public Result<Panel> PanelExists(Panel panel)
        {
            Result<List<Panel>> panels = new Result<List<Panel>>();
            panels = _repo.GetAll();

            Result<Panel> result = new Result<Panel>();
            result = _repo.FindPanel(panel.Section, panel.Row, panel.Column);
            
            return result;
        }
        private Result<Panel> ValidatePanel(Panel panel)
        {
            Result<List<Panel>> panels = new();
            panels = _repo.GetAll();

            Result<Panel> result = new();
            if (panels.Data.Contains(panel) || PanelExists(panel).Success)
            {
                result.Success = false;
                result.Message = "Duplicate panel";
            }
            if (panel.Row > 250 || panel.Row < 1)
            {
                result.Success = false;
                result.Message = "Invalid Row";
            }
            if (panel.Column > 250 || panel.Column < 1)
            {
                result.Success = false;
                result.Message = "Invalid Row";
            }
            if (panel.Year > DateTime.Now.Year)
            {
                result.Success = false;
                result.Message = "Year is in future";
            }
            if (!panel.Material.Equals(Material.cadmiumTelluride) && !panel.Material.Equals(Material.copperIndiumGalliumSelenide)
                 && !panel.Material.Equals(Material.multicrystallineSilicon) && !panel.Material.Equals(Material.monocrystallineSilicon)
                 && !panel.Material.Equals(Material.amorphousSilicon))
            {
                result.Success = false;
                result.Message = "Invalid Material";
            }
            //if (!panel.isTracking)
            //{
            //    result.Success = false;
            //    result.Message = "Not tracking";
            //}
            else
            {
                result.Success = true;
                result.Data = panel;
            }
            return result;
        }


    }
}
