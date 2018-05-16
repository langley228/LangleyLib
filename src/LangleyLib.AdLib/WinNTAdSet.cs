using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LangleyLib.AdLib
{

    /// <summary>
    /// 用來 AD 資料 物件化的 T 型別 (WinNT 專用)
    /// </summary>
    /// <typeparam name="T">Mdoel 型別</typeparam>
    public class WinNTAdSet<T> : AdSetBase<T>, IAdSet<T> where T : class
    {
        private List<string> m_ClassNames = new List<string>();

        /// <summary>
        /// 建構子 : 用來 AD 資料 物件化的 T 型別 (WinNT 專用)
        /// </summary>                        
        /// <param name="adService">用來存取的 AdService 介面</param>
        /// <param name="classNames">
        /// 過濾 WinNT  的  className  
        ///     不過濾(預設) : "" 或 不需傳入 , 只找 AD User : "USER"
        /// </param>
        public WinNTAdSet(IAdService adService, params string[] classNames)
        {
            m_AdService = adService;
            foreach (var item in classNames)
            {
                if (!string.IsNullOrEmpty(item))
                    m_ClassNames.Add(item.ToUpper());
            }
        }
        #region 實作介面 public Method

        #region FindAll
        /// <summary>
        /// 尋找 所有 T型別 AD資料
        /// </summary>
        /// <returns></returns>
        public override List<T> FindAll()
        {
            return FindAllByProp(null, null, false);
        }

        /// <summary>
        /// 尋找 多筆 T型別 AD資料
        /// </summary>                          
        /// <param name="propName">屬性/攔位 名稱</param>    
        /// <param name="propValue">屬性/攔位 值</param>
        /// <returns></returns>
        public override List<T> FindAll(string propName, object propValue)
        {
            return FindAllByProp(propName, propValue, false);
        }
        #endregion

        #region FindOne
        /// <summary>
        /// 尋找 一筆 T型別 AD資料
        /// </summary>                          
        /// <param name="userName">帳號</param>
        /// <returns></returns>
        public override T FindOne(string userName)
        {
            return FindOne("Name", userName);
        }

        /// <summary>
        /// 尋找 一筆 T型別 AD資料
        /// </summary>                          
        /// <param name="propName">屬性/攔位 名稱</param>    
        /// <param name="propValue">屬性/攔位 值</param>
        /// <returns></returns>
        public override T FindOne(string propName, object propValue)
        {
            List<T> list = FindAllByProp(propName, propValue, true);
            if (list != null && list.Count > 0)
                return list[0];
            else
                return default(T);
        }
        #endregion

        #endregion

        #region private Method
        /// <summary>
        /// 尋找 AD 帳號資訊
        /// </summary>                          
        /// <param name="propName">屬性/攔位 名稱</param>    
        /// <param name="propValue">屬性/攔位 值</param>  
        /// <param name="bFindOne">是否只找一筆</param>
        /// <returns></returns>
        private List<T> FindAllByProp(string propName, object propValue, bool bFindOne)
        {
            List<T> list = new List<T>();
            foreach (DirectoryEntry objChildDE in m_AdService.Root.Children)
            {
                // 在這裡判斷子項目是否為使用者帳號的SchemaClassName
                if (m_ClassNames == null || m_ClassNames.Count == 0 || m_ClassNames.Contains(objChildDE.SchemaClassName.ToUpper()))
                {
                    string adPropName = GetAdPropName(propName);
                    if (string.IsNullOrEmpty(adPropName) || (objChildDE.Properties.Contains(adPropName.ToUpper()) &&
                                                           objChildDE.Properties[adPropName].Value.Equals(propValue)))
                    {
                        T obj = GetFindOneData(objChildDE);
                        list.Add(obj);
                        if (bFindOne)
                            break;
                    }
                }
            }
            return list;
        }

        private T GetFindOneData(DirectoryEntry item)
        {
            T obj = default(T);
            if (item != null)
            {
                obj = Activator.CreateInstance<T>();
                //prop 不分大小寫全部轉全形
                foreach (var prop in typeof(T).GetProperties())
                {
                    string adPropName = GetAdPropName(prop);
                    if (item.Properties.Contains(adPropName.ToUpper()))
                        prop.SetValue(obj, item.Properties[adPropName.ToUpper()].Value, null);
                }
            }
            return obj;
        }
        private string GetAdPropName(PropertyInfo prop)
        {
            var atts = prop.GetCustomAttributes(typeof(PropertyAttribute), true);
            string propName = prop.Name;
            foreach (PropertyAttribute attx in atts)
            {
                if (attx.AdType == AdType.WinNT)
                {
                    propName = attx.PropName;
                    break;
                }
            }
            return propName;
        }
        private string GetAdPropName(string propName)
        {
            string adPropName = propName;
            PropertyInfo prop = null;
            if (!string.IsNullOrEmpty(propName))
                prop = typeof(T).GetProperty(propName);
            if (prop != null)
                adPropName = GetAdPropName(prop);
            return adPropName;
        }
        #endregion

    }
}
