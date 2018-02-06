using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Hosting;

namespace APaers.DataGen.WebApi.Helpers
{
    public static class ThemeHelper
    {
        public const string BundleBase = "~/Content/css/";

        static ThemeHelper()
        {
            string themesFolder = Path.Combine(HostingEnvironment.ApplicationPhysicalPath, "Content", "Themes");
            Themes = new List<string>(Directory.GetDirectories(themesFolder).Select(dir => Path.GetFileName(dir)).Where(dir => dir != "fonts"));
        }

        public static List<string> Themes { get; }

        private static string Default { get; } = "Default";
        private static string CookieName { get; } = "CssTheme";

        public static string CurrentTheme
        {
            get
            {
                string themeName = CookieHelper.GetCookie(CookieName);
                return string.IsNullOrWhiteSpace(themeName) ? Default : themeName;
            }
            set
            {
                if (Themes.Contains(value))
                    CookieHelper.SetCookie(CookieName, value, TimeSpan.FromDays(1000));
            }
        }

        public static string Bundle(string themeName)
        {
            return BundleBase + themeName;
        }
    }
}