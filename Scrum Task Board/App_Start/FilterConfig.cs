using System.Web;
using System.Web.Mvc;

namespace Scrum_Task_Board
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
