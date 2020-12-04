﻿using IniParser;
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
        public void Save(string db_path, string group, string parameter, string value)
        {
            var parser = new FileIniDataParser();
            if (File.Exists(db_path))
            {
                IniData data;
                try
                {
                    data = parser.ReadFile(db_path);
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine(
                        $">>{ex.Message}\r\n" +
                        $"{ex.ToString()}");

                    data = new IniData();

                }
                data[group][parameter] = value;
                parser.WriteFile(db_path, data);
            }
            else
            {

                string directory = Path.GetDirectoryName(db_path);

                if (directory != "" && !Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                IniData data = new IniData();
                data[group][parameter] = value;
                parser.WriteFile(db_path, data);
            }
        }
        
        public void Save<T>(string db_path, string group, string parameter, T value)
        {
            this.Save(db_path, group, parameter, Convert.ToString(value));
        }


        public string Load(string db_path, string group, string parameter)
        {
            IniData data;
            if (File.Exists(db_path))
            {
                try
                {
                    var parser = new FileIniDataParser();
                    data = parser.ReadFile(db_path);
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
                
        public T Load<T>(string db_path, string group, string parameter, T default_value)
        {
            T value;
            return this.TryLoad(db_path, group, parameter, out value) ? value : default_value;
        }        

        public bool TryLoad<T>(string db_path, string group, string parameter, out T value)
        {
            // https://stackoverflow.com/questions/2961656/generic-tryparse
            string value_str = this.Load(db_path, group, parameter);
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

        public IniData Load(string db_path)
        {
            IniData data;
            if (File.Exists(db_path))
            {
                try
                {
                    var parser = new FileIniDataParser();
                    data = parser.ReadFile(db_path);
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

            return data;
        }
        
        public bool TryLoad<T>(IniData db_mem, string group, string parameter, out T value)
        {
            string value_str = db_mem[group][parameter];
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

        public T Load<T>(IniData db_mem, string group, string parameter, T default_value)
        {
            T value;
            return this.TryLoad(db_mem, group, parameter, out value) ? value : default_value;
        }

        public void Insert<T>(IniData db_mem, string group, string parameter, T value)
        {
            db_mem[group][parameter] = Convert.ToString(value);
        }

        public void Save(string db_path, IniData db_mem)
        {
            var parser = new FileIniDataParser();
            if (File.Exists(db_path))
            {
                IniData data;
                try
                {
                    data = parser.ReadFile(db_path);
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine(
                        $">>{ex.Message}\r\n" +
                        $"{ex.ToString()}");

                    data = new IniData();
                }
                data.Merge(db_mem);
                parser.WriteFile(db_path, data);
            }
            else
            {

                string directory = Path.GetDirectoryName(db_path);

                if (directory != "" && !Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                IniData data = new IniData();
                parser.WriteFile(db_path, db_mem);
            }
        }
    }
}
