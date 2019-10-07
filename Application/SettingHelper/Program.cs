using Gat.Base;
using Gat.Business;
using Gat.Define;
using System;
using System.Configuration;
using System.Linq;
using Gat.Business.Hum;

namespace SettingHelper
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var appData = ConfigurationManager.AppSettings["App_Data"];
            BaseInfo.AppDataPath = appData;
            DemoSetting();
        }

        /// <summary>
        /// 機制Demo
        /// </summary>
        private static void DemoSetting()
        {
            var helper = new InitDataHelper();

            DoDemoSetting(helper);
        }

        private static void DoDemoSetting(InitDataHelper helper)
        {
            Console.Clear();
            Console.WriteLine("1. 定義匯入，2. 資料處理，or Exit？");
            var ans = Console.ReadLine();
            if (ans == "1")
                ImportDefine(helper);
            else if (ans == "2")
                DataCrud(helper);
            else if (ans.ToUpper().Equals("EXIT"))
            {
                Console.WriteLine("bye bye...");
                Console.ReadKey();
                return;
            }
            else
                Console.WriteLine("輸入錯誤");

            Console.ReadKey();
            DoDemoSetting(helper);
        }

        private static void ImportDefine(InitDataHelper helper)
        {
            Console.WriteLine("程式定義匯入");
            Console.ReadKey();
            helper.InitData();
            Console.WriteLine("程式定義匯入完成");
            Console.WriteLine("==================================");

            Console.WriteLine("資料表升級");
            Console.ReadKey();
            helper.CreateDbTable();
            Console.WriteLine("資料表完成");
            Console.WriteLine("==================================");

            Console.WriteLine("新增資料");
            Console.ReadKey();
            helper.AddData();
            Console.WriteLine("新增資料完成");
            Console.WriteLine("==================================");
        }

        private static void DataCrud(InitDataHelper helper)
        {
            Console.WriteLine("查詢清單資料");
            Console.ReadKey();
            Console.WriteLine("請輸入程式代碼：");
            var progId = Console.ReadLine();
            var list = helper.FindData(progId);
            Console.WriteLine(list);
            Console.WriteLine("查詢清單資料完成");
            Console.WriteLine("==================================");

            Console.WriteLine("查詢單筆資料");
            Console.ReadKey();
            Console.WriteLine("請輸入單據號碼：");
            var formId = Console.ReadLine();
            var form = helper.FindSingleData(progId, formId);
            Console.WriteLine(form);
            Console.WriteLine("查詢單筆資料完成");
            Console.WriteLine("==================================");

            Console.WriteLine("修改資料");
            Console.ReadKey();
            helper.EditData();
            Console.WriteLine("修改資料完成");
            Console.WriteLine("==================================");

            Console.WriteLine("刪除資料");
            Console.ReadKey();
            helper.DeleteData();
            Console.WriteLine("刪除資料完成");
            Console.WriteLine("==================================");
        }
    }
}
