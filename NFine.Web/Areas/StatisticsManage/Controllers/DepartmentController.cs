using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HT.Common;
using NFine.Application.SystemManage;
using NFine.Application.TaskManage;
using NFine.Code;
using NFine.Domain.Entity.SystemManage;
using NFine.Web.Areas.StatisticsManage.Models;

namespace NFine.Web.Areas.StatisticsManage.Controllers
{
    public class DepartmentController : ControllerBase
    {
        private const string schedule = "ScheduleState";
        //
        // GET: /StatisticsManage/Department/
        public override ActionResult Index()
        {
            string organizeId = Common.GetString("organizeid");
            string startDT = Common.GetString("startdt");// Request["StartDT"];
            string endDT = Common.GetString("enddt");// Request["EndDT"];
            UserApp userApp = new UserApp();
            organizeId = string.IsNullOrEmpty(organizeId) ? NFine.Code.OperatorProvider.Provider.GetCurrent().CompanyId : organizeId;//获取到当前的组织结构id
            //todo 找出该组织的下级
            OrganizeApp organizeApp = new OrganizeApp();
            var organizeData = organizeApp.GetList(organizeId);
            List<string> listOrganizeId = organizeData.Select(x => x.F_Id).ToList();// new List<string>() { organizeId };
            //var userData = userApp.GetList(listOrganizeId);
            ViewBag.OrganizeList = organizeData.Select(x => x.F_FullName).ToJson();
            //todo 找出每个员工的完成情况 放入内存中 执行逻辑操作
            TaskApp taskApp = new TaskApp();
            //var taskData = taskApp.GetList(userData.Select(x => x.F_Id).ToList());
            List<Report> listReports = new List<Report>();
            ItemsDetailApp itemsDetailApp = new ItemsDetailApp();
            var itemsData = itemsDetailApp.GetItemList(schedule);
            foreach (var item in itemsData)
            {
                Report report = new Report();
                report.name = item.F_ItemName;
                List<int> listCount = new List<int>();
                foreach (var organizeItem in organizeData)
                {
                    //todo 获取组织下的所有员工
                    //获取组织下的组织
                    var data = organizeApp.GetAllList(organizeItem.F_Id);
                    List<string> listOrganizeNext = new List<string>();
                    foreach (var organizeItemNext in data)
                    {
                        listOrganizeNext.Add(organizeItemNext);
                    }
                    //获取下级下的所有员工
                    var userData = userApp.GetList(listOrganizeNext);
                    //获取下级下的所有员工的任务
                    var taskData = taskApp.GetList(userData.Select(x => x.F_Id).ToList());
                    if (!string.IsNullOrEmpty(startDT))
                    {
                        var time = Utils.ObjectToDateTime(startDT);
                        taskData = taskData.Where(d => d.F_CreatorTime > time).ToList();
                    }
                    if (!string.IsNullOrEmpty(endDT))
                    {
                        var time = Utils.ObjectToDateTime(endDT);
                        taskData = taskData.Where(d => d.F_CreatorTime <= time).ToList();
                    }
                    listCount.Add(taskData.Count(d => d.F_Status == item.F_Id));
                }
                report.data = listCount;
                listReports.Add(report);
            }
            ViewBag.Series = listReports.ToJson();
            return View();
        }
    }
}
