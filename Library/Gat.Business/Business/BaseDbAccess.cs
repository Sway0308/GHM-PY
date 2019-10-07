using Gat.Cache;
using Gat.Database;
using Gat.Define;
using System;

namespace Gat.Business
{
    /// <summary>
    /// 具有連線資訊及資料庫存取的元件基底類別。
    /// </summary>
    public class BaseDbAccess
    {
        private SessionInfo _SessionInfo;

        /// <summary>
        /// 建構函式
        /// </summary>
        public BaseDbAccess(Guid sessionGuid)
        {
            var dbSetting = CacheFunc.GetDatabaseSettings();
            this.DbAccess = new DbAccess(dbSetting);
        }

        /// <summary>
        /// 資料庫存取物件。
        /// </summary>
        public DbAccess DbAccess { get; }

        /// <summary>
        /// 連線識別
        /// </summary>
        public Guid SessionGuid { get; }

        /// <summary>
        /// 連線資訊
        /// </summary>
        public SessionInfo SessionInfo
        {
            get
            {
                if (_SessionInfo == null)
                    _SessionInfo = CacheFunc.GetSessionInfo(this.SessionGuid);
                return _SessionInfo;
            }
        }
    }
}