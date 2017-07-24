using System.Web.Mvc;

namespace NFine.Web.Areas.VisitManage
{
    public class VisitManageAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "VisitManage";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "VisitManage_default",
                "VisitManage/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
