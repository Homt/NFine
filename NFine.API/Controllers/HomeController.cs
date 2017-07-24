using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NFine.API.Controllers
{
    public class HomeController : Controller
    {
        //系统入口
        public ActionResult Index()
        {
            return Redirect("swagger");
        }
    }
}
