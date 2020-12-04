using AIM.Modules;
using IniParser.Model;
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
            // db
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
            System.Console.WriteLine($"data.ini load... {watch_save.ElapsedMilliseconds} ms");


            Console.ReadKey();
        }
    }
}
