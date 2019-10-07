using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gat.Base
{
    /// <summary>
    /// Json相關函式
    /// </summary>
    public class JsonFunc
    {
        /// <summary>
        /// 取得 JSON 序列化設定。
        /// </summary>
        /// <param name="options">Json序列化選項。</param>
        private static JsonSerializerSettings GetJsonSerializerSettings(JsonOptions options)
        {
            if (options == null)
                options = new JsonOptions();
            return GetJsonSerializerSettings(options.IgnoreDefaultValue, options.IgnoreNullValue, options.IncludeTypeName);
        }

        /// <summary>
        /// 取得 JSON 序列化設定。
        /// </summary>
        /// <param name="ignoreDefaultValue">是否忽略預設值。</param>
        /// <param name="ignoreNullValue">是否忽略 Null 值。</param>
        /// <param name="includeTypeName">是否包含型別名稱。</param>
        private static JsonSerializerSettings GetJsonSerializerSettings(bool ignoreDefaultValue, bool ignoreNullValue, bool includeTypeName)
        {
            var oSettings = new JsonSerializerSettings { Formatting = Formatting.Indented };
            // 忽略預設值
            if (ignoreDefaultValue)
                oSettings.DefaultValueHandling = DefaultValueHandling.Ignore;
            // 忽略 Null 值
            if (ignoreNullValue)
                oSettings.NullValueHandling = NullValueHandling.Ignore;
            // 加入型別名稱
            if (includeTypeName)
                oSettings.TypeNameHandling = TypeNameHandling.Auto;
            // 列舉型別使用字串
            oSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
            return oSettings;
        }

        /// <summary>
        /// 將物件序列化為 JSON 字串。
        /// </summary>
        /// <param name="value">物件。</param>
        /// <param name="options">Json序列化選項。</param>
        public static string ObjectToJson(object value, JsonOptions options = null)
        {
            // 序列化為 JSON 字串
            var oSettings = GetJsonSerializerSettings(options);
            var sJson = JsonConvert.SerializeObject(value, oSettings);

            return sJson;
        }

        /// <summary>
        /// 將 Json 字串反序列化為物件。
        /// </summary>
        /// <param name="json">JSON 字串。</param>
        /// <param name="type">型別。</param>
        /// <param name="options">Json序列化選項。</param>
        public static object JsonToObject(string json, Type type, JsonOptions options = null)
        {
            // 反序列化 JSON 字串
            var oSettings =  GetJsonSerializerSettings(options);
            var oValue = JsonConvert.DeserializeObject(json, type, oSettings);
            return oValue;
        }

        /// <summary>
        /// 將 Json 字串反序列化為物件。
        /// </summary>
        /// <typeparam name="T">泛型型別。</typeparam>
        /// <param name="json">JSON 字串。</param>
        /// <param name="options">Json序列化選項。</param>
        public static T JsonToObject<T>(string json, JsonOptions options = null)
        {
            return (T)JsonToObject(json, typeof(T), options);
        }
    }

    /// <summary>
    /// Json序列化選項
    /// </summary>
    public class JsonOptions
    {
        /// <summary>
        /// 是否忽略預設值
        /// </summary>
        public bool IgnoreDefaultValue { get; set; }
        /// <summary>
        /// 是否忽略 Null 值。
        /// </summary>
        public bool IgnoreNullValue { get; set; }
        /// <summary>
        /// 是否包含型別名稱
        /// </summary>
        public bool IncludeTypeName { get; set; }
    }
}
