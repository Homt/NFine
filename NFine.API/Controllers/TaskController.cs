using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using NFine.Application;
using NFine.Application.MessageManage;
using NFine.Application.SystemManage;
using NFine.Application.TaskManage;
using NFine.API.Models;
using NFine.Code;
using NFine.Domain._03_Entity.CustomerManage;

namespace NFine.API.Controllers
{
    public class TaskController : ApiController
    {
        /// <summary>
        /// 任务列表
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">数量</param>
        /// <param name="bank">银行</param>
        /// <param name="schedule">进度</param>
        /// <param name="keyword">关键字</param>
        /// <param name="startdt">开始时间</param>
        /// <param name="enddt">结束时间</param>
        /// <param name="userid">用户id</param>
        /// <returns></returns>
        public ApiResult<dynamic> PostTaskList(int? pageIndex, int? pageSize, string bank, string schedule, string keyword, string startdt, string enddt, string userid)
        {
            ApiResult<dynamic> api = new ApiResult<dynamic>();
            try
            {
                int pageindex = pageIndex ?? 0;// Common.GetInt("pageIndex", 0);// jsondata.pageIndex ?? 0;
                int pagesize = pageSize ?? 0;// Common.GetInt("pageSize", 0);// jsondata.pageSize ?? 0;
                TaskApp taskApp = new TaskApp();
                Pagination pagination = new Pagination();
                pagination.page = pageindex;
                pagination.rows = pagesize;
                pagination.sidx = "F_CreatorTime";
                pagination.sord = "desc";
                var result = taskApp.GetList(pagination, keyword, schedule, startdt, enddt, userid, bank);
                CustomerApp customerApp = new CustomerApp();
                List<dynamic> list = new List<dynamic>();
                foreach (var item in result)
                {
                    var entity = customerApp.GetForm(item.F_CustomerId);
                    list.Add(entity);
                }
                api.Message = "获取成功";
                api.Status = true;
                api.Result = list;
                return api;
            }
            catch (Exception e)
            {
                api.Message = e.Message;
                return api;
            }
        }
       /// <summary>
       /// 任务详情
       /// </summary>
       /// <param name="id">任务id</param>
       /// <returns></returns>
        public ApiResult<dynamic> Details(string id)
        {
            ApiResult<dynamic> api = new ApiResult<dynamic>();
            try
            {
                TaskApp taskApp = new TaskApp();
                var taskData = taskApp.GetForm(id);
                CustomerApp customerApp = new CustomerApp();
                var customerData = customerApp.GetForm(taskData.F_CustomerId);
                UserApp userApp = new UserApp();
                var userData = userApp.GetForm(taskData.F_UserId);
                var data = new
                {
                    F_Shop_Name = customerData.F_Shop_Name,
                    F_Shop_Address = customerData.F_Shop_Address,
                    F_Name = customerData.F_Name,
                    F_Tel = customerData.F_Tel,
                    F_User_Name = userData.F_RealName,
                    F_Task_Time = taskData.F_CreatorTime,
                    F_User_Tel = userData.F_MobilePhone,
                    F_Status = GetName(taskData.F_Status)
                };
                api.Message = "获取成功";
                api.Status = true;
                api.Result = data;
                return api;
            }
            catch (Exception e)
            {
                api.Message = e.Message;
                return api;
            }
        }
        /// <summary>
        /// 获取数据字典名称
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private string GetName(string id)
        {
            ItemsDetailApp itemsDetailApp = new ItemsDetailApp();
            var data = itemsDetailApp.GetForm(id);
            if (data != null)
                return data.F_ItemName;
            return string.Empty;
        }

       /// <summary>
       /// 提交任务
       /// </summary>
       /// <param name="customerId">客户id</param>
       /// <param name="userId">用户id</param>
       /// <param name="lat">纬度</param>
       /// <param name="lng">经度</param>
       /// <returns></returns>
        public ApiResult<dynamic> PostCustomerData(string customerId,string userId,float lat,float lng)
        {
            ApiResult<dynamic> api = new ApiResult<dynamic>();
            try
            {
                CustomerApp customerApp = new CustomerApp();
                var customerModel = customerApp.GetForm(customerId);
                customerModel.F_LastModifyTime = DateTime.Now;
                customerModel.F_LastModifyUserId = userId;
                customerModel.F_Lat = lat;
                customerModel.F_Lng = lng;
                customerApp.SubmitFormAPI(customerModel, customerModel.F_Id);
                api.Message = "提交成功";
                api.Status = true;
                return api;
            }
            catch (Exception e)
            {
                api.Message = e.Message;
                return api;
            }
        }
    }
}