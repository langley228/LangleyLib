using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LyExtLib.Extensions
{
    /// <summary>
    /// class 相關功能 的 Extensions 
    /// </summary>
    public static class ClassExtensions
    {
        /// <summary>取得所有屬性名稱</summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string[] GetPropNames<TModel>(this TModel source) where TModel : class
        {
            Type type = source.GetType();
            return type.GetProperties().Select(m => m.Name).ToArray();
        }
        /// <summary>
        /// 屬性是否存在
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="source"></param>
        /// <param name="name"></param>
        /// <param name="comparisonType"></param>
        /// <returns></returns>
        public static bool PropExists<TModel>(this TModel source, string name, StringComparison comparisonType = StringComparison.OrdinalIgnoreCase) where TModel : class
        {
            Type type = source.GetType();
            switch (comparisonType)
            {
                case StringComparison.CurrentCulture:
                    break;
                case StringComparison.CurrentCultureIgnoreCase:
                    break;
                case StringComparison.InvariantCulture:
                    break;
                case StringComparison.InvariantCultureIgnoreCase:
                    break;
                case StringComparison.Ordinal:
                    break;
                case StringComparison.OrdinalIgnoreCase:
                    break;
                default:
                    break;
            }
            return type.GetProperties().Any(m => m.Name.IndexOf(name, comparisonType) > -1);
        }

        /// <summary>取得屬性值</summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static object[] GetPropValues<TModel>(this TModel source) where TModel : class
        {
            Type type = source.GetType();
            return type.GetProperties().Select(m => m.GetValue(source)).ToArray();
        }
        /// <summary>取得屬性值</summary>
        /// <param name="source"></param>
        /// <param name="name">屬性名稱</param>
        /// <returns></returns>
        public static object GetPropValue<TModel>(this TModel source, string name) where TModel : class
        {
            Type type = source.GetType();
            PropertyInfo propertyInfo = type.GetProperty(name);
            if (propertyInfo != null)
                return type.GetProperty(name).GetValue(source);
            else
                return null;
        }


        /// <summary>設定屬性值</summary>
        /// <param name="source"></param>
        /// <param name="name">屬性名稱</param>
        /// <param name="value">屬性值</param>
        /// <returns></returns>
        public static void SetPropValue<TModel>(this TModel source, string name, object value) where TModel : class
        {
            Type type = source.GetType();
            PropertyInfo propertyInfo = type.GetProperty(name);
            if (propertyInfo != null)
                propertyInfo.SetValue(source, value);
        }
        /// <summary>設定屬性值</summary>
        /// <param name="source"></param>
        /// <param name="value">來源</param>
        /// <returns></returns>
        public static void SetPropValues<TModel>(this TModel source, TModel value) where TModel : class
        {
            string[] propName = source.GetPropNames();
            foreach (var name in propName)
            {
                source.SetPropValue(name, value.GetPropValue(name));
            }
        }

    }
}
