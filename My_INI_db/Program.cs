using AIM.Modules;
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
            INI_db db = new INI_db();           
            string db_path = "db.ini";

            // save example
            db.Save(db_path, "Group1", "Boolean", true);
            db.Save(db_path, "Group1", "Integer", 100);
            db.Save(db_path, "Group1", "Float", 123.321);
            db.Save(db_path, "Group1", "String", "Hello");

            // load example
            int value_default = 123;
            System.Console.WriteLine("data = {0}", db.Load(db_path, "Group1", "Integer", value_default));
            System.Console.WriteLine("data = {0}", db.Load(db_path, "Group1", "Integer_1", value_default));

            // tryload example 
            double oValue;
            if (db.TryLoad(db_path, "Group1", "Float", out oValue))
                Console.WriteLine($"Load Success, data = {oValue}");
            else
                Console.WriteLine("Load Failed");

            if (db.TryLoad(db_path, "Group1", "Float_1", out oValue))
                Console.WriteLine($"Load Success, data = {oValue}");
            else
                Console.WriteLine("Load Failed");


            Console.ReadKey();
        }
    }
}
