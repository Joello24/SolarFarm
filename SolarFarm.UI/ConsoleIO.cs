using System;
using SolarFarm.Core.DTO;
using SolarFarm.DAL;
namespace SolarFarm.UI
{
    public class ConsoleIO
    {
        public int GetInt(string prompt)
        {
            int result = -1;
            bool valid = false;
            while (!valid)
            {
                Console.Write($"{prompt}: ");
                Console.ForegroundColor= ConsoleColor.Green;
                Console.Write("~ ");
                if (!int.TryParse(Console.ReadLine(), out result))
                {
                    Error("Please input a proper integer\n\n");
                }
                else
                {
                    valid = true;
                }
            }
            Console.ResetColor();
            return result;
        }
        public int GetIntRowCol(string prompt)
        {
            int result = -1;
            bool valid = false;
            while (!valid)
            {
                Console.Write($"{prompt}: ");
                Console.ForegroundColor= ConsoleColor.Green;
                Console.Write("~ ");
                if (!int.TryParse(Console.ReadLine(), out result))
                {
                    Error("Please input a proper integer\n\n");
                }
                else if (result > 250 || result < 1)
                {
                    Error("1-250");

                }
                else
                {
                    valid = true;
                }
            }
            Console.ResetColor();
            return result;
        }
        public decimal GetDecimal(string prompt)
        {
            decimal result = -1;
            bool valid = false;
            while (!valid)
            {
                Console.Write($"{prompt}: ");
                Console.ForegroundColor= ConsoleColor.Green;
                Console.Write("~ ");
                if (!decimal.TryParse(Console.ReadLine(), out result))
                {
                    Error("Please input a proper integer\n\n");
                }
                else
                {
                    valid = true;
                }
            }
            Console.ResetColor();
            return result;
        }
        public string GetString(string prompt)
        {
            string result = "";
            bool valid = false;
            while (!valid)
            {
                Console.Write($"{prompt}: ");
                Console.ForegroundColor= ConsoleColor.Green;
                Console.Write("~ ");
                result = Console.ReadLine();
                if (string.IsNullOrEmpty(result))
                {
                    Error("Please input a proper string\n\n");
                }
                else
                {
                    valid = true;
                }
            }
            Console.ResetColor();
            return result;
        }
        public Panel UpdatePanel(Panel panel)
        {
            bool valid = false;
            Panel ret = new Panel();
            string holder;
            while (!valid)
            {
                Display($"Editing {panel.Section}-{panel.Row}-{panel.Column}");
                Display("Press [Enter] to keep original value");

                Console.Write($"Section {panel.Section}: ");
                holder = Console.ReadLine();

                if (string.IsNullOrEmpty(holder))
                {
                        ret.Section = panel.Section;
                }
                else
                {
                    ret.Section = holder;
                }

                Console.Write($"Row {panel.Row}: ");
                holder = Console.ReadLine();

                if (string.IsNullOrEmpty(holder) || !int.TryParse(holder, out int row))
                {
                    ret.Row = panel.Row;
                }
                else
                {
                    ret.Row = row;
                }

                Console.Write($"Column {panel.Column}: ");
                holder = Console.ReadLine();

                if (string.IsNullOrEmpty(holder) || !int.TryParse(holder, out int col))
                {
                    ret.Column = panel.Column;
                }
                else
                {
                    ret.Column = col;
                }

                Console.Write($"Material {panel.Material}: ");
                holder = Console.ReadLine();

                if (string.IsNullOrEmpty(holder) || !Material.TryParse(holder, out Material material))
                {
                    ret.Material = panel.Material;
                }
                else
                {
                    ret.Material = material;
                }

                Console.Write($"Year {panel.Year}: ");
                holder = Console.ReadLine();

                if (string.IsNullOrEmpty(holder) || !int.TryParse(holder, out int year))
                {
                    ret.Year = panel.Year;
                }
                else
                {
                    ret.Year = year;
                }

                Console.Write($"Tracked {panel.isTracking} [y/n]: ");
                string boolholder = Console.ReadLine();

                if (string.IsNullOrEmpty(holder) || char.ToUpper(char.Parse(boolholder)) != 'Y' || char.ToUpper(char.Parse(boolholder)) != 'N')
                {
                    ret.isTracking = panel.isTracking;
                }
                else
                {
                    bool sender;
                    if (char.ToUpper(char.Parse(boolholder)) == 'Y')
                    {
                        sender = true;
                        ret.isTracking = sender;
                    }
                    else if (char.ToUpper(char.Parse(boolholder)) == 'N')
                    {
                        sender= false;
                        ret.isTracking = sender;
                    }
                    else
                        ret.isTracking=panel.isTracking;
                    
                    
                }
                valid = true;
            }
            return ret;
        }

