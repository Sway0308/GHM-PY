using Gat.Base;
using Gat.Define;
using System;

namespace SettingHelper
{
    public class ProgramDefineHelper
    {
        public void CreateProgramDefine()
        {
            var ans = true;
            while (ans)
            {
                DoCreateProgramDefine();
                Console.WriteLine("是否繼續建立程式定義？0：No, 1：Yes");
                var doNext = Console.ReadLine();
                if (doNext.SameTextOr("0", "No"))
                    ans = false;
                Console.WriteLine("================================================");
            }
            Console.WriteLine("Bye bye");
            Console.ReadKey();
        }

        public void DoCreateProgramDefine()
        {
            var helper = new InitDataHelper();

            var progDefine = new ProgramDefine();
            Console.WriteLine("progId：");
            progDefine.ProgId = Console.ReadLine();
            Console.WriteLine("DisplayName：");
            progDefine.DisplayName = Console.ReadLine();
            Console.WriteLine("Start Create Table");
            var tableDefine = new TableDefine();
            tableDefine.PrimaryKey = progDefine.ProgId + "Id";
            progDefine.Tables.Add(tableDefine);
            Console.WriteLine("TableName：");
            tableDefine.TableName = Console.ReadLine();
            Console.WriteLine("DbTableName：");
            tableDefine.DbTableName = Console.ReadLine();
            Console.WriteLine("Table DisplayName：");
            tableDefine.DisplayName = Console.ReadLine();
            var createField = true;
            while (createField)
            {
                Console.WriteLine("Start Create Field Define");
                var fieldDefine = new FieldDefine();
                tableDefine.Fields.Add(fieldDefine);
                Console.WriteLine("FieldName：");
                fieldDefine.FieldName = Console.ReadLine();
                Console.WriteLine("DisplayName：");
                fieldDefine.DisplayName = Console.ReadLine();
                Console.WriteLine("DbType：");
                Console.WriteLine("(0：String, 1：Text, 2：Boolean, 3：Integer, 4：Double, 5：Currency, 6：DateTime, 7：GUID, 8：Binary)");
                var dbType = int.Parse(Console.ReadLine());
                fieldDefine.DbType = BaseFunc.CEnum<EFieldDbType>(dbType, 0);
                Console.WriteLine("MaxLength：");
                fieldDefine.MaxLength = int.Parse(Console.ReadLine());
                Console.WriteLine("AllowNull：(0: false, 1: true)");
                fieldDefine.AllowNull = BaseFunc.CBool(Console.ReadLine());
                Console.WriteLine("LinkFieldName：");
                fieldDefine.LinkFieldName = Console.ReadLine();
                Console.WriteLine("LinkprogId：");
                fieldDefine.LinkProgId = Console.ReadLine();
                Console.WriteLine("Continue Create Field Define? 1：Yes? 0：No?");
                var ans = Console.ReadLine();
                if (ans.SameTextOr("No", "0"))
                    createField = false;
                Console.WriteLine("================================================");
            }

            helper.ProgDefineToJson(progDefine);
            Console.WriteLine($"已產生程式定義檔案：{progDefine.ProgId}/{progDefine.DisplayName}");
            Console.ReadKey();
        }
    }
}
