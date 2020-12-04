using IniParser;
using IniParser.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        
        public void Save<T>(string db, string group, string parameter, T value)
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
                
        public T Load<T>(string db, string group, string parameter, T default_value)
        {
            T value;
            return this.TryLoad(db, group, parameter, out value) ? value : default_value;
        }        

        public bool TryLoad<T>(string db, string group, string parameter, out T value)
        {
            // https://stackoverflow.com/questions/2961656/generic-tryparse
            string value_str = this.Load(db, group, parameter);
            try
            {
                var converter = TypeDescriptor.GetConverter(typeof(T));
                if (converter != null)
                {
                    // Cast ConvertFromString(string text) : object to (T)
                    value = (T)converter.ConvertFromString(value_str);
                    return true;
                }
                value = default(T);
                return false;
            }
            catch (Exception)
            {
                value = default(T);
                return false;
            }
        }

    }
}
