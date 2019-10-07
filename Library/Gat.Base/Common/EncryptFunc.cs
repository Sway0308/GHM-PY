using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Gat.Base
{
    /// <summary>
    /// 加解密共用函式
    /// </summary>
    public class EncryptFunc
    {
        /// <summary>
        /// 使用 HMAC-MD5 演算法進行資料加密。
        /// </summary>
        /// <param name="s">原字串。</param>
        /// <param name="key">加密鍵值。</param>
        public static string HMACMD5Encrypt(string s, string key)
        {
            ASCIIEncoding oEncoding;
            byte[] oKeyByte;
            byte[] oMessageBytes;
            HMACMD5 oMD5;

            oEncoding = new ASCIIEncoding();
            oKeyByte = oEncoding.GetBytes(key);
            oMessageBytes = oEncoding.GetBytes(s);
            oMD5 = new HMACMD5(oKeyByte);
            return BaseFunc.ByteToHex(oMD5.ComputeHash(oMessageBytes));
        }

        /// <summary>
        /// 使用 SaltHash 演算法進行資料加密。
        /// </summary>
        /// <param name="s">原字串。</param>
        /// <param name="key">加密鍵值。</param>
        public static string SaltHashEncrypt(string s, string key = "")
        {
            string sSalted = key;
            byte[] oOriginal = null;
            byte[] oSaltValue = null;
            byte[] oToSalt = null;
            MD5 oMD5 = null;
            byte[] oSaltPWD = null;
            byte[] oPWD = null;

            if (StrFunc.StrIsEmpty(sSalted))
            {
                sSalted = "scskxmd";
            }

            oOriginal = Encoding.Default.GetBytes(s);
            oSaltValue = Encoding.Default.GetBytes(sSalted);
            oToSalt = new byte[oOriginal.Length + oSaltValue.Length];

            oOriginal.CopyTo(oToSalt, 0);
            oSaltValue.CopyTo(oToSalt, oOriginal.Length);

            oMD5 = MD5.Create();
            oSaltPWD = oMD5.ComputeHash(oToSalt);
            oPWD = new byte[oSaltPWD.Length + oSaltValue.Length];
            oSaltPWD.CopyTo(oPWD, 0);
            oSaltValue.CopyTo(oPWD, oSaltPWD.Length);

            return Convert.ToBase64String(oPWD);
        }

        /// <summary>
        /// DES字串加密(隨機密碼)。
        /// </summary>
        /// <param name="str">未加密的文字。</param>
        public static string DESEncrypt(string str)
        {
            string s = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string sPassword = string.Empty;
            string sEncrypt = string.Empty;

            //空字串無法加密，直接回傳空字串。
            if (StrFunc.StrIsEmpty(str))
            {
                return string.Empty;
            }

            sPassword = BaseFunc.RndString(s, 8);
            sEncrypt = DESEncrypt(str, sPassword);
            //將密碼儲存於加密字串中
            sEncrypt = StrFunc.StrLeft(sPassword, 2) + sEncrypt.Insert(8, StrFunc.StrSubstring(sPassword, 2, 4)) + StrFunc.StrRight(sPassword, 2);

            return sEncrypt;
        }

        /// <summary>
        /// DES字串加密。
        /// </summary>
        /// <param name="str">未加密的文字。</param>
        /// <param name="password">密碼，需為 8 碼英文+數值的組合密碼。</param>
        public static string DESEncrypt(string str, string password)
        {
            DESCryptoServiceProvider oDESCrypto = null;
            byte[] oInputByteArray = null;
            StringBuilder oOutputText = null;
            byte oByte = 0;
            MemoryStream oMemoryStream = null;
            CryptoStream oCryptoStream = null;

            oDESCrypto = new DESCryptoServiceProvider();
            oInputByteArray = Encoding.Default.GetBytes(str);

            //建立加密對象的密碼和初始化向量
            oDESCrypto.Key = ASCIIEncoding.ASCII.GetBytes(password);
            oDESCrypto.IV = ASCIIEncoding.ASCII.GetBytes(password);

            //寫入加密後的資料到資料流
            oMemoryStream = new System.IO.MemoryStream();
            oCryptoStream = new CryptoStream(oMemoryStream, oDESCrypto.CreateEncryptor(), CryptoStreamMode.Write);
            oCryptoStream.Write(oInputByteArray, 0, oInputByteArray.Length);
            oCryptoStream.FlushFinalBlock();

            //輸出加密後的字串
            oOutputText = new StringBuilder();
            foreach (byte oByte_loopVariable in oMemoryStream.ToArray())
            {
                oByte = oByte_loopVariable;
                oOutputText.AppendFormat("{0:X2}", oByte);
            }

            return oOutputText.ToString();
        }

        /// <summary>
        /// DES字串解密(隨機密碼)。
        /// </summary>
        /// <param name="str">加密的字串。</param>
        public static string DESDecrypt(string str)
        {
            bool bResult;

            return DESDecrypt(str, out bResult);
        }

        /// <summary>
        /// DES字串解密(隨機密碼)。
        /// </summary>
        /// <param name="str">加密的字串。</param>
        /// <param name="result">傳出是否解密成功。</param>
        public static string DESDecrypt(string str, out bool result)
        {
            string sPassword = string.Empty;
            string sEncrypt = string.Empty;
            string sDecrypt;
            string sValue;

            result = false;

            if (StrFunc.StrIsEmpty(str)) { return string.Empty; }
            if (str.Length < 10) { return str; }

            //密碼
            sPassword = StrFunc.StrLeft(str, 2) + StrFunc.StrSubstring(str, 10, 4) + StrFunc.StrRight(str, 2);
            //加密字串
            sValue = StrFunc.StrLeftRightCut(str, 2);
            sEncrypt = StrFunc.StrSubstring(sValue, 0, 8) + StrFunc.StrSubstring(sValue, 12, sValue.Length - 12);

            try
            {
                sDecrypt = DESDecrypt(sEncrypt, sPassword);
                result = true;
                return sDecrypt;
            }
            catch
            {
                return str;
            }
        }

        /// <summary>
        /// 字串解密。
        /// </summary>
        /// <param name="str">加密的字串。</param>
        /// <param name="password">密碼。</param>
        public static string DESDecrypt(string str, string password)
        {
            DESCryptoServiceProvider oDESCrypto = null;
            int iLen = 0;
            int N1 = 0;
            int iByte = 0;
            System.IO.MemoryStream oMemoryStream = null;
            CryptoStream oCryptoStream = null;

            if (StrFunc.StrIsEmpty(str))
            {
                return string.Empty;
            }

            oDESCrypto = new DESCryptoServiceProvider();
            //將字串放入 Byte 數組。
            iLen = Convert.ToInt32(str.Length / 2) - 1;
            byte[] inputByteArray = new byte[iLen + 1];
            for (N1 = 0; N1 <= iLen; N1++)
            {
                iByte = Convert.ToInt32(str.Substring(N1 * 2, 2), 16);
                inputByteArray[N1] = Convert.ToByte(iByte);
            }

            //建立加密對象的密碼和初始化向量
            oDESCrypto.Key = ASCIIEncoding.ASCII.GetBytes(password);
            oDESCrypto.IV = ASCIIEncoding.ASCII.GetBytes(password);
            oMemoryStream = new System.IO.MemoryStream();
            oCryptoStream = new CryptoStream(oMemoryStream, oDESCrypto.CreateDecryptor(), CryptoStreamMode.Write);
            oCryptoStream.Write(inputByteArray, 0, inputByteArray.Length);
            oCryptoStream.FlushFinalBlock();

            return Encoding.Default.GetString(oMemoryStream.ToArray());
        }
    }
}
