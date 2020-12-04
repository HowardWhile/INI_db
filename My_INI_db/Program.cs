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
            INI_db db = new INI_db();

            // db
            string db_path = "db.ini";

            // data
            bool bool_value = true;
            int int_value = 100;
            double f_value = 123.321;
            string str_value = "Hello";

            db.Save(db_path, "Group1", "Boolean", bool_value);
            db.Save(db_path, "Group1", "Integer", int_value);
            db.Save(db_path, "Group1", "Float", f_value);
            db.Save(db_path, "Group1", "String", str_value);
            

            Console.ReadKey();
        }
    }
}
