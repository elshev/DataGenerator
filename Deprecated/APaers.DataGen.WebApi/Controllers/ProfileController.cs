using System;
using System.Web.Mvc;
using APaers.DataGen.WebApi.Helpers;

namespace APaers.DataGen.WebApi.Controllers
{
    public class ProfileController : Controller
    {
        public ActionResult ChangeTheme(string themeName)
        {
            ThemeHelper.CurrentTheme = themeName;
            if (Request.UrlReferrer != null)
            {
                string returnUrl = Request.UrlReferrer.ToString();
                return new RedirectResult(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}