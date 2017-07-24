using System.Web.Mvc;

namespace NFine.Web.Areas.FeedBackManage
{
    public class FeedBackManageAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "FeedBackManage";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "FeedBackManage_default",
                "FeedBackManage/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
