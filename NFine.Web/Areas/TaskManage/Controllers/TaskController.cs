using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NFine.Application;
using NFine.Application.SystemManage;
using NFine.Application.TaskManage;
using NFine.Application.VisitManage;
using NFine.Code;

namespace NFine.Web.Areas.TaskManage.Controllers
{
    public class TaskController : ControllerBase
    {
        TaskApp taskApp = new TaskApp();
        //
        // GET: /TaskManage/Task/

        //
        // GET: /CustomerManage/Customer/
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridJson(Pagination pagination, string keyword)
        {
            string schedule = Common.GetString("schedule");
            string startTime = Common.GetString("startdt");
            string endTime = Common.GetString("enddt");
            string user = Common.GetString("user");
            string bank = Common.GetString("bank");
            CustomerApp customerApp = new CustomerApp();
            List<dynamic> list = new List<dynamic>();
            var result = taskApp.GetList(pagination, keyword, schedule, startTime, endTime,user,bank);
            foreach (var item in result)
            {
                var entity = customerApp.GetForm(item.F_CustomerId);
                var temp = new
                {
                    F_Customer_Name = entity.F_Name,
                    F_Customer_Phone = entity.F_Tel,
                    F_UserId = item.F_UserId,
                    F_CreatorUserId = item.F_CreatorUserId,
                    F_CreatorTime = item.F_CreatorTime,
                    F_Status = item.F_Status,
                    F_Id = item.F_Id,
                    F_Customer_Id = entity.F_Id
                };
                list.Add(temp);
            }
            var data = new
            {
                rows = list,
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
            string taskId = Common.GetString("keyValue");
            TaskApp taskApp = new TaskApp();
            var taskData = taskApp.GetForm(taskId);
            CustomerApp customerApp = new CustomerApp();
            var customerData = customerApp.GetForm(taskData.F_CustomerId);
            UserApp userApp = new UserApp();
            var userData = userApp.GetForm(taskData.F_UserId);
            var data = new
            {
                F_Shop_Name = customerData.F_Shop_Name,
                F_Shop_Address=customerData.F_Shop_Address,
               F_Name=customerData.F_Name,
               F_Tel=customerData.F_Tel,
               F_User_Name=userData.F_RealName,
               F_Task_Time=taskData.F_CreatorTime,
               F_User_Tel=userData.F_MobilePhone,
               F_Status=GetName(taskData.F_Status)
            };
            return Content(data.ToJson());
        }
    }
}
