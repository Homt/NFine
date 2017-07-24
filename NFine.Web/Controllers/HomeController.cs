/*******************************************************************************
 * Copyright © 2016 NFine.Framework 版权所有
 * Author: NFine
 * Description: 锡润管理平台
 * Website：http://www.softdog.cc
*********************************************************************************/

using System;
using NFine.Application.SystemManage;
using NFine.Code;
using NFine.Domain.Entity.SystemManage;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using NFine.Application.TaskManage;

namespace NFine.Web.Controllers
{
    [HandlerLogin]
    public class HomeController : Controller
    {
        private const string Doing = "7a6d1bc4-3ec7-4c57-be9b-b4c97d60d5f6";//进行中 id
        private const string Done = "ce340c73-5048-4940-b86e-e3b3d53fdb2c";//已完成 id
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Default()
        {
            var organizeId = NFine.Code.OperatorProvider.Provider.GetCurrent().CompanyId;//获取到当前的组织结构id
            UserApp userApp = new UserApp();
            TaskApp taskApp = new TaskApp();
            OrganizeApp organizeApp = new OrganizeApp();
            var organizeData = organizeApp.GetAllList(organizeId);
            List<string> listOrganizeNext = new List<string>();
            foreach (var item in organizeData)
            {
                listOrganizeNext.Add(item);
            }
            var userData = userApp.GetList(listOrganizeNext);
            var taskData = taskApp.GetList(userData.Select(x => x.F_Id).ToList());
            DateTime dt = DateTime.Now;  //当前时间
            //当天待处理任务
            ViewBag.DayTaskDoing = taskData.Count(d => d.F_Status == Doing && d.F_CreatorTime > dt.AddDays(-1) && d.F_CreatorTime <= dt);
            //本周待处理任务
            DateTime startWeek = dt.AddDays(1 - Convert.ToInt32(dt.DayOfWeek.ToString("d")));  //本周周一
            DateTime endWeek = startWeek.AddDays(6);  //本周周日
            ViewBag.WeekTaskDoing = taskData.Count(d => d.F_Status == Doing && d.F_CreatorTime > startWeek && d.F_CreatorTime <= endWeek);
            //本周已处理任务
            ViewBag.WeekTaskDone = taskData.Count(d => d.F_Status == Done && d.F_CreatorTime > startWeek && d.F_CreatorTime <= endWeek);
            //本月待处理任务
            DateTime startMonth = dt.AddDays(1 - dt.Day);  //本月月初
            DateTime endMonth = startMonth.AddMonths(1).AddDays(-1);  //本月月末
            //本月任务
            var tempTask= taskData.Where(d => d.F_CreatorTime > startMonth && d.F_CreatorTime <= endMonth).ToList();
            //本月待处理
            ViewBag.MonthTaskDoing = tempTask.Count(d => d.F_Status == Doing);
           
            //本月已处理任务
            ViewBag.MonthTaskDone = tempTask.Count(d => d.F_Status == Done);
            //本月未完成
            ViewBag.MonthTaskNone = tempTask.Count(d => d.F_Status != Done && d.F_Status != Doing);
            //最近6个月的完成情况
            List<int> listLineChartData = new List<int>();
            List<string> listLineChartName = new List<string>();
            for (int i = 6; i > 0; i--)
            {
                startMonth = dt.AddMonths(-i).AddDays(1 - dt.Day);  //当月月初
                endMonth = startMonth.AddMonths(1).AddDays(-1);  //当月月末
                var tempCount = taskData.Count(d => d.F_Status == Done && d.F_CreatorTime > startMonth && d.F_CreatorTime <= endMonth);
                listLineChartData.Add(tempCount);
                listLineChartName.Add(dt.AddMonths(-i).Year + "年" + dt.AddMonths(-i).Month + "月");
            }
            ViewBag.LineChartData = listLineChartData.ToJson();
            ViewBag.LineChartName = listLineChartName.ToJson();
            return View();
        }
        [HttpGet]
        public ActionResult About()
        {
            return View();
        }
    }
}
