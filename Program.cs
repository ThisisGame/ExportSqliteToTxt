using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Data.SQLite;
using System.Collections;
using System.Data;

namespace ExportSqliteToTxt
{
    class Program
    {
        static void Main(string[] args)
        {
            string tmpdbpath = args[0];
            string tmptextpath = args[1];

            StringBuilder tmpContent = new StringBuilder();


            SQLiteDatabase db = new SQLiteDatabase(tmpdbpath);
            DataTable recipe;
            String query = "SELECT name FROM sqlite_master " +
                    "WHERE type = 'table'" +
                    "ORDER BY 1";

            recipe = db.GetDataTable(query);

            ArrayList list = new ArrayList();
            foreach (DataRow row in recipe.Rows)
            {
                list.Add(row.ItemArray[0].ToString());
            }


            

            //foreach (var item in list)
            for(int tableIndex=0;tableIndex<list.Count;tableIndex++)
            {
                Console.WriteLine(tableIndex + "/" + list.Count + "    : " + list[tableIndex]);

                DataTable tmpDataTable = db.GetDataTable("select * from " + list[tableIndex]);
                foreach (var tableRow in tmpDataTable.Rows)
                {
                    DataRow tmpRow = tableRow as DataRow;
                    for (int i = 0; i < tmpRow.ItemArray.Length; i++)
                    {
                        tmpContent.Append(tmpRow.ItemArray[i].ToString());
                    }
                }
            }


            //{
            //    tmpContent = tmpContent.Replace(Environment.NewLine, string.Empty);

            //    //只要中文
            //    string tmpStrChinese = "";
            //    for (int i = 0; i < tmpContent.Length; i++)
            //    {
            //        if ((int)tmpContent[i] > 127)
            //        {
            //            tmpStrChinese += tmpContent[i];
            //        }
            //    }
            //    tmpStrChinese += "0123456789qwertyuiopasdfghjklmnbvcxzQWERTYUIOPLKJHGFDSAZXCVBNM,<.>/?';:[{]}\\|=+-_)(*&^%$#@!`~\\\"";

            //    int lenth = tmpStrChinese.Length;


            //}

            using (StreamWriter sw = new StreamWriter(tmptextpath, true))
            {
                sw.Write(tmpContent);
                sw.Flush();
                sw.Close();
            }

            Console.WriteLine("Finish");

            Console.ReadLine();
        }
    }
}
