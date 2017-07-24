using System.Web.Mvc;

namespace NFine.Web.Areas.StatisticsManage
{
    public class StatisticsManageAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "StatisticsManage";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "StatisticsManage_default",
                "StatisticsManage/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
