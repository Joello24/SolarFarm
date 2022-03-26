using System;
using SolarFarm.BLL;
using SolarFarm.Core.DTO;
using SolarFarm.Core.Interface;
using SolarFarm.DAL;
using SolarFarm.UI;

namespace SolarFarm
{
    class Program
    {
        static void Main(string[] args)
        {

            
            // Create UI and controller
            ConsoleIO ui = new ConsoleIO();
            MenuController menu = new MenuController(ui);
            IPanelRepository repo = new PanelRepo();
            
            // Build or load section configuration

            // Build service with repo and configuration
            IPanelService service = new PanelService(repo);
            menu.Service = service;
            menu.Run();    //Do the thing!
        }
    }
}