using System.Web.Mvc;

namespace NFine.Web.Areas.MessageManage
{
    public class MessageManageAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "MessageManage";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "MessageManage_default",
                "MessageManage/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
