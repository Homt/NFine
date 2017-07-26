using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using NFine.Application;
using NFine.API.Models;

namespace NFine.API.Controllers
{
    public class CustomerController : ApiController
    {
        //
        // GET: /Customer/
        CustomerApp customerApp = new CustomerApp();
        /// <summary>
        /// 客户地图分布 默认是整个客户列表 lat和lng是经纬度
        /// </summary>
        /// <param name="bank">银行</param>
        /// <param name="keyword">关键字</param>
        /// <returns></returns>
        public ApiResult<dynamic> PostDistribution(string bank = "", string keyword = "")
        {
            ApiResult<dynamic> api = new ApiResult<dynamic>();
            try
            {
                api.Result = customerApp.GetList(keyword, bank);
                api.Message = "获取成功";
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
