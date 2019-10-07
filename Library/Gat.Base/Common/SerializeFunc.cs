using System;
using System.IO;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Newtonsoft.Json;

namespace Gat.Base
{
    /// <summary>
    /// 序列化函式庫。
    /// </summary>
    public class SerializeFunc
    {
        /// <summary>
        /// 將物件序列化為 XML 字串。
        /// </summary>
        /// <param name="value">物件。</param>
        public static string ObjectToXml(object value)
        {
            string sXml;
            XmlSerializer oSerializer = new XmlSerializer(value.GetType());
            using (StringWriter oStringWriter = new StringWriter())
            {
                oSerializer.Serialize((TextWriter)oStringWriter, value);
                sXml = oStringWriter.ToString();
            }

            return sXml;
        }

        /// <summary>
        /// 將 XML 字串反序列化為物件。
        /// </summary>
        /// <param name="xml">XML 字串。</param>
        /// <param name="type">型別。</param>
        public static object XmlToObject(string xml, Type type)
        {
            object oValue;
            XmlSerializer oSerializer = new XmlSerializer(type);
            using (StringReader oStringReader = new StringReader(xml))
            {
                oValue = oSerializer.Deserialize(oStringReader);
            }
            return oValue;
        }

        /// <summary>
        /// 將物件序列化為二進位資料。
        /// </summary>
        /// <param name="value">物件。</param>
        public static byte[] ObjectToBinary(object value)
        {
            BinaryFormatter oFormatter;
            byte[] oBuffer;

            using (MemoryStream stream = new MemoryStream())
            {
                oFormatter = new BinaryFormatter();
                oFormatter.Serialize(stream, value);
                oBuffer = stream.ToArray();
            }

            return oBuffer;
        }

        /// <summary>
        /// 將二進位資料反序列化為物件。
        /// </summary>
        /// <typeparam name="T">泛型型別。</typeparam>
        /// <param name="bytes">二進位資料。</param>
        public static T BinaryToObject<T>(byte[] bytes)
        {
            BinaryFormatter oFormatter;
            object oValue;

            using (MemoryStream stream = new MemoryStream(bytes))
            {
                oFormatter = new BinaryFormatter();
                oValue = oFormatter.Deserialize(stream);
            }

            return (T)oValue;
        }
    }
}
