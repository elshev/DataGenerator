using System.Web.Http.Filters;
using APaers.DataGen.Api.Infrastructure;

namespace APaers.DataGen.Api
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(HttpFilterCollection filters)
        {
            filters.Add(new RequireHttpsApiAttribute());
        }
    }
}