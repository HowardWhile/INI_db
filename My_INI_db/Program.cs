using AIM.Modules;
using IniParser.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My_INI_db
{
    class Program
    {
        static void Main(string[] args)
        {
            example_simply();
            example_db();

            // How about json
            example_json();
            example_json_db();

            Console.ReadKey();
        }

        private static void example_json()
        {
            INI_db db_tool = new INI_db();
            // Save json to .ini
            dynamic json_data = new JObject();
            json_data.Boolean = false;
            json_data.Integer = 200;
            json_data.Float = 234.567;
            json_data.String = "Hello World";
            json_data.Array = new JArray(1, 2, 3, 4, 5);
            db_tool.Save("json.ini", "Group1", "Json", json_data);

            // Load json from .ini
            JObject j_default = new JObject();
            JObject j = db_tool.Load("json.ini", "Group1", "Json", j_default);
            Console.WriteLine("json_value = {0}", j.ToString());
        }

        private static void example_simply()
        {

            // db_tool
            INI_db db_tool = new INI_db();
            string db_path = "db.ini";

            // save example
            db_tool.Save(db_path, "Group1", "Boolean", true);
            db_tool.Save(db_path, "Group1", "Integer", 100);
            db_tool.Save(db_path, "Group1", "Float", 123.321);
            db_tool.Save(db_path, "Group1", "String", "Hello");


            // load example
            int value_default = 123;
            System.Console.WriteLine("data = {0}", db_tool.Load(db_path, "Group1", "Integer", value_default));
            System.Console.WriteLine("data = {0}", db_tool.Load(db_path, "Group1", "Integer_1", value_default));

            // tryload example 
            double oValue;
            if (db_tool.TryLoad(db_path, "Group1", "Float", out oValue))
                Console.WriteLine($"Load Success, data = {oValue}");
            else
                Console.WriteLine("Load Failed");
        }

        private static void example_db()
        {
            INI_db db_tool = new INI_db();
            // Save DB
            System.Diagnostics.Stopwatch watch_save = new System.Diagnostics.Stopwatch();
            Random rand = new Random();

            watch_save.Start();
            int k_group_num = 50; // 5x100
            int k_para_num = 100;
            IniData db_mem = new IniData();
            for (int idx_group = 0; idx_group < k_group_num; idx_group++)
            {
                for (int idx_para = 0; idx_para < k_para_num; idx_para++)
                {
                    //db.Save("data.ini", $"Group_{idx_group}", $"Value_{idx_para}", rand.NextDouble());
                    db_tool.Insert(db_mem, $"Group_{idx_group}", $"Value_{idx_para}", rand.NextDouble());
                    //System.Console.WriteLine("({0},{1})", idx_group, idx_para);
                }
            }
            db_tool.Save("data.ini", db_mem);
            watch_save.Stop();

            // Load DB
            System.Diagnostics.Stopwatch watch_load = new System.Diagnostics.Stopwatch();
            watch_load.Start();
            db_mem = db_tool.Load("data.ini");
            for (int idx_group = 0; idx_group < k_group_num; idx_group++)
            {
                for (int idx_para = 0; idx_para < k_para_num; idx_para++)
                {
                    //double f_value;
                    //db.TryLoad(db_mem, $"Group_{idx_group}", $"Value_{idx_para}", out f_value);
                    double f_value = db_tool.Load(db_mem, $"Group_{idx_group}", $"Value_{idx_para}", 0.0);
                    //System.Console.WriteLine("({0},{1}) = {2}", idx_group, idx_para, f_value);
                }
            }
            watch_load.Stop();
            System.Console.WriteLine($"data.ini save... {watch_save.ElapsedMilliseconds} ms");
            System.Console.WriteLine($"data.ini load... {watch_load.ElapsedMilliseconds} ms");
        }

        private static void example_json_db()
        {
            INI_db db_tool = new INI_db();

            // Save DB
            System.Diagnostics.Stopwatch watch_save = new System.Diagnostics.Stopwatch();
            System.Diagnostics.Stopwatch watch_load = new System.Diagnostics.Stopwatch();
            Random rand = new Random();

            watch_save.Start();
            int k_group_num = 50; // 5x100
            int k_para_num = 100;

            IniData db_mem = new IniData();
            for (int idx_group = 0; idx_group < k_group_num; idx_group++)
            {
                for (int idx_para = 0; idx_para < k_para_num; idx_para++)
                {
                    dynamic json_data = new JObject();
                    json_data.Integer = rand.Next();
                    json_data.Float = rand.NextDouble();
                    db_tool.Insert(db_mem, $"Group_{idx_group}", $"Value_{idx_para}", json_data);
                }
            }
            db_tool.Save("json_db.ini", db_mem);
            watch_save.Stop();

            // Load DB
            watch_load.Start();
            db_mem = db_tool.Load("json_db.ini");
            for (int idx_group = 0; idx_group < k_group_num; idx_group++)
            {
                for (int idx_para = 0; idx_para < k_para_num; idx_para++)
                {
                    JObject j_default = new JObject();
                    JObject j = db_tool.Load(db_mem, $"Group_{idx_group}", $"Value_{idx_para}", j_default);
                }
            }
            watch_load.Stop();

            System.Console.WriteLine($"json_db.ini save... {watch_save.ElapsedMilliseconds} ms");
            System.Console.WriteLine($"json_db.ini load... {watch_load.ElapsedMilliseconds} ms");
        }

        
    }
}
