using System;
using System.Web;

namespace APaers.DataGen.WebApi.Helpers
{
    public static class CookieHelper
    {
        public static void SetCookie(string key, string value, TimeSpan expires)
        {
            HttpCookie cookie = new HttpCookie(key, value);

            if (HttpContext.Current.Request.Cookies[key] != null)
            {
                HttpCookie cookieOld = HttpContext.Current.Request.Cookies[key];
                cookieOld.Expires = DateTime.Now.Add(expires);
                cookieOld.Value = cookie.Value;
                HttpContext.Current.Response.Cookies.Add(cookieOld);
            }
            else
            {
                cookie.Expires = DateTime.Now.Add(expires);
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
        }

        public static string GetCookie(string key)
        {
            string value = string.Empty;
            HttpCookie cookie = HttpContext.Current.Request.Cookies[key];
            if (cookie != null)
                value = cookie.Value;
            return value;
        }
    }
}