using Gat.Base;
using Gat.Define;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;

namespace Gat.Cache
{
    /// <summary>
    /// 快取相關函式庫。
    /// </summary>
    public class CacheFunc
    {
        /// <summary>
        /// 快取暫存器
        /// </summary>
        private static ItemKeeper CacheKeeper { get; } = new ItemKeeper();

        /// <summary>
        /// json檔案轉型為定義
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filePath"></param>
        /// <returns></returns>
        private static T ConvertToDefine<T>(string filePath)
        {
            var json = FileFunc.FileReadAllText(filePath);
            return JsonFunc.JsonToObject<T>(json);
        }

        /// <summary>
        /// 取得程式定義檔案路徑
        /// </summary>
        /// <param name="progId"></param>
        /// <returns></returns>
        public static ProgramDefine GetProgramDefine(string progId)
        {
            return CacheKeeper.GetItem(progId, () => 
                ConvertToDefine<ProgramDefine>(SysDefineSettingName.ProgramDefineFilePath(progId))
            );
        }

        /// <summary>
        /// 取得公司項目設定
        /// </summary>
        /// <returns></returns>
        public static CompanySettings GetCompanySettings()
        {
            return CacheKeeper.GetItem(nameof(CompanySettings), () =>
                   ConvertToDefine<CompanySettings>(SysDefineSettingName.CompanySettingPath));
        }

        /// <summary>
        /// 取得資料庫設定檔案資料表
        /// </summary>
        /// <returns></returns>
        public static DatabaseSettings GetDatabaseSettings()
        {
            return CacheKeeper.GetItem(nameof(DatabaseSettings), () =>
                   ConvertToDefine<DatabaseSettings>(SysDefineSettingName.DbSettingPath));
        }

        /// <summary>
        /// 取得程式設定檔案路徑
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<ProgramSetting> GetProgramSettings()
        {
            InitProgramSetting();
            return CacheKeeper.GetAllItems<ProgramSetting>();
        }

        /// <summary>
        /// 取得程式設定檔案路徑
        /// </summary>
        /// <returns></returns>
        public static ProgramSetting GetProgramSetting()
        {
            InitProgramSetting();
            var result = CacheKeeper.GetItem<ProgramSetting>(nameof(ProgramSetting));
            if (result != null)
                return result;

            ExtractProgramSetting();
            return CacheKeeper.GetItem<ProgramSetting>(nameof(ProgramSetting));
        }

        /// <summary>
        /// 取得程式項目
        /// </summary>
        /// <param name="progId"></param>
        /// <returns></returns>
        public static ProgramItem GetProgramItem(string progId)
        {
            InitProgramSetting();
            var result = CacheKeeper.GetItem<ProgramItem>(progId);
            if (result != null)
                return result;

            return CacheKeeper.GetItem<ProgramItem>(progId);
        }

        /// <summary>
        /// 初始化程式設定
        /// </summary>
        private static void InitProgramSetting()
        {
            if (CacheKeeper.HasItem<ProgramSetting>())
                return;

            ExtractProgramSetting();
        }

        /// <summary>
        /// 展開程式設定
        /// </summary>
        private static void ExtractProgramSetting()
        {
            var progSettings = from f in Directory.EnumerateFiles(SysDefineSettingName.SystemPath, SysDefineSettingName.ProgramSettingName, SearchOption.AllDirectories)
                               select new { Setting = JsonFunc.JsonToObject<ProgramSetting>(FileFunc.FileReadAllText(f)) };
            foreach (var set in progSettings)
            {
                if (!CacheKeeper.HasItem<ProgramSetting>(nameof(ProgramSetting)))
                {
                    CacheKeeper.AddItem(nameof(ProgramSetting), set.Setting);
                    ExtractProgramSetting(set.Setting);
                }
            }
        }

        /// <summary>
        /// 展開程式設定的程式項目，快取到快取暫存器
        /// </summary>
        /// <param name="progSetting"></param>
        private static void ExtractProgramSetting(ProgramSetting progSetting)
        {
            foreach (ProgramItem progItem in progSetting.Items)
            {
                CacheKeeper.AddItem(progItem.ProgId, progItem);
            }
        }

        /// <summary>
        /// 取得實體資料表定義列舉
        /// </summary>
        /// <param name="progId"></param>
        /// <returns></returns>
        private static void InitDbTableDefines()
        {
            InitProgramSetting();
            var progSettings = CacheKeeper.GetAllItems<ProgramSetting>();
            if (!progSettings.Any())
                return;

            foreach (var progSetting in progSettings)
            {
                var progItems = CacheKeeper.GetAllItems<ProgramItem>();
                foreach (var item in progItems)
                {
                    CacheKeeper.GetItem(item.ProgId, () =>
                        ConvertToDefine<DbTableDefine>(SysDefineSettingName.DbTableDefineFilePath(item.ProgId))
                    );
                }
            }
        }

        /// <summary>
        /// 取得實體資料表定義列舉
        /// </summary>
        /// <param name="progId"></param>
        /// <returns></returns>
        public static IEnumerable<DbTableDefine> GetDbTableDefines()
        {
            InitProgramSetting();
            InitDbTableDefines();
            return CacheKeeper.GetAllItems<DbTableDefine>();
        }

        /// <summary>
        /// 由快取區取得程式定義。
        /// </summary>
        /// <param name="sessionGuid">連線識別</param>
        /// <param name="progId">程式代碼。</param>
        public static SessionInfo GetSessionInfo(Guid sessionGuid)
        {
            return CacheKeeper.GetItem<SessionInfo>(sessionGuid.ToString(), () => new SessionInfo { SessionGuid = sessionGuid });
        }
    }
}
