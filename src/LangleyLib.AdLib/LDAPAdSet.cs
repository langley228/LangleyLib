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
    /// 用來 AD 資料 物件化的 T 型別 (LDAP 專用)
    /// </summary>
    /// <typeparam name="T">Mdoel 型別</typeparam>
    public class LDAPAdSet<T> : AdSetBase<T>, IAdSet<T> where T : class
    {
        private string m_ClassFilter = "(objectClass=*)";

        /// <summary>
        /// 建構子 : 用來 AD 資料 物件化的 T 型別 (LDAP 專用)
        /// </summary>                        
        /// <param name="adService">用來存取的 AdService 介面</param>
        /// <param name="classFilters">                                   
        /// 過濾 LDAP   的  classFilter 
        ///     不過濾(預設) : "(objectClass=*)" , 只找 AD User : "(objectClass=user)(objectCategory=person)"
        /// </param>
        public LDAPAdSet(IAdService adService, params string[] classFilters)
        {
            m_AdService = adService;
            if (classFilters != null & classFilters.Length > 0)
                m_ClassFilter = string.Join("", classFilters);
            else
                m_ClassFilter = "(objectClass=*)";
        }
        #region 實作介面 public Method
        #region FindAll

        /// <summary>
        /// 尋找 所有 T型別 AD資料
        /// </summary>
        /// <returns></returns>
        public override List<T> FindAll()
        {
            return FindAllByFilter(string.Format("(&{0})", m_ClassFilter));
        }
        /// <summary>
        /// 
        /// 尋找 多筆 T型別 AD資料
        /// </summary>                          
        /// <param name="propName">屬性/攔位 名稱</param>    
        /// <param name="propValue">屬性/攔位 值</param>
        /// <returns></returns>
        public override List<T> FindAll(string propName, object propValue)
        {
            string adPropName = GetAdPropName(propName);
            return FindAllByFilter(string.Format("(&{0}({1}={2}))", m_ClassFilter, adPropName, propValue));
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
            return FindOne("UserName", userName);
        }
        /// <summary>
        /// 尋找 一筆 T型別 AD資料
        /// </summary>                          
        /// <param name="propName">屬性/攔位 名稱</param>    
        /// <param name="propValue">屬性/攔位 值</param>
        /// <returns></returns>
        public override T FindOne(string propName, object propValue)
        {
            string adPropName = GetAdPropName(propName);
            return FindOneByFilter(string.Format("(&{0}({1}={2}))", m_ClassFilter, adPropName, propValue));
        }
        #endregion

        #endregion

        #region private Method
        /// <summary>
        /// 依 搜尋篩選條件字串 尋找 多筆 AD 帳號資訊
        /// </summary>
        /// <param name="setFilter">LDAP 格式的搜尋篩選條件字串，例如，"(objectClass=user)".。預設為「(objectClass=*)」，表示擷取所有物件。</param>
        /// <returns></returns>
        private List<T> FindAllByFilter(string setFilter)
        {
            List<T> list = new List<T>();
            using (DirectorySearcher search = CreateSearch(setFilter))
            {
                if (!string.IsNullOrEmpty(setFilter))
                    search.Filter = setFilter;
                SetSearchProps(search);
                foreach (SearchResult item in search.FindAll())
                {
                    T obj = GetFindOneData(item);
                    list.Add(obj);
                }
            }
            return list;
        }
        /// <summary>
        /// 依 搜尋篩選條件字串 尋找 一筆 AD 帳號
        /// </summary>                          
        /// <param name="setFilter">LDAP 格式的搜尋篩選條件字串，例如，"(objectClass=user)".。預設為「(objectClass=*)」，表示擷取所有物件。</param>
        /// <returns></returns>
        private T FindOneByFilter(string setFilter)
        {
            T obj;
            using (DirectorySearcher search = CreateSearch(setFilter))
            {
                search.Filter = setFilter;
                SetSearchProps(search);
                obj = GetFindOneData(search.FindOne());
            }
            return obj;
        }

        private void SetSearchProps(DirectorySearcher AdSearch)
        {
            //prop 不分大小寫全部轉全形
            foreach (var prop in typeof(T).GetProperties())
            {
                string adPropName = GetAdPropName(prop);
                AdSearch.PropertiesToLoad.Add(adPropName.ToUpper());
            }
        }

        private DirectorySearcher CreateSearch(string filter)
        {
            DirectorySearcher AdSearch = new DirectorySearcher(m_AdService.Root, filter);
            return AdSearch;
        }

        private T GetFindOneData(SearchResult item)
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
                    {
                        if (item.Properties[adPropName.ToUpper()] != null && item.Properties[adPropName.ToUpper()].Count > 0)
                            prop.SetValue(obj, item.Properties[adPropName.ToUpper()][0], null);
                    }
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
                if (attx.AdType == AdType.LDAP)
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
