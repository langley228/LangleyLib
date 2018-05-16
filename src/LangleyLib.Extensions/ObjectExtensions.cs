using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangleyLib.Extensions
{

    /// <summary>object 相關功能 的 Extensions</summary>
    public static class ObjectExtensions
    {
        /// <summary>轉換為任意型態</summary>
        /// <typeparam name="T">型態</typeparam>
        /// <param name="source"></param>
        /// <param name="defaultValue">預設值 ex : default(T)</param>
        /// <returns></returns>
        public static T ToAnyType<T>(this object source, T defaultValue = default(T)) where T : struct
        {
            T ret = defaultValue;
            try
            {
                ret = (T)Convert.ChangeType(source, typeof(T));
            }
            catch (Exception)
            {

            }
            return ret;
        }

    }
}
