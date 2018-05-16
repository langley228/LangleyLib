using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangleyLib.AdLib
{

    /// <summary>
    /// 用來 存取 T型別 AD資料 的介面
    /// </summary>
    /// <typeparam name="T">Mdoel 型別</typeparam>
    public interface IAdSet<T> where T : class
    {
        /// <summary>
        /// 尋找 所有 T型別 AD資料
        /// </summary>
        /// <returns></returns>
        List<T> FindAll();

        /// <summary>
        /// 尋找 多筆 T型別 AD資料
        /// </summary>                          
        /// <param name="propName">屬性/攔位 名稱</param>    
        /// <param name="propValue">屬性/攔位 值</param>
        /// <returns></returns>
        List<T> FindAll(string propName, object propValue);

        /// <summary>
        /// 尋找 一筆 T型別 AD資料
        /// </summary>                          
        /// <param name="userName">帳號</param>
        /// <returns></returns>
        T FindOne(string userName);
        /// <summary>
        /// 尋找 一筆 T型別 AD資料
        /// </summary>                          
        /// <param name="propName">屬性/攔位 名稱</param>    
        /// <param name="propValue">屬性/攔位 值</param>
        /// <returns></returns>
        T FindOne(string propName, object propValue);
    }
}
