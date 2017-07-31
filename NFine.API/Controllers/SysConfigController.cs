using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using NFine.Application.SystemManage;
using NFine.API.Models;
using NFine.Domain.Entity.SystemManage;
using NFine.Code;

namespace NFine.API.Controllers
{
    public class SysConfigController : ApiController
    {
        /// <summary>
        /// 数据字典
        /// </summary>
        /// <param name="enCode">字典父节点英文简写</param>
        /// <returns></returns>
        public ApiResult<dynamic> PostItemDetail()
        {
            ApiResult<dynamic> api = new ApiResult<dynamic>();
            try
            {
                string enCode = Common.GetString("enCode");
                ItemsDetailApp itemsDetailApp = new ItemsDetailApp();
                var data = itemsDetailApp.GetItemList(enCode);
                List<object> list = new List<object>();
                foreach (ItemsDetailEntity item in data)
                {
                    list.Add(new { id = item.F_ItemCode, text = item.F_ItemName });
                }
                api.Message = "获取成功";
                api.Result = list;
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