using IniParser;
using IniParser.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIM.Modules
{
    class INI_db
    {        
        public void Save(string db, string group, string parameter, string value)
        {            
            var parser = new FileIniDataParser();
            if (File.Exists(db))
            {
                IniData data;
                try
                {
                    data = parser.ReadFile(db);
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine(
                        $">>{ex.Message}\r\n" +
                        $"{ex.ToString()}");

                    data = new IniData();

                }
                data[group][parameter] = value;
                parser.WriteFile(db, data);
            }
            else
            {

                string directory = Path.GetDirectoryName(db);

                if (directory != "" && !Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                IniData data = new IniData();
                data[group][parameter] = value;
                parser.WriteFile(db, data);
            }
        }

        public void Save(string db, string group, string parameter, bool value)
        {
            this.Save(db, group, parameter, Convert.ToString(value));
        }
        public void Save(string db, string group, string parameter, int value)
        {
            this.Save(db, group, parameter, Convert.ToString(value));
        }
        public void Save(string db, string group, string parameter, double value)
        {
            this.Save(db, group, parameter, Convert.ToString(value));
        }

        public string Load(string db, string group, string parameter)
        {
            IniData data;
            if (File.Exists(db))
            {
                try
                {
                    var parser = new FileIniDataParser();
                    data = parser.ReadFile(db);
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine(
                        $">>{ex.Message}\r\n" +
                        $"{ex.ToString()}");
                    data = new IniData();
                }
            }
            else
            {
                data = new IniData();
            }

            return data[group][parameter];
        }

        public string Load(string db, string group, string parameter, string default_value)
        {
            string value = this.Load(db, group, parameter);
            return value == null ? default_value : value;
        }

        public int Load(string db, string group, string parameter, int default_value)
        {
            int value;
            return this.TryLoad(db, group, parameter, out value) ? value : default_value;
        }

        public bool Load(string db, string group, string parameter, bool default_value)
        {
            bool value;
            return this.TryLoad(db, group, parameter, out value) ? value : default_value;
        }

        public double Load(string db, string group, string parameter, double default_value)
        {
            double value;
            return this.TryLoad(db, group, parameter, out value) ? value : default_value;
        }

        public bool TryLoad(string db, string group, string parameter, out string value)
        {
            value = this.Load(db, group, parameter);
            return value == null ? false : true;
        }

        public bool TryLoad(string db, string group, string parameter, out int value)
        {
            string value_str = this.Load(db, group, parameter);
            return int.TryParse(value_str, out value); ;
        }

        public bool TryLoad(string db, string group, string parameter, out bool value)
        {
            string value_str = this.Load(db, group, parameter);
            return bool.TryParse(value_str, out value); ;
        }
        public bool TryLoad(string db, string group, string parameter, out double value)
        {
            string value_str = this.Load(db, group, parameter);
            return double.TryParse(value_str, out value); ;
        }

    }
}