        public Panel GetPanelSecRowCol(List<string> sections)        
        {
            bool valid = false;
            Panel panel = new Panel();
            while (!valid)
            {
                panel.Section = GetSection("Section: ", sections);
                panel.Row = GetIntRowCol("Row: ");
                panel.Column = GetIntRowCol("Column: ");
                valid = true;
            }
            return panel;
        }
        public Panel GetPanel(List<string> sections)
        {
            bool valid = false;
            Panel panel = new Panel();
            while (!valid)
            {
                panel.Section = GetSection("Section: ", sections);
                panel.Row = GetIntRowCol("Row: ");
                panel.Column = GetIntRowCol("Column: ");
                while (!valid)
                {
                    panel.Year = GetInt("Year: ");
                    if (panel.Year < 0 && panel.Year < DateTime.Now.Year)
                        Display("Invalid year");
                    else
                        valid = true;
                }
                panel.Material = (Material)GetInt("Material [1-5]: ");
                panel.isTracking = GetYesOrNo("Tracked? [y/n]: ");
                valid = true;
            }
            return panel;
        }
        internal string GetSection(string prompt, List<string> sections)
        {
            string section = "";
            while (true)
            {
                if (sections == null)
                {
                    Error("No sections to display!");
                }
                else
                {

                    foreach (string s in sections)
                    {
                        Error($"{s.ToString()}");
                    }
                }
                Console.Write($"{prompt}: ");
                Console.ForegroundColor= ConsoleColor.Green;
                Console.Write("~ ");
                string result = Console.ReadLine();
                string ret;
                
                ret = result;
                Console.ResetColor();
                return ret;
            }
        }
        internal string CreateSection(string prompt)
        {
            while (true)
            {
                Console.Write($"{prompt}: ");
                Console.ForegroundColor= ConsoleColor.Green;
                Console.Write("~ ");
                string result = Console.ReadLine();
                string ret;

                ret = result;
                Console.ResetColor();
                return ret;
            }
        }
        public DateTime GetDate(string prompt)
        {
            while (true)
            {
                Console.Write($"{prompt}: ");
                Console.ForegroundColor= ConsoleColor.Green;
                Console.Write("~ ");
                if (!DateTime.TryParse(Console.ReadLine(), out DateTime result))
                {
                    Error("Please input a proper date\n\n");
                }
                else
                {
                    Console.ResetColor();
                    return result;
                }
            }
        }
        public bool GetYesOrNo(string message)
        {
            while (true)
            {
                Console.Write(message);
                Console.ForegroundColor= ConsoleColor.Green;
                Console.Write("~ ");
                string sender = Console.ReadLine();
                if (sender.ToUpper() == "Y")
                {
                    Console.ResetColor();
                    return true;
                }
                else if (sender.ToUpper() == "N")
                {
                    Console.ResetColor();
                    return false;
                }
            }
        }
        public void Display(string message)
        {
            Console.WriteLine(message);
        }
        public void Error(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Display(message);
            Console.ForegroundColor = ConsoleColor.White;
        }
        public void Warn(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Display(message);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
