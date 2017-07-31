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
using NFine.Application.VisitManage;
using NFine.Domain._03_Entity.VisitManage;
using NFine.Domain._03_Entity.TaskManage;

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
        public ApiResult<dynamic> PostTaskList()
        {
            ApiResult<dynamic> api = new ApiResult<dynamic>();
            try
            {

                int pageindex = Common.GetInt("pageIndex", 0);// jsondata.pageIndex ?? 0;
                int pagesize = Common.GetInt("pageSize", 0);// jsondata.pageSize ?? 0;
                string userid = Common.GetString("userid");
                string bank = Common.GetString("bank");
                string schedule = Common.GetString("schedule");
                string keyword = Common.GetString("keyword");
                string startdt = Common.GetString("startdt");
                string enddt = Common.GetString("enddt");
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
        /// 提交访单
        /// </summary>
        /// <param name="taskId">任务id</param>
        /// <param name="code">商户编码</param>
        /// <param name="name">商户名称</param>
        /// <param name="address">商户地址</param>
        /// <param name="tel">电话</param>
        /// <param name="mobile">手机</param>
        /// <param name="branch">支行</param>
        /// <param name="manager_mobile">经理电话</param>
        /// <param name="manager_name">经理名称</param>
        /// <param name="number">机器编号</param>
        /// <param name="count">机器数量</param>
        /// <param name="type">商户类型</param>
        /// <param name="cashier_name">收银员</param>
        /// <param name="cashier_code">收银员证件号</param>
        /// <param name="contacts">联系人</param>
        /// <param name="machine_type">机器类型</param>
        /// <param name="machine_count">机器数量</param>
        /// <param name="category">范畴</param>
        /// <param name="psam">psam卡</param>
        /// <param name="qu1">第一个问题 以下同理</param>
        /// <param name="qu2"></param>
        /// <param name="qu3"></param>
        /// <param name="qu4"></param>
        /// <param name="qu5"></param>
        /// <param name="qu6"></param>
        /// <param name="qu7"></param>
        /// <param name="qu8"></param>
        /// <param name="qu9"></param>
        /// <param name="qu10"></param>
        /// <param name="qu11"></param>
        /// <param name="qu12"></param>
        /// <param name="qu13"></param>
        /// <param name="qu14"></param>
        /// <param name="qu15"></param>
        /// <returns></returns>
        public ApiResult<dynamic> PostTaskData()
        {
            ApiResult<dynamic> api = new ApiResult<dynamic>();
            try
            {
                string taskId = Common.GetString("taskId");
                bool qu1 = Convert.ToBoolean(Common.GetString("qu1"));
                bool qu2 = Convert.ToBoolean(Common.GetString("qu2"));
                bool qu3 = Convert.ToBoolean(Common.GetString("qu3"));
                bool qu4 = Convert.ToBoolean(Common.GetString("qu4"));
                bool qu5 = Convert.ToBoolean(Common.GetString("qu5"));
                bool qu6 = Convert.ToBoolean(Common.GetString("qu6"));
                bool qu7 = Convert.ToBoolean(Common.GetString("qu7"));
                bool qu8 = Convert.ToBoolean(Common.GetString("qu8"));
                bool qu9 = Convert.ToBoolean(Common.GetString("qu9"));
                bool qu10 = Convert.ToBoolean(Common.GetString("qu10"));
                bool qu11 = Convert.ToBoolean(Common.GetString("qu11"));
                bool qu12 = Convert.ToBoolean(Common.GetString("qu12"));
                bool qu13 = Convert.ToBoolean(Common.GetString("qu13"));
                bool qu14 = Convert.ToBoolean(Common.GetString("qu14"));
                bool qu15 = Convert.ToBoolean(Common.GetString("qu15"));
                int count = Common.GetInt("count", 0);
                string code = Common.GetString("code");
                string name = Common.GetString("name");
                string address = Common.GetString("address");
                string tel = Common.GetString("tel");
                string machine_type = Common.GetString("machine_type");
                string mobile = Common.GetString("mobile");
                string machine_count = Common.GetString("machine_count");
                string branch = Common.GetString("branch");
                string manager_mobile = Common.GetString("manager_mobile");
                string manager_name = Common.GetString("manager_name");
                string number = Common.GetString("number");
                string type = Common.GetString("type");
                string cashier_name = Common.GetString("cashier_name");
                string cashier_code = Common.GetString("cashier_code");
                string contacts = Common.GetString("contacts");
                string category = Common.GetString("category");
                string psam = Common.GetString("psam");
                VisitEntity visitEntity = new VisitEntity();
                visitEntity.F_TaskId = taskId;
                visitEntity.F_Code = code;
                visitEntity.F_Name = name;
                visitEntity.F_Address = address;
                visitEntity.F_Tel = tel;
                visitEntity.F_Mobile = mobile;
                visitEntity.F_Branch = branch;
                visitEntity.F_Manager_Name = manager_name;
                visitEntity.F_Manager_Mobile = manager_mobile;
                visitEntity.F_Number = number;
                visitEntity.F_Count = count;
                visitEntity.F_Type = type;
                visitEntity.F_Cashier_Name = cashier_name;
                visitEntity.F_Cashier_Code = cashier_code;
                visitEntity.F_Contacts = contacts;
                visitEntity.F_Machine_Type = machine_type;
                visitEntity.F_Machine_Count = machine_count;
                visitEntity.F_Category = category;
                visitEntity.F_PSAM = psam;
                visitEntity.F_Qu1 = qu1;
                visitEntity.F_Qu2 = qu2;
                visitEntity.F_Qu3 = qu3;
                visitEntity.F_Qu4 = qu4;
                visitEntity.F_Qu5 = qu5; visitEntity.F_Qu6 = qu6;
                visitEntity.F_Qu7 = qu7;
                visitEntity.F_Qu8 = qu8;
                visitEntity.F_Qu9 = qu9;
                visitEntity.F_Qu10 = qu10; visitEntity.F_Qu11 = qu11;
                visitEntity.F_Qu12 = qu12;
                visitEntity.F_Qu13 = qu13; visitEntity.F_Qu14 = qu14;
                visitEntity.F_Qu15 = qu15;
                VisitApp VisitApp = new VisitApp();
                VisitApp.SubmitFormAPI(visitEntity);
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
        /// <summary>
        /// 任务处理 提交任务状态
        /// </summary>
        /// <param name="taskId">任务id</param>
        /// <param name="statusId">状态id</param>
        /// <returns></returns>
        public ApiResult<dynamic> PostTaskStatus()
        {
            ApiResult<dynamic> api = new ApiResult<dynamic>();
            try
            {
                string taskId = Common.GetString("taskId");
                string statusId = Common.GetString("statusId");
                TaskApp taskApp = new TaskApp();
                TaskEntity taskEntity = new TaskEntity();
                taskEntity.F_Status = statusId;
                taskApp.SubmitFormAPI(taskEntity, taskId);
                api.Message = "操作成功";
                api.Status = true;
                return api;
            }
            catch (Exception e)
            {
                api.Message = e.Message;
                return api;
            }
        }
        /// <summary>
        /// 任务详情,单据需要什么 取什么数据
        /// </summary>
        /// <param name="id">任务id</param>
        /// <returns></returns>
        public ApiResult<dynamic> Details()
        {
            ApiResult<dynamic> api = new ApiResult<dynamic>();
            try
            {
                string id = Common.GetString("id");
                TaskApp taskApp = new TaskApp();
                var taskData = taskApp.GetForm(id);
                CustomerApp customerApp = new CustomerApp();
                var customerData = customerApp.GetForm(taskData.F_CustomerId);
                UserApp userApp = new UserApp();
                var userData = userApp.GetForm(taskData.F_UserId);
                VisitApp visitApp = new VisitApp();
                var model = visitApp.GetList(id).OrderByDescending(d => d.F_CreatorTime).FirstOrDefault();
                var visitData = visitApp.GetForm(model == null ? string.Empty : model.F_Id);
                var data = new
                {
                    VisitData = visitData,
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
        public ApiResult<dynamic> PostCustomerData()
        {
            ApiResult<dynamic> api = new ApiResult<dynamic>();
            try
            {
                string customerId = Common.GetString("customerId");
                string userId = Common.GetString("userId");
                double lat = Convert.ToDouble(Common.GetString("lat"));
                double lng = Convert.ToDouble(Common.GetString("lng"));
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