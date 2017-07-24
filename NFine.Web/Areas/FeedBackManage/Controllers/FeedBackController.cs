using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NFine.Application.FeedBackManage;
using NFine.Application.TaskManage;
using NFine.Code;
using NFine.Domain._03_Entity.CustomerManage;
using NFine.Domain._03_Entity.TaskManage;

namespace NFine.Web.Areas.FeedBackManage.Controllers
{
    public class FeedBackController : ControllerBase
    {
        FeedBackApp feedBackApp = new FeedBackApp();
        //
        // GET: /CustomerManage/Customer/
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridJson(Pagination pagination, string keyword)
        {
            var data = new
            {
                rows = feedBackApp.GetList(pagination),
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
            var data = feedBackApp.GetForm(keyValue);
            return Content(data.ToJson());
        }
        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitForm(FeedBackEntity feedBackEntity, string keyValue)
        {
            feedBackApp.SubmitForm(feedBackEntity, keyValue);
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
            feedBackApp.DeleteForm(keyValue);
            return Success("删除成功。");
        }
    }
}
