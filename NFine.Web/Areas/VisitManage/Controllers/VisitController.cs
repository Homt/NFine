using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NFine.Application.TaskManage;
using NFine.Application.VisitManage;
using NFine.Code;
using NFine.Domain._03_Entity.CustomerManage;
using NFine.Domain._03_Entity.TaskManage;
using NFine.Domain._03_Entity.VisitManage;
using NFine.Application;

namespace NFine.Web.Areas.VisitManage.Controllers
{
    public class VisitController : ControllerBase
    {
        VisitApp VisitApp = new VisitApp();
        //
        // GET: /CustomerManage/Customer/
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridJson(Pagination pagination, string keyword)
        {
            var data = new
            {
                rows = VisitApp.GetList(pagination, keyword),
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
            var model = VisitApp.GetList(keyValue).OrderByDescending(d => d.F_CreatorTime).FirstOrDefault();
            var data = VisitApp.GetForm(model == null ? string.Empty : model.F_Id);
            return Content(data.ToJson());
        }
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitForm(VisitEntity visitEntity, string keyValue)
        {
            VisitApp.SubmitForm(visitEntity, keyValue);
            return Success("操作成功。");
        }
        [HttpPost]
        [HandlerAuthorize]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteForm(string keyValue)
        {
            VisitApp.DeleteForm(keyValue);
            return Success("删除成功。");
        }
        [HttpGet]
        [HandlerAuthorize]
        public override ActionResult Details()
        {
            string keyValue = Common.GetString("keyValue");
            TaskApp taskApp = new TaskApp();
            var taskModel = taskApp.GetForm(keyValue);
            CustomerApp customerApp = new CustomerApp();
            var customerModel = customerApp.GetForm(taskModel.F_CustomerId);
            switch (customerModel.F_Bank)
            {
                case "d69cb819-2bb3-4e1d-9917-33b9a439233d"://中国光大银行
                    return RedirectToAction("CEBDetails", new { TaskId = keyValue });
                    break;
                case "4c2f2428-2e00-4336-b9ce-5a61f24193f6"://郑州银行
                    return RedirectToAction("ZZBANKDetails", new { TaskId = keyValue });
                    break;
                case "48c4e0f5-f570-4601-8946-6078762db3bf"://广发银行
                    return RedirectToAction("CGBDetails", new { TaskId = keyValue });
                    break;
                case "0096ad81-4317-486e-9144-a6a02999ff19"://中国邮政储蓄
                    return RedirectToAction("PSBCDetails", new { TaskId = keyValue });
                    break;
                case "ace2d5e8-56d4-4c8b-8409-34bc272df404"://中国民生银行
                    return RedirectToAction("CMBCDetails", new { TaskId = keyValue });
                    break;
                default:
                    break;
            }
            return View();
        }
        [HttpGet]
        [HandlerAuthorize]
        public override ActionResult Form()
        {
            string keyValue = Common.GetString("keyValue");
            TaskApp taskApp = new TaskApp();
            var taskModel = taskApp.GetForm(keyValue);
            CustomerApp customerApp = new CustomerApp();
            var customerModel = customerApp.GetForm(taskModel.F_CustomerId);
            switch (customerModel.F_Bank)
            {
                case "d69cb819-2bb3-4e1d-9917-33b9a439233d"://中国光大银行
                    return RedirectToAction("CEBForm", new { TaskId = keyValue });
                    break;
                case "4c2f2428-2e00-4336-b9ce-5a61f24193f6"://郑州银行
                    return RedirectToAction("ZZBANKForm", new { TaskId = keyValue });
                    break;
                case "48c4e0f5-f570-4601-8946-6078762db3bf"://广发银行
                    return RedirectToAction("CGBForm", new { TaskId = keyValue });
                    break;
                case "0096ad81-4317-486e-9144-a6a02999ff19"://中国邮政储蓄
                    return RedirectToAction("PSBCForm", new { TaskId = keyValue });
                    break;
                case "ace2d5e8-56d4-4c8b-8409-34bc272df404"://中国民生银行
                    return RedirectToAction("CMBCForm", new { TaskId = keyValue });
                    break;
                default:
                    break;
            }
            return View();
        }
        //郑州银行
        public ActionResult ZZBANKForm()
        {
            ViewBag.TaskId = Common.GetString("TaskId");
            return View();
        }
        public ActionResult ZZBANKDetails()
        {
            ViewBag.TaskId = Common.GetString("TaskId");
            return View();
        }
        public ActionResult ZZBANKPrint(string id)
        {
            var model = VisitApp.GetList(id).OrderByDescending(d => d.F_CreatorTime).FirstOrDefault();
            return View(model);
        }
        ///中国邮政储蓄
        public ActionResult PSBCForm()
        {
            ViewBag.TaskId = Common.GetString("TaskId");
            return View();
        }
        public ActionResult PSBCDetails()
        {
            ViewBag.TaskId = Common.GetString("TaskId");
            return View();
        }

        public ActionResult PSBCPrint(string id)
        {
            var model = VisitApp.GetList(id).OrderByDescending(d => d.F_CreatorTime).FirstOrDefault();
            return View(model);
        }
        //中国民生银行
        public ActionResult CMBCForm()
        {
            ViewBag.TaskId = Common.GetString("TaskId");
            return View();
        }
        public ActionResult CMBCDetails()
        {
            ViewBag.TaskId = Common.GetString("TaskId");
            return View();
        }

        public ActionResult CMBCPrint(string id)
        {
            var model = VisitApp.GetList(id).OrderByDescending(d => d.F_CreatorTime).FirstOrDefault();
            return View(model);
        }
        //中国光大银行
        public ActionResult CEBForm()
        {
            ViewBag.TaskId = Common.GetString("TaskId");
            return View();
        }
        public ActionResult CEBDetails()
        {
            ViewBag.TaskId = Common.GetString("TaskId");
            return View();
        }
        public ActionResult CEBCPrint(string id)
        {
            var model = VisitApp.GetList(id).OrderByDescending(d => d.F_CreatorTime).FirstOrDefault();
            return View(model);
        }
        //中国广发银行
        public ActionResult CGBForm()
        {
            ViewBag.TaskId = Common.GetString("TaskId");
            return View();
        }
        public ActionResult CGBDetails()
        {
            ViewBag.TaskId = Common.GetString("TaskId");
            return View();
        }

        public ActionResult CGBPrint(string id)
        {
            var model = VisitApp.GetList(id).OrderByDescending(d => d.F_CreatorTime).FirstOrDefault();
            return View(model);
        }
    }
}
