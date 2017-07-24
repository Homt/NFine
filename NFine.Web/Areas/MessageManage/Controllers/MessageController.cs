using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NFine.Application.MessageManage;
using NFine.Application.SystemManage;
using NFine.Application.TaskManage;
using NFine.Code;
using NFine.Domain._03_Entity.CustomerManage;
using NFine.Domain._03_Entity.TaskManage;

namespace NFine.Web.Areas.MessageManage.Controllers
{
    public class MessageController : ControllerBase
    {
        MessageApp customerApp = new MessageApp();
        //
        // GET: /CustomerManage/Customer/
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridJson(Pagination pagination, string keyword)
        {
            var expression = ExtLinq.True<MessageEntity>();
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(d => d.F_Title.Contains(keyword));
                expression = expression.Or(d => d.F_Content.Contains(keyword));
            }
            var data = new
            {
                rows = customerApp.GetList(pagination, expression),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = customerApp.GetForm(keyValue);
            return Content(data.ToJson());
        }
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitForm(MessageEntity customerEntity, string keyValue)
        {
            customerApp.SubmitForm(customerEntity, keyValue);
            return Success("操作成功。");
        }
    }
}
