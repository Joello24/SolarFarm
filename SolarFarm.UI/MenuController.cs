using System;
using System.Collections.Generic;
using SolarFarm.Core.DTO;
using SolarFarm.Core.Interface;

namespace SolarFarm.UI
{
    class MenuController
    {
        private ConsoleIO _ui;
        public IPanelService Service { get; set; }

        public MenuController(ConsoleIO ui) 
        {
            _ui = ui;
        }
        public int GetSetupMenuChoice()
        {
            DisplaySetupMenu();
            return _ui.GetInt("Enter Choice");
        }
        public void DisplaySetupMenu()
        {
            _ui.Display("Configuration mode");
            _ui.Display("===========================");
            _ui.Display("1. Add string");
            _ui.Display("2. Add a Panel");
            _ui.Display("3. Update a Panel");
            _ui.Display("4. Remove a Panel");
            _ui.Display("Select [0-4]");
        }
        public List<string> Setup()
        {
            _ui.Error("Entering Initial Configuration");
            _ui.Error("------------------------------");
            _ui.Error("Add sections: ");

            List<string> sections = new List<string>();
            bool isSettingUp = true;
            sections.Add(_ui.CreateSection(""));
            while (isSettingUp)
            {
                if (_ui.GetYesOrNo("Keep Going? [y/n]: "))
                {
                    sections.Add(_ui.CreateSection(""));
                }
                else
                {
                    isSettingUp = false;
                }
            }
            return sections;
        }
    

        public void Run()
        {
            bool running = true;

            while(running)
            {
                switch(GetMenuChoice())
                {
                    case 0:
                        QuitSave();
                        running = false;
                        break;
                    case 1:
                        FindPanelsBySection();
                        break;
                    case 2:
                        AddPanel();
                        break;
                    case 3:
                        UpdatePanel();
                        break;
                    case 4:
                        RemovePanel();
                        break;
                    default:
                        //Huh??
                        _ui.Error("Invalid menu option");
                        break;
                }
            }
        }

        private void QuitSave()
        {
            Result<string> ret;
            Console.ForegroundColor = ConsoleColor.Blue;
            bool save = _ui.GetYesOrNo("Quitting, would you like to save changes to drive? (Y/N): ");
            Console.ResetColor();   
            if (save)
            {
                ret = Service.SaveQuit();
                if (ret.Success)
                    _ui.Error("Save Successful!");
                //_ui.Display(ret.Data.ToString());
                else
                    _ui.Display(ret.Message);
            }
        }

        public int GetMenuChoice()
        {
            DisplayMenu();
            return _ui.GetInt("Enter Choice");
        }
        public void DisplayMenu()
        {
            _ui.Display("Main Menu");
            _ui.Display("===========================");
            _ui.Display("0. Exit");
            _ui.Display("1. Find Panels by Section");
            _ui.Display("2. Add a Panel");
            _ui.Display("3. Update a Panel");
            _ui.Display("4. Remove a Panel");
            _ui.Display("Select [0-4]");
        }
        public Result<List<string>> GetAllSections()
        {
            Result<List<string>> list = new Result<List<string>>();
            //list = Service.GetSections();
            list.Data = Service.FindUniqueSections();
            return list;
        }
      
        public void LoadPanel()
        {
            Result<Panel> ret;
            Panel p = _ui.GetPanelSecRowCol(GetAllSections().Data);
            ret = Service.Get(p.Section, p.Row, p.Column);
            if (ret.Success)
                _ui.Warn(ret.Data.ToString());
            else
                _ui.Error(ret.Message);
        }
        public void FindPanelsBySection()
        {
            Result<List<Panel>> ret;
            string section = _ui.GetSection("Section: ", GetAllSections().Data);
            ret = Service.LoadSection(section);
            if (ret.Success)
                foreach(Panel p in ret.Data)
                    _ui.Warn(p.ToString());
            else
                _ui.Error(ret.Message);
        }
        public void AddPanel()
        {
            Result<Panel> ret;
            ret = Service.Add(_ui.GetPanel(GetAllSections().Data));
            if (ret.Success)
                _ui.Warn(ret.Data.ToString());
            else
                _ui.Error(ret.Message);
        }
        public void UpdatePanel() 
        {
            Result<Panel> returnCurrent, returnEdit;

            Panel p = _ui.GetPanelSecRowCol(GetAllSections().Data);
            returnCurrent = Service.Get(p.Section, p.Row, p.Column);

            if (returnCurrent.Success)
            {
                returnEdit = Service.Update(_ui.UpdatePanel(returnCurrent.Data), returnCurrent.Data);
                if (returnEdit.Success)
                    _ui.Warn(returnEdit.Data.ToString());
                else
                    _ui.Error(returnEdit.Message);
            }

            else
            {
                _ui.Error($"{returnCurrent.Message}");
            }
            
        }

        public void RemovePanel()
        {
            Result<Panel> ret;
            Panel p = _ui.GetPanelSecRowCol(GetAllSections().Data);
            ret = Service.Remove(p.Section, p.Row, p.Column);

            if (ret.Success)
                _ui.Warn(ret.Data.ToString());
            else
                _ui.Error(ret.Message);
        }
        public void RemovePanel(string section, int row, int column)
        {
            Result<Panel> ret;
            
            ret = Service.Remove(section, row, column);

            if (ret.Success)
                _ui.Warn(ret.Data.ToString());
            else
                _ui.Error(ret.Message);
        }
    }
}
