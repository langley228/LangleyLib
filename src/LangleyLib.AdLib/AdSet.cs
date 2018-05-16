using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangleyLib.AdLib
{

    /// <summary>
    /// 用來 存取 T型別 AD資料 的抽象類別
    /// </summary>
    /// <typeparam name="T">Mdoel 型別</typeparam>
    public abstract class AdSetBase<T> : IAdSet<T> where T : class
    {
        /// <summary>
        /// 用來存取的 AdService 介面
        /// </summary>
        protected IAdService m_AdService;
        /// <summary>
        /// 尋找 所有 T型別 AD資料
        /// </summary>
        /// <returns></returns>
        public abstract List<T> FindAll();

        /// <summary>
        /// 尋找 多筆 T型別 AD資料
        /// </summary>                          
        /// <param name="propName">屬性/攔位 名稱</param>    
        /// <param name="propValue">屬性/攔位 值</param>
        /// <returns></returns>
        public abstract List<T> FindAll(string propName, object propValue);

        /// <summary>
        /// 尋找 一筆 T型別 AD資料
        /// </summary>                          
        /// <param name="userName">帳號</param>
        /// <returns></returns>
        public abstract T FindOne(string userName);
        /// <summary>
        /// 尋找 一筆 T型別 AD資料
        /// </summary>                          
        /// <param name="propName">屬性/攔位 名稱</param>    
        /// <param name="propValue">屬性/攔位 值</param>
        /// <returns></returns>
        public abstract T FindOne(string propName, object propValue);
    }
}
