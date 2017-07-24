using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NFine.Application;
using NFine.Application.TaskManage;
using NFine.Code;
using NFine.Domain._03_Entity.CustomerManage;
using NFine.Domain._03_Entity.TaskManage;

namespace NFine.Web.Areas.CustomerManage.Controllers
{
    public class CustomerController : ControllerBase
    {
        CustomerApp customerApp = new CustomerApp();
        //
        // GET: /CustomerManage/Customer/
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridJson(Pagination pagination, string keyword)
        {
            int area = Common.GetInt("area", 0);// Convert.ToInt32(Request["area"]);
            var data = new
            {
                rows = customerApp.GetList(pagination, keyword, area),
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
        public ActionResult SubmitForm(CustomerEntity customerEntity, string keyValue)
        {
            customerApp.SubmitForm(customerEntity, keyValue);
            return Success("操作成功。");
        }

        /// <summary>
        /// 分配任务  生成任务
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitTaskForm(CustomerEntity customerEntity, string keyValue, string userId, string taskId)
        {
            string defaultStatus = "7a6d1bc4-3ec7-4c57-be9b-b4c97d60d5f6";//默认状态
            TaskApp taskApp = new TaskApp();
            TaskEntity taskEntity = new TaskEntity();
            taskEntity.F_UserId = userId;
            taskEntity.F_CustomerId = keyValue;
            taskEntity.F_Status = defaultStatus;
            taskEntity.F_Id = taskId;
            keyValue = taskId;
            taskApp.SubmitForm(taskEntity, keyValue);
            return Success("操作成功。");
        }
        /// <summary>
        /// 修改任务状态
        /// </summary>
        /// <param name="customerEntity"></param>
        /// <param name="keyValue"></param>
        /// <param name="statusId"></param>
        /// <param name="taskId"></param>
        /// <returns></returns>
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitTaskStatusForm(CustomerEntity customerEntity, string statusId, string taskId)
        {
            //string defaultStatus = "7a6d1bc4-3ec7-4c57-be9b-b4c97d60d5f6";//默认状态
            TaskApp taskApp = new TaskApp();
            TaskEntity taskEntity = new TaskEntity();
            taskEntity.F_Status = statusId;
            taskApp.SubmitForm(taskEntity, taskId);
            return Success("操作成功。");
        }
        [HttpPost]
        [HandlerAuthorize]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteForm(string keyValue)
        {
            customerApp.DeleteForm(keyValue);
            return Success("删除成功。");
        }
        [HttpGet]
        [HandlerAuthorize]
        public ActionResult Task()
        {
            return View();
        }
        /// <summary>
        /// 客户状态处理
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize]
        public ActionResult Dispose()
        {
            return View();
        }
        /// <summary>
        ///客户分布图
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize]
        public ActionResult Distribution(string keyword, string bank)
        {
            ViewBag.CustomerData = customerApp.GetList(keyword, bank).ToJson();
            return View();
        }
    }
}
