using System.Web;
using System.Web.Mvc;

namespace NetVulkanoPruebasAutomatizadas_Back
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
