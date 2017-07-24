using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HT.Common;
using NFine.Application.SystemManage;
using NFine.Application.TaskManage;
using NFine.Code;
using NFine.Web.Areas.StatisticsManage.Models;

namespace NFine.Web.Areas.StatisticsManage.Controllers
{
    public class StatisticsController : ControllerBase
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
            string organizeId = Common.GetString("organizeid");//组织id
            string startTime = Common.GetString("startdt");// 开始时间
            string endTime = Common.GetString("enddt");// 结束时间
            string schedule = Common.GetString("schedule");//进度
            //TODO 找出该组织下的员工
            UserApp userApp = new UserApp();
            var userData = userApp.GetList(pagination, keyword, organizeId);
            //ViewBag.UserList = userData.Select(x => x.F_RealName).ToJson();

            //todo 找出每个员工的完成情况 放入内存中 执行逻辑操作
            TaskApp taskApp = new TaskApp();
            var taskData = taskApp.GetList(userData.Select(x => x.F_Id).ToList());
            List<Series> list = new List<Series>();
            foreach (var item in userData)
            {
                Series series = new Series();
                series.id = item.F_Id;
                series.name = item.F_RealName;
                series.tel = item.F_MobilePhone;
                var tempTask = taskData.Where(d => d.F_UserId == item.F_Id);
                if (!string.IsNullOrEmpty(schedule))
                    tempTask = tempTask.Where(d => d.F_Status == schedule);
                if (!string.IsNullOrEmpty(startTime))
                    tempTask = tempTask.Where(d => d.F_CreatorTime > Utils.ObjectToDateTime(startTime));
                if (!string.IsNullOrEmpty(endTime))
                    tempTask = tempTask.Where(d => d.F_CreatorTime <= Utils.ObjectToDateTime(endTime));
                series.count = tempTask.Count();
                series.schedule = schedule;
                list.Add(series);
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

        //public override ActionResult Index()
        //{
        //    string organizeId = Common.GetString("OrganizeId");
        //    string startDT = Common.GetString("StartDT");// Request["StartDT"];
        //    string endDT = Common.GetString("EndDT");// Request["EndDT"];
        //    //TODO 找出该组织下的员工
        //    UserApp userApp = new UserApp();
        //    organizeId = string.IsNullOrEmpty(organizeId) ? NFine.Code.OperatorProvider.Provider.GetCurrent().CompanyId : organizeId;//获取到当前的组织结构id
        //    List<string> listOrganizeId = new List<string>() { organizeId };
        //    var userData = userApp.GetList(listOrganizeId);
        //    ViewBag.UserList = userData.Select(x => x.F_RealName).ToJson();

        //    //todo 找出每个员工的完成情况 放入内存中 执行逻辑操作
        //    TaskApp taskApp = new TaskApp();
        //    var taskData = taskApp.GetList(userData.Select(x => x.F_Id).ToList());
        //    //todo 执行逻辑操作
        //    List<Series> list=new List<Series>();
        //    ItemsDetailApp itemsDetailApp=new ItemsDetailApp();
        //    var itemsDetail= itemsDetailApp.GetItemList("ScheduleState");
        //    foreach (var item in itemsDetail)
        //    {
        //       Series series=new Series();
        //        series.name = item.F_ItemName;
        //        List<int> listCount=new List<int>();
        //        foreach (var userItem in userData)
        //        {
        //            listCount.Add(taskData.Count(d => d.F_Status == item.F_Id && d.F_UserId == userItem.F_Id));
        //        }
        //        series.data = listCount.ToJson();
        //        list.Add(series);
        //    }
        //    ViewBag.Series = list.ToJson();
        //    //todo 序列化为数组串 
        //    return View();
        //}
    }
}
