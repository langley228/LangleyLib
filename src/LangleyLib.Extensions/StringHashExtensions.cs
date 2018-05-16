using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LangleyLib.Extensions
{
    /// <summary>
    /// 字串加解密相關 Extensions
    /// </summary>
    public static class StringHashExtensions
    {
        /// <summary>字串轉Base64編碼 (UTF8)</summary>
        /// <param name="source">字串值</param>
        /// <returns></returns>
        public static string ToEncodeBase64(this string source)
        {
            return source.ToEncodeBase64(Encoding.UTF8);
        }
        /// <summary>字串轉Base64編碼</summary>
        /// <param name="source">字串值</param>
        /// <param name="encoding">編碼方式</param>
        /// <returns></returns>
        public static string ToEncodeBase64(this string source, Encoding encoding)
        {
            if (string.IsNullOrEmpty(source))
                return "";

            byte[] textAsBytes = encoding.GetBytes(source);
            return Convert.ToBase64String(textAsBytes);
        }
        /// <summary>Base64編碼字串解碼 (UTF8)</summary>
        /// <param name="encodedText">Base64編碼字串值</param>
        /// <returns></returns>
        public static string ToDecodeBase64(this string encodedText)
        {
            return encodedText.ToDecodeBase64(Encoding.UTF8);
        }

        /// <summary>Base64編碼字串解碼</summary>
        /// <param name="encodedText">Base64編碼字串值</param>
        /// <param name="encoding">編碼方式</param>
        /// <returns></returns>
        public static string ToDecodeBase64(this string encodedText, Encoding encoding)
        {
            if (string.IsNullOrEmpty(encodedText))
                return "";

            byte[] textAsBytes = System.Convert.FromBase64String(encodedText);
            return encoding.GetString(textAsBytes);
        }

        /// <summary>字串轉64位元編碼 (UTF8)</summary>
        /// <param name="source">字串值</param>
        /// <returns></returns>
        public static string ToHexEncrypt(this string source)
        {
            return source.ToHexEncrypt(Encoding.UTF8); 
        }
        /// <summary>字串轉64位元編碼</summary>
        /// <param name="source">字串值</param>
        /// <param name="encoding">編碼方式</param>
        /// <returns></returns>
        public static string ToHexEncrypt(this string source, Encoding encoding)
        {
            var sb = new StringBuilder();
            var bytes = encoding.GetBytes(source);
            foreach (var t in bytes)
            {
                sb.Append(t.ToString("X2"));
            }
            return sb.ToString(); 
        }
        /// <summary>64位元編碼轉字串 (UTF8)</summary>
        /// <param name="hexString">64位元編碼字串值</param>
        /// <returns></returns>
        public static string ToHexDecrypt(this string hexString)
        {
            return hexString.ToHexDecrypt(Encoding.UTF8); 
        }
        /// <summary>64位元編碼轉字串</summary>
        /// <param name="hexString">64位元編碼字串值</param>
        /// <param name="encoding">編碼方式</param>
        /// <returns></returns>
        public static string ToHexDecrypt(this string hexString, Encoding encoding)
        {
            var bytes = new byte[hexString.Length / 2];
            for (var i = 0; i < bytes.Length; i++)
            {
                bytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            }
            return encoding.GetString(bytes); 
        }

        /// <summary>字串 MD5 加密 (系統預設編碼 Encoding.Default)</summary>
        /// <param name="text">字串值</param>
        /// <returns></returns>
        public static string ToMd5Hash(this string text)
        {
            return text.ToMd5Hash(Encoding.Default);
        }
        /// <summary>字串 MD5 加密</summary>
        /// <param name="source">字串值</param>
        /// <param name="encoding">編碼方式</param>
        /// <returns></returns>
        public static string ToMd5Hash(this string source, Encoding encoding)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                // Convert the input string to a byte array and compute the hash.
                byte[] data = md5Hash.ComputeHash(encoding.GetBytes(source));

                // Create a new Stringbuilder to collect the bytes
                // and create a string.
                StringBuilder sBuilder = new StringBuilder();

                // Loop through each byte of the hashed data 
                // and format each one as a hexadecimal string.
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("X2"));
                }

                // Return the hexadecimal string.
                return sBuilder.ToString();
            }
        }

        /// <summary>檢查MD5加密是否相等 (系統預設編碼 Encoding.Default)</summary>
        /// <param name="source">字串值</param>
        /// <param name="hash">比對Hash值</param>
        /// <returns></returns>
        public static bool VerifyMd5Hash(this string source, string hash)
        {
            return source.VerifyMd5Hash(hash, Encoding.Default);
        }

        /// <summary>檢查MD5加密是否相等</summary>
        /// <param name="source">字串值</param>
        /// <param name="hash">比對Hash值</param>
        /// <param name="encoding">編碼方式</param>
        /// <returns></returns>
        public static bool VerifyMd5Hash(this string source, string hash, Encoding encoding)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                // Hash the input.
                string hashOfInput = source.ToMd5Hash(encoding);

                // Create a StringComparer an compare the hashes.
                StringComparer comparer = StringComparer.OrdinalIgnoreCase;

                if (0 == comparer.Compare(hashOfInput, hash))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

    }
}
