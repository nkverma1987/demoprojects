using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace MVCDemoNew.App_Start
{
    public class CacheHelper
    {
        public static object GetItem(string key)
        {
            //key = String.Format("{0}-{1}", key, WebHelper.GetApplicationName());
            //key = String.Format("{0}-{1}", ConfigurationManager.AppSettings[CommonConstant.CachKeyPrefixKey], key);
            return HttpRuntime.Cache.Get(key);
        }

        public static void RemoveItem(string key)
        {
            //key = String.Format("{0}-{1}", key, WebHelper.GetApplicationName());
           // key = String.Format("{0}-{1}", ConfigurationManager.AppSettings[CommonConstant.CachKeyPrefixKey], key);
            HttpRuntime.Cache.Remove(key);
        }

        public static void Clear()
        {
            IDictionaryEnumerator enumerator = HttpRuntime.Cache.GetEnumerator();
            while (enumerator.MoveNext())
            {
                HttpContext.Current.Cache.Remove(enumerator.Key.ToString());
            }
        }

        public static void InsertItem(string key, object value)
        {
            //key =String.Format("{0}-{1}",key, WebHelper.GetApplicationName());
            //key = String.Format("{0}-{1}", ConfigurationManager.AppSettings[CommonConstant.CachKeyPrefixKey], key);
            if (null != GetItem(key))
            {
                HttpRuntime.Cache[key] = value;
            }
            else
            {
                HttpRuntime.Cache.Add(key, value, null, System.Web.Caching.Cache.NoAbsoluteExpiration, System.Web.Caching.Cache.NoSlidingExpiration, System.Web.Caching.CacheItemPriority.Normal, null);
            }
        }
    }

}